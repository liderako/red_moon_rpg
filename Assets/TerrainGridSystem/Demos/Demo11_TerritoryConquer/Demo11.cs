using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace TGS {
				public class Demo11 : MonoBehaviour {

								TerrainGridSystem tgs;
								GUIStyle labelStyle;

								void Start () {
												// setup GUI styles
												//labelStyle = new GUIStyle ();
												//labelStyle.alignment = TextAnchor.MiddleCenter;
												//labelStyle.normal.textColor = Color.black;

												//// Get a reference to Terrain Grid System's API
												tgs = TerrainGridSystem.instance;

												//// Set colors for frontiers
												//tgs.territoryDisputedFrontierColor = Color.yellow;
												//tgs.TerritorySetFrontierColor (0, Color.red);
												//tgs.TerritorySetFrontierColor (1, Color.blue);

												//// Color for neutral territory
												//tgs.TerritoryToggleRegionSurface (2, true, new Color (0.2f, 0.2f, 0.2f));
												//tgs.TerritorySetNeutral (2, true);

												// listen to events
												tgs.OnCellClick += (cellIndex, buttonIndex) => changeCellOwner (cellIndex);
								}

								void changeCellOwner (int cellIndex)
								{
										tgs.CellToggleRegionSurface(cellIndex, true, Color.red);
										tgs.CellSetCanCross(cellIndex, false);
										//int currentTerritory = tgs.cells [cellIndex].territoryIndex;
										//// Looks for a neighbour territory
										//List<Cell> neighbours = tgs.CellGetNeighbours(cellIndex);
										//for (int k=0;k<neighbours.Count;k++) {
										//				if (neighbours[k].territoryIndex != currentTerritory) {
										//								currentTerritory = neighbours[k].territoryIndex;
										//								break;
										//				}
										//}
										//tgs.CellSetTerritory (cellIndex, currentTerritory);
								}
				}
}
