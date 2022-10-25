using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;
using TGS;

namespace RedMoonRPG.Systems.Battle.Grid
{
    public class AwakeDisplayGridSystem : ReactiveSystem<BattleEntity>
    {
        public AwakeDisplayGridSystem(Contexts contexts) : base(contexts.battle)
        {
        }
    
        protected override ICollector<BattleEntity> GetTrigger(IContext<BattleEntity> context)
        {
            return context.CreateCollector(BattleMatcher.Battle); // true
        }
    
        protected override bool Filter(BattleEntity entity)
        {
            return entity.hasTerrainGrid;
        }
    
        protected override void Execute(List<BattleEntity> entities)
        {
            for (int i = 0; i < entities.Count; i++)
            {
                bool state = entities[i].isBattle;
                entities[i].terrainGrid.value.showCells = state;
            }
        }
    }
}
