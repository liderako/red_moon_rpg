using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using Newtonsoft.Json;
using RedMoonRPG.Settings;
using TGS;

namespace RedMoonRPG.Settings
{
	public class CellEditor : MonoBehaviour
	{
        private TerrainGridSystem tgs;
        [SerializeField] private string _levelName;
        [SerializeField] private List<int> _cells = new List<int>();
        public Color colorClose;
        public Color colorOpen;

        private FileReader _fileReader;
		private void Start ()
		{
            tgs = TerrainGridSystem.instance;
            _fileReader = new FileReader();
            _cells = _fileReader.LoadGridJson(_levelName);
            DefaultDrawColor();
            tgs.OnCellClick += (cellIndex, buttonIndex) => changeCellOwner(cellIndex);
        }

		private void changeCellOwner(int cellIndex)
        {
            if (!_cells.Contains(cellIndex))
            {
                _cells.Add(cellIndex);
                tgs.CellSetColor(cellIndex, colorClose);
                tgs.CellSetCanCross(cellIndex, false);
            }
            else
            {
                _cells.RemoveAt(_cells.IndexOf(cellIndex));
                tgs.CellSetColor(cellIndex, Color.green);
                tgs.CellSetCanCross(cellIndex, true);
            }
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                Debug.Log("Save");
                _fileReader.SaveGridSystem(_levelName, _cells);
            }
        }

        private void DefaultDrawColor()
        {
            List<Cell> cells = tgs.cells;
            for (int i = 0; i < cells.Count; i++)
            {
                tgs.CellSetColor(tgs.CellGetIndex(cells[i]), colorOpen);
                tgs.CellSetCanCross(tgs.CellGetIndex(cells[i]), true);
            }
            DrawCloseGrids();
        }

        private void DrawCloseGrids()
        {
            for (int i = 0; i < _cells.Count; i++)
            {
                tgs.CellSetColor(_cells[i], colorClose);
                tgs.CellSetCanCross(_cells[i], false);
            }
        }
    }
}