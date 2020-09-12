using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TGS;

namespace RedMoonRPG.Settings
{
    public class GridData : MonoBehaviour
    {
        [SerializeField] private string _levelName;
        [SerializeField] private List<int> _cells = new List<int>();
        [SerializeField] private Color colorClose;
        [SerializeField] private Color colorOpen;
        private TerrainGridSystem _tgs;

        private void Start()
        {
            _cells = new FileReader().LoadGridJson(_levelName);
            _tgs = TerrainGridSystem.instance;
            DefaultGridSettings();
        }

        private void DefaultGridSettings()
        {
            List<Cell> cells = _tgs.cells;
            for (int i = 0; i < cells.Count; i++)
            {
                _tgs.CellSetCanCross(_tgs.CellGetIndex(cells[i]), true);
            }
            for (int i = 0; i < _cells.Count; i++)
            {
                _tgs.CellSetCanCross(_cells[i], false);
            }
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                TerrainGridSystem tgs = TerrainGridSystem.instance;

                for (int i = 0; i < tgs.cells.Count; i++)
                {

                    if (tgs.cells[i].canCross == false)
                    {
                        _tgs.CellSetColor(i, colorClose);
                    }
                    else
                    {
                        _tgs.CellSetColor(i, colorOpen);
                    }
                }
            }
        }

        private void DefaultDrawColor()
        {
            List<Cell> cells = _tgs.cells;
            for (int i = 0; i < cells.Count; i++)
            {
                _tgs.CellSetColor(_tgs.CellGetIndex(cells[i]), colorOpen);
            }
        }

        private void DrawCloseGrids()
        {
            for (int i = 0; i < _cells.Count; i++)
            {
                _tgs.CellSetColor(_cells[i], colorClose);
            }
        }
    }
}