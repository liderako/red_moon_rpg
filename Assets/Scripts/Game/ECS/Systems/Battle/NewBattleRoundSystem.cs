using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;

namespace RedMoonRPG.Systems.Battle
{
    public class NewBattleRound : ReactiveSystem<BattleEntity>
    {
        public NewBattleRound(Contexts contexts) : base(contexts.battle)
        {
        }

        protected override ICollector<BattleEntity> GetTrigger(IContext<BattleEntity> context)
        {
            return context.CreateCollector(BattleMatcher.UpdateBattleRound);
        }

        protected override bool Filter(BattleEntity entity)
        {
            return entity.isUpdateBattleRound;
        }

        protected override void Execute(List<BattleEntity> entities)
        {
            if (entities.Count > 1)
            {
                Debug.LogError("NewBattleRound error");
                return;
            }
            BattleEntity battleManager = entities[0];
            battleManager.battleList.iterator = -1;
            battleManager.isUpdateActiveAvatar = true;
            battleManager.isUpdateBattleRound = false;
            battleManager.round.value += 1;
            Debug.Log("New round");
        }
    }
}