using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System;

namespace TGS
{
	/// <summary>
	/// Marks random cells as unpassable
	/// </summary>
	public class Obstacles : MonoBehaviour
	{
		[SerializeField] public List<GameObject> _cells;
		void Start ()
		{
            TerrainGridSystem tgs = TerrainGridSystem.instance;
            for (int k = 0; k < _cells.Count; k++)
            {
				int cellIndex = Int32.Parse(_cells[k].name);
                tgs.CellToggleRegionSurface(cellIndex, true, Color.white);
                tgs.CellSetCanCross(cellIndex, false);
            }
        }
	}
}




