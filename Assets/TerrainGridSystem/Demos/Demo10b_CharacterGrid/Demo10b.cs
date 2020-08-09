using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System;

namespace TGS
{
	public class Demo10b : MonoBehaviour
	{

		TerrainGridSystem tgs;
		GUIStyle labelStyle;
		Rigidbody character;

		void Start () {
			tgs = TerrainGridSystem.instance;

			// setup GUI resizer - only for the demo
			GUIResizer.Init (800, 500); 

			// setup GUI styles
			labelStyle = new GUIStyle ();
			labelStyle.alignment = TextAnchor.MiddleLeft;
			labelStyle.normal.textColor = Color.black;

			character = GameObject.Find ("Character").GetComponent<Rigidbody>();
		}

		void OnGUI () {
			// Do autoresizing of GUI layer
			GUIResizer.AutoResize ();
			GUI.backgroundColor = new Color (0.8f, 0.8f, 1f, 0.5f);
			GUI.Label (new Rect (10, 5, 160, 30), "Move the ball with WASD and press G to reposition grid around it.", labelStyle);
			GUI.Label (new Rect (10, 25, 160, 30), "Press N to show neighbour cells around the character position.", labelStyle);
			GUI.Label (new Rect (10, 45, 160, 30), "Press C to snap to center of cell.", labelStyle);
			GUI.Label (new Rect (10, 65, 160, 30), "Open the Demo10b.cs script to learn how to assign gridCenter property using code.", labelStyle);
		}

		void Update() {

			// Move ball
			const float strength = 10f;
			if (Input.GetKey(KeyCode.W)) {
				character.AddForce(Vector3.forward * strength);
			}
			if (Input.GetKey(KeyCode.S)) {
				character.AddForce(Vector3.back * strength);
			}
			if (Input.GetKey(KeyCode.A)) {
				character.AddForce(Vector3.left * strength);
			}
			if (Input.GetKey(KeyCode.D)) {
				character.AddForce(Vector3.right * strength);
			}
			if (Input.GetKeyDown(KeyCode.C)) {
				SnapToCellCenter();
			}

			// Reposition grid
			if (Input.GetKeyDown(KeyCode.G)) {
				RepositionGrid();
			}

			// Show neighbour cells
			if (Input.GetKeyDown(KeyCode.N)) {
				ShowNeighbours(character.transform.position);
			}

			// Position camera
			Camera.main.transform.position = character.transform.position + new Vector3(0,20,-20);
			Camera.main.transform.LookAt(character.transform.position);

		}

		// Updates grid position around newPosition
		void RepositionGrid() {
			tgs.SetGridCenterWorldPosition(character.transform.position, true);
		}

		// Moves character to center of current cell
		void SnapToCellCenter() {
			Vector3 pos = tgs.SnapToCell(character.transform.position);
			character.transform.position = pos + Vector3.up;
		}


		// Highlight neighbour cells around character posiiton
		void ShowNeighbours(Vector3 position)
		{
			BzeroColor();
			Cell charactercell = tgs.CellGetAtPosition(position, true);
			_array = new List<int>();
			Show(0, charactercell);
			tgs.CellSetColor(tgs.CellGetIndex(charactercell), Color.green);
			_array.Clear();
		}

		private const int maxPrice = 4;
		private const int priceXstep = 1;
		private const int priceYstep = 1;
		private List<int> _array;

		private void Show(int price, Cell currentCell)
        {
			if (price > maxPrice)
            {
				return;
            }
			if (!_array.Contains(tgs.CellGetIndex(currentCell)))
            {
				_array.Add(tgs.CellGetIndex(currentCell));
				tgs.CellSetColor(tgs.CellGetIndex(currentCell), Color.red);
				Show(price + priceXstep, tgs.cells[tgs.CellGetIndex(tgs.CellGetRow(tgs.CellGetIndex(currentCell)) + 1, tgs.CellGetColumn(tgs.CellGetIndex(currentCell)), true)]);
				Show(price + priceXstep, tgs.cells[tgs.CellGetIndex(tgs.CellGetRow(tgs.CellGetIndex(currentCell)) - 1, tgs.CellGetColumn(tgs.CellGetIndex(currentCell)), true)]);
				Show(price + priceYstep, tgs.cells[tgs.CellGetIndex(tgs.CellGetRow(tgs.CellGetIndex(currentCell)), tgs.CellGetColumn(tgs.CellGetIndex(currentCell)) + 1, true)]);
				Show(price + priceYstep, tgs.cells[tgs.CellGetIndex(tgs.CellGetRow(tgs.CellGetIndex(currentCell)), tgs.CellGetColumn(tgs.CellGetIndex(currentCell)) - 1, true)]);
				//Show(price + priceYstep + priceXstep, tgs.cells[tgs.CellGetIndex(tgs.CellGetRow(tgs.CellGetIndex(currentCell)) - 1, tgs.CellGetColumn(tgs.CellGetIndex(currentCell)) - 1, true)]);
				//Show(price + priceYstep + priceXstep, tgs.cells[tgs.CellGetIndex(tgs.CellGetRow(tgs.CellGetIndex(currentCell)) + 1, tgs.CellGetColumn(tgs.CellGetIndex(currentCell)) - 1, true)]);
				//Show(price + priceYstep + priceXstep, tgs.cells[tgs.CellGetIndex(tgs.CellGetRow(tgs.CellGetIndex(currentCell)) - 1, tgs.CellGetColumn(tgs.CellGetIndex(currentCell)) + 1, true)]);
				//Show(price + priceYstep + priceXstep, tgs.cells[tgs.CellGetIndex(tgs.CellGetRow(tgs.CellGetIndex(currentCell)) + 1, tgs.CellGetColumn(tgs.CellGetIndex(currentCell)) + 1, true)]);
			}
        }

		private void BzeroColor()
        {
			for (int i = 0; i < tgs.cells.Count; i++)
            {
				tgs.CellSetColor(tgs.CellGetIndex(tgs.cells[i]), Color.black);
            }
        }
    }
}