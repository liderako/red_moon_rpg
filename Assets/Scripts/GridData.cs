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
            //DefaultDrawColor();
        }

        private void DefaultDrawColor()
        {
            List<Cell> cells = _tgs.cells;
            for (int i = 0; i < cells.Count; i++)
            {
                _tgs.CellSetColor(_tgs.CellGetIndex(cells[i]), colorOpen);
                _tgs.CellSetCanCross(_tgs.CellGetIndex(cells[i]), true);
            }
            //DrawCloseGrids();
        }

        private void DrawCloseGrids()
        {
            for (int i = 0; i < _cells.Count; i++)
            {
                _tgs.CellSetColor(_cells[i], colorClose);
                _tgs.CellSetCanCross(_cells[i], false);
            }
        }
    }
}