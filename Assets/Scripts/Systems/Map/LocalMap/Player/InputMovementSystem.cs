using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TGS;
using Entitas;

namespace RedMoonRPG.Systems.LocalMap.Player
{
    public class InputMovementSystem : IExecuteSystem
	{
        public void Execute()
		{
			if (Input.GetMouseButtonDown(0))
			{
				GameEntity player = ESCLibrary.GetActivePlayer();
				if (player == null) return;
				RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				LayerMask mask = LayerMask.GetMask("Default");
				if (Physics.Raycast(ray, out hit, 1000, mask))
                {
					GridEntity gridEntity = Contexts.sharedInstance.grid.GetEntityWithName(player.name.name);
					TerrainGridSystem tgs = gridEntity.terrainGrid.value;
					/* Если клетка закрыта то выходим*/
					if (tgs.CellGetAtPosition(hit.point, true).canCross == false)
                    {
						return;
                    }
					GridEntity cellPointer = Contexts.sharedInstance.grid.GetEntityWithCellPointer(true);
					/*
					 * инициализируем клетку
					 */
					if (cellPointer == null)
                    {
						cellPointer = Contexts.sharedInstance.grid.CreateEntity();
						cellPointer.AddCellPointer(true);
						GameObject go = GameObject.Instantiate<GameObject>(Resources.Load<GameObject>("Circle"));
						cellPointer.AddTransform(go.transform);
                    }
					gridEntity.ReplaceActiveAvatar(true);
					int endCell = tgs.CellGetIndex(tgs.CellGetAtPosition(hit.point, true));
					int startCell = tgs.CellGetIndex(tgs.CellGetAtPosition(player.transform.value.position, true));
                    cellPointer.transform.value.position = new Vector3(tgs.CellGetPosition(endCell, true).x, tgs.CellGetPosition(endCell, true).y + 0.25f, tgs.CellGetPosition(endCell, true).z);
                    List<int> moveList = tgs.FindPath(startCell, endCell, 0, 0, -1); /* третий параметр ограничит может дальность поиска клеток для пошагового боя*/
                    if (moveList == null)
                    {
                        return;
                    }
					if (!player.hasNextAnimation)
					{
						player.AddNextAnimation(AnimationTags.walk);
					}
					else
                    {
						player.ReplaceNextAnimation(AnimationTags.walk);
					}
					gridEntity.ReplacePath(moveList, 0);
					gridEntity.ReplaceMapPosition(gridEntity.mapPosition.value);
					return;
                }
            }
		}
	}
}