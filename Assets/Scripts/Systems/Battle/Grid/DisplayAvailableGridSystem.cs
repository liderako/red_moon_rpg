using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;
using TGS;

namespace RedMoonRPG.Battle.Grid
{
    public class DisplayAvailableGridSystem : ReactiveSystem<GridEntity>
    {
        public DisplayAvailableGridSystem(Contexts contexts) : base(contexts.grid)
        {
        }

        protected override ICollector<GridEntity> GetTrigger(IContext<GridEntity> context)
        {
            return context.CreateCollector(GridMatcher.ActiveAvatar);
        }

        protected override bool Filter(GridEntity entity)
        {
            return entity.activeAvatar.value == true && entity.hasActionPoint;
        }

        protected override void Execute(List<GridEntity> entities)
        {
            for (int i = 0; i < entities.Count; i++)
            {
                BzeroColor(entities[i].terrainGrid.value);
                ShowNeighbours(entities[i].mapPosition.value.vector, entities[i].actionPoint.value, entities[i].terrainGrid.value);
            }
        }

        private void BzeroColor(TerrainGridSystem tgs)
        {
            for (int i = 0; i < tgs.cells.Count; i++)
            {
                tgs.CellSetColor(tgs.CellGetIndex(tgs.cells[i]), new Color(0, 0, 0, 0));
            }
        }

        private void ShowNeighbours(Vector3 positionActiveAvatar, int maxPrice, TerrainGridSystem tgs)
        {
            BzeroColor(tgs);
            Cell charactercell = tgs.CellGetAtPosition(positionActiveAvatar, true);
            List<int> array = new List<int>();
            SearchAvailabeCell(tgs, maxPrice, array, 0, charactercell);
            tgs.CellSetColor(tgs.CellGetIndex(charactercell), new Color(0,0,0,0));
            array.Clear();
        }

        private void SearchAvailabeCell(TerrainGridSystem tgs, int maxPrice, List<int> array, int price, Cell currentCell)
        {
            if (price > maxPrice)
            {
                return;
            }
            if (!array.Contains(tgs.CellGetIndex(currentCell)) && currentCell.canCross)
            {
                array.Add(tgs.CellGetIndex(currentCell));
                tgs.CellSetColor(tgs.CellGetIndex(currentCell), new Color(194, 194, 194, 0.1f));
                // right
                SearchAvailabeCell(tgs, maxPrice, array, price + Tags.priceXstep,
                    tgs.cells[tgs.CellGetIndex(tgs.CellGetRow(tgs.CellGetIndex(currentCell)) + 1, tgs.CellGetColumn(tgs.CellGetIndex(currentCell)), true)]);
                // left
                SearchAvailabeCell(tgs, maxPrice, array, price + Tags.priceXstep,
                    tgs.cells[tgs.CellGetIndex(tgs.CellGetRow(tgs.CellGetIndex(currentCell)) - 1, tgs.CellGetColumn(tgs.CellGetIndex(currentCell)), true)]);
                // top
                SearchAvailabeCell(tgs, maxPrice, array, price + Tags.priceYstep,
                    tgs.cells[tgs.CellGetIndex(tgs.CellGetRow(tgs.CellGetIndex(currentCell)), tgs.CellGetColumn(tgs.CellGetIndex(currentCell)) + 1, true)]);
                // down
                SearchAvailabeCell(tgs, maxPrice, array, price + Tags.priceYstep,
                    tgs.cells[tgs.CellGetIndex(tgs.CellGetRow(tgs.CellGetIndex(currentCell)), tgs.CellGetColumn(tgs.CellGetIndex(currentCell)) - 1, true)]);
            }
        }
    }
}