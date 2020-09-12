using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;
using TGS;

namespace RedMoonRPG.Systems.Battle.Grid
{
    public class DisplayAvailableGridSystem : ReactiveSystem<BattleEntity>
    {
        public DisplayAvailableGridSystem(Contexts contexts) : base(contexts.battle)
        {
        }
    
        protected override ICollector<BattleEntity> GetTrigger(IContext<BattleEntity> context)
        {
            return context.CreateCollector(BattleMatcher.ActiveAvatar);
        }
    
        protected override bool Filter(BattleEntity entity)
        {
            return entity.isBattle && entity.activeAvatar.value == true && entity.hasActionPoint && entity.isPlayer;
        }
    
        protected override void Execute(List<BattleEntity> entities)
        {
            if (entities.Count != 1)
            {
                Debug.LogError("Сущность для этой системы может быть только одна.");
                return;
            }
            if (entities[0].hasPathDisplay)
            {
                BzeroCell(entities[0].terrainGrid.value, entities[0].pathDisplay.value);
                List<int> array = ShowNeighbours(entities[0].mapPosition.value.vector, entities[0].actionPoint.value, entities[0].terrainGrid.value);
                entities[0].ReplacePathDisplay(array);
            }
            else
            {
                List<int> array = ShowNeighbours(entities[0].mapPosition.value.vector, entities[0].actionPoint.value, entities[0].terrainGrid.value);
                entities[0].AddPathDisplay(array);
            }
        }
        
        public static void BzeroCell(TerrainGridSystem tgs, List<int> array)
        {
            int len = array.Count;
            for (int i = 0; i < len; i++)
            {
                tgs.CellSetColor(array[i], new Color(0, 0, 0, 0));
            }
        }
    
        private List<int> ShowNeighbours(Vector3 positionActiveAvatar, int maxPrice, TerrainGridSystem tgs)
        {
            Cell charactercell = tgs.CellGetAtPosition(positionActiveAvatar, true);
            List<int> array = new List<int>();
            SearchAvailabeCell(tgs, maxPrice, array, 0, charactercell);
            tgs.CellSetColor(tgs.CellGetIndex(charactercell), new Color(0,0,0,0));
            return array;
        }
    
        private void SearchAvailabeCell(TerrainGridSystem tgs, int maxPrice, List<int> array, int price, Cell currentCell)
        {
            if (price > maxPrice - 1)
            {
                return;
            }
            int row = currentCell.row;
            int column = currentCell.column;
            int right = tgs.CellGetIndex(row + 1, column, true);
            int left = tgs.CellGetIndex(row - 1, column, true);
            int top = tgs.CellGetIndex(row, column + 1, true);
            int down = tgs.CellGetIndex(row, column - 1, true);
            if (tgs.cells[right].canCross)
            {
                array.Add(right);
                tgs.CellSetColor(right, new Color(194, 194, 194, 0.1f));
                SearchAvailabeCell(tgs, maxPrice, array, price + Tags.priceXstep, tgs.cells[right]);
            }
            if (tgs.cells[left].canCross)
            {
                array.Add(left);
                tgs.CellSetColor(left, new Color(194, 194, 194, 0.1f));
                SearchAvailabeCell(tgs, maxPrice, array, price + Tags.priceXstep, tgs.cells[left]);
            }
            if (tgs.cells[top].canCross)
            {
                array.Add(top);
                tgs.CellSetColor(top, new Color(194, 194, 194, 0.1f));
                SearchAvailabeCell(tgs, maxPrice, array, price + Tags.priceXstep, tgs.cells[top]);
            }
            if (tgs.cells[down].canCross)
            {
                array.Add(down);
                tgs.CellSetColor(down, new Color(194, 194, 194, 0.1f));
                SearchAvailabeCell(tgs, maxPrice, array, price + Tags.priceXstep, tgs.cells[down]);
            }
        }
    }
}