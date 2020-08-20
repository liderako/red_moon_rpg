using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;
using TGS;

namespace RedMoonRPG.Systems.Battle.Grid
{
    public class AwakeDisplayGridSystem : ReactiveSystem<GridEntity>
    {
        public AwakeDisplayGridSystem(Contexts contexts) : base(contexts.grid)
        {
        }

        protected override ICollector<GridEntity> GetTrigger(IContext<GridEntity> context)
        {
            return context.CreateCollector(GridMatcher.Battle);
        }

        protected override bool Filter(GridEntity entity)
        {
            return entity.hasTerrainGrid;
        }

        protected override void Execute(List<GridEntity> entities)
        {
            for (int i = 0; i < entities.Count; i++)
            {
                bool state = entities[i].isBattle;
                entities[i].terrainGrid.value.showCells = state;
            }
        }
    }
}
