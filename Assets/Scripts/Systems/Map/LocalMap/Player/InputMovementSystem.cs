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
					gridEntity.ReplaceActiveAvatar(true);
					TerrainGridSystem tgs = gridEntity.terrainGrid.value;
					int t_cell = tgs.CellGetIndex(tgs.CellGetAtPosition(hit.point, true));
                    int startCell = tgs.CellGetIndex(tgs.CellGetAtPosition(player.transform.value.position, true));
					List<int> moveList = tgs.FindPath(startCell, t_cell, 0, 0, -1); /* третий параметр ограничит может дальность поиска клеток для пошагового боя*/
                    if (moveList == null)
                    {
                        return;
                    }
					gridEntity.ReplacePath(moveList, 0);
					gridEntity.ReplaceMapPosition(gridEntity.mapPosition.value);
					return;
                }
            }
		}
	}
}