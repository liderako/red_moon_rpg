using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace TGS {
	public class SphereController : MonoBehaviour
	{
		private TerrainGridSystem tgs;
		[SerializeField]private List<int> moveList = new List<int>();
		private short moveCounter = 0;
		//private int lastTerrIndex;

		// Use this for initialization
		void Start ()
		{
			tgs = TerrainGridSystem.instance;
		}
	
		// Update is called once per frame
		void Update ()
		{
			// Blinks the new territory under the sphere
			//Territory terr = tgs.TerritoryGetAtPosition (transform.position, true);
			//int terrIndex = tgs.TerritoryGetIndex (terr);
			//if (terrIndex != lastTerrIndex)
			//{
			//	lastTerrIndex = terrIndex;
			//	tgs.TerritoryBlink (terrIndex, Color.red, 0.5f);
			//}

			if (moveList != null)
			{
				if (moveCounter < moveList.Count)
				{
					Move(tgs.CellGetPosition(moveList[moveCounter]));
				}
				else
				{
					moveList.Clear();
					moveCounter = 0;
				}
			}

			if (Input.GetMouseButtonUp(0))
			{
				RaycastHit hit;
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				if (Physics.Raycast(ray, out hit))
				{
					int t_cell = tgs.CellGetIndex(tgs.CellGetAtPosition(hit.point, true));
					int startCell = tgs.CellGetIndex(tgs.CellGetAtPosition(transform.position, true));
					moveList = tgs.FindPath(startCell, t_cell, 0, 0,-1); // третий параметр ограничит может дальность поиска клеток для пошагового боя
					if (moveList == null)
					{
						return;
					}
					moveCounter = 0;
					return;
				}
				else
				{
					Debug.Log("NULL_CELL");
					return;
				}
			}
		}

		void Move (Vector3 in_vec) {
			float speed = 35;
            float step = speed * Time.deltaTime;

			// target position must account the sphere height since the cellGetPosition will return the center of the cell which is at floor.
			in_vec.y += transform.localScale.y * 0.5f; 
			transform.position = Vector3.MoveTowards (transform.position, in_vec, step);

			// Check if character has reached next cell (we use a small threshold to avoid floating point comparison; also we check only xz plane since the character y position could be adjusted or limited
			// by the slope of the terrain).
			float dist = Vector2.Distance (new Vector2 (transform.position.x, transform.position.z), new Vector2 (in_vec.x, in_vec.z));
			if (dist <= 0.1f)
			{
				moveCounter++;
			}
		}

	}
}




