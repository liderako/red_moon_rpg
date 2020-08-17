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
				RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit))
                {
					GridEntity gridEntity = Contexts.sharedInstance.grid.GetEntityWithName(player.name.name);
					GridEntity cellPointer = Contexts.sharedInstance.grid.GetEntityWithCellPointer(true);
					if (cellPointer == null)
                    {
						cellPointer = Contexts.sharedInstance.grid.CreateEntity();
						cellPointer.AddCellPointer(true);
						GameObject go = GameObject.Instantiate<GameObject>(Resources.Load<GameObject>("Circle"));
						cellPointer.AddTransform(go.transform);
                    }
					gridEntity.ReplaceActiveAvatar(true);
					TerrainGridSystem tgs = gridEntity.terrainGrid.value;
					int t_cell = tgs.CellGetIndex(tgs.CellGetAtPosition(hit.point, true));
                    int startCell = tgs.CellGetIndex(tgs.CellGetAtPosition(player.transform.value.position, true));
                    cellPointer.transform.value.position = new Vector3(tgs.CellGetPosition(t_cell, true).x, tgs.CellGetPosition(t_cell, true).y + 0.25f, tgs.CellGetPosition(t_cell, true).z);
                    List<int> moveList = tgs.FindPath(startCell, t_cell, 0, 0, -1); /* третий параметр ограничит может дальность поиска клеток для пошагового боя*/
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