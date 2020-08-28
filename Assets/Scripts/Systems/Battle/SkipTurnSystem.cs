using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;
using RedMoonRPG.Systems.Battle.Grid;

namespace RedMoonRPG.Systems.Battle
{
    public class SkipTurnSystem : ReactiveSystem<BattleEntity>
    {
        public SkipTurnSystem(Contexts contexts) : base(contexts.battle)
        {
        }

        protected override ICollector<BattleEntity> GetTrigger(IContext<BattleEntity> context)
        {
            return context.CreateCollector(BattleMatcher.SkipTurn);
        }

        protected override bool Filter(BattleEntity entity)
        {
            return entity.isSkipTurn;
        }

        protected override void Execute(List<BattleEntity> entities)
        {
            if (entities.Count != 1)
            {
                Debug.LogError("SkipTurnSystem error");
                return;
            }
            int i = entities[0].battleList.iterator;
            entities[0].battleList.gridAvatars[i].ReplaceActionPoint(0);
            DisplayAvailableGridSystem.BzeroCell(entities[0].battleList.gridAvatars[i].terrainGrid.value, entities[0].battleList.gridAvatars[i].pathDisplay.value);
            entities[0].isUpdateActiveAvatar = true;
        }
    }
}