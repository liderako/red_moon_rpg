using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;

namespace RedMoonRPG.Systems.Battle
{
    public class QueueBattleSystems : ReactiveSystem<BattleEntity>
    {
        public QueueBattleSystems(Contexts contexts) : base(contexts.battle)
        {
        }
    
        protected override ICollector<BattleEntity> GetTrigger(IContext<BattleEntity> context)
        {
            return context.CreateCollector(BattleMatcher.UpdateActiveAvatar);
        }
    
        protected override bool Filter(BattleEntity entity)
        {
            return entity.hasBattleList;
        }
    
        protected override void Execute(List<BattleEntity> entities)
        {
            if (entities.Count > 1)
            {
                Debug.LogError("QueueBattle error");
                return;
            }
            BattleEntity battleManager = entities[0];
            battleManager.battleList.iterator += 1;
            battleManager.isUpdateActiveAvatar = false;
            int i = battleManager.battleList.iterator;
            if (battleManager.battleList.iterator >= battleManager.battleList.battleAvatars.Count)
            {
                battleManager.isUpdateBattleRound = true;
                Debug.Log("Новый раунд");
                return;
            }
            battleManager.battleList.battleAvatars[i].ReplaceActiveAvatar(true);
            battleManager.battleList.battleAvatars[i].ReplaceActionPoint(battleManager.battleList.units[i].dexterity.value);
            battleManager.battleList.units[i].ReplaceActiveAvatar(true);
            Debug.Log("Now Active Avatar is " + battleManager.battleList.units[i].name.name);
        }
    }
}