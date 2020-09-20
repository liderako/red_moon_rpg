using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TGS;
using Entitas;

namespace RedMoonRPG.Systems.Battle.Movement
{
    public class InputMovementSystem : ReactiveSystem<InputEntity>
	{
		public InputMovementSystem(Contexts contexts) : base(contexts.input)
		{
		}
 
		protected override ICollector<InputEntity> GetTrigger(IContext<InputEntity> context)
		{
			return context.CreateCollector(InputMatcher.InputMouse);
		}
 
		protected override bool Filter(InputEntity entity)
		{
			return entity.hasInputMouse && entity.inputMouse.button == ButtonTags.mouseButton0 && entity.isBattle;
		}
 
		protected override void Execute(List<InputEntity> entities)
		{
			for (int i = 0; i < entities.Count; i++)
            {
				entities[i].RemoveInputMouse();
            }
			GameEntity player = ESCLibrary.GetActivePlayer();
			if (player == null) return;
			RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			LayerMask mask = LayerMask.GetMask("Default");
			if (Physics.Raycast(ray, out hit, 1000, mask))
            {
				BattleEntity avatar = Contexts.sharedInstance.battle.GetEntityWithName(player.name.name);
				if (avatar.hasPath && avatar.path.iterator != 0 && avatar.path.iterator > 0 && avatar.path.iterator < avatar.path.gridPath.Count)
                {
					Debug.Log("Движение еще не окончено");
					return;
                }
				if (avatar.actionPoint.value <= 0)
                {
					Debug.Log("Очков действия не хватает");
					return;
                }
				TerrainGridSystem tgs = avatar.terrainGrid.value;
				/* Если клетка закрыта то выходим*/
				if (tgs.CellGetAtPosition(hit.point, true).canCross == false)
                {
					return;
                }
				BattleEntity cellPointer = Contexts.sharedInstance.battle.GetEntityWithCellPointer(true);
				/*инициализируем клетку */
				if (cellPointer == null)
                {
					cellPointer = Contexts.sharedInstance.battle.CreateEntity();
					cellPointer.AddCellPointer(true);
					GameObject go = GameObject.Instantiate<GameObject>(Resources.Load<GameObject>("Circle"));
					cellPointer.AddTransform(go.transform);
                }
				/* Задаем параметры для алгоритма поиска пути*/
				Cell cell = avatar.terrainGrid.value.CellGetAtPosition(avatar.mapPosition.value.vector, true);
				avatar.terrainGrid.value.CellSetCanCross(avatar.terrainGrid.value.CellGetIndex(cell), true);
				int endCell = tgs.CellGetIndex(tgs.CellGetAtPosition(hit.point, true));
				int startCell = tgs.CellGetIndex(tgs.CellGetAtPosition(player.transform.value.position, true));
				int coast = 0;
				List<int> moveList = tgs.FindPath(startCell, endCell, out coast, avatar.actionPoint.value, 0, -1); /* третий параметр ограничит может дальность поиска клеток для пошагового боя*
				/* защита от пустых путей */
				if (moveList == null)
                {
                    return;
                }
				/* настраиваем анимацию */
				if (!player.hasNextAnimation)
				{
					player.AddNextAnimation(AnimationTags.run);
				}
				else
                {
					player.ReplaceNextAnimation(AnimationTags.run);
				}
				/* запускаем тригеры для старта систем*/
				cellPointer.transform.value.position = new Vector3(tgs.CellGetPosition(endCell, true).x, tgs.CellGetPosition(endCell, true).y + 0.25f, tgs.CellGetPosition(endCell, true).z);
				avatar.ReplacePath(moveList, 0);
				avatar.ReplaceMapPosition(avatar.mapPosition.value);
				avatar.ReplaceActionPoint(avatar.actionPoint.value - coast);
				if (avatar.hasPathDisplay)
				{
					Grid.DisplayAvailableGridSystem.BzeroCell(avatar.terrainGrid.value, avatar.pathDisplay.value);
				}
            }
		}
	}
}