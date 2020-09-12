using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;

namespace RedMoonRPG.Systems.Battle
{
    public class EndTurnSystem : ReactiveSystem<BattleEntity>
    {
        public EndTurnSystem(Contexts contexts) : base(contexts.battle)
        {
        }
    
        protected override ICollector<BattleEntity> GetTrigger(IContext<BattleEntity> context)
        {
            return context.CreateCollector(BattleMatcher.ActionPoint);
        }
    
        protected override bool Filter(BattleEntity entity)
        {
            return entity.activeAvatar.value == true && entity.actionPoint.value == 0 && entity.path.gridPath.Count == 0;
        }
    
        protected override void Execute(List<BattleEntity> entities)
        {
            if (entities.Count > 1)
            {
                Debug.LogError("EndTurnSystem error");
                return;
            }
            BattleEntity battleManager = Contexts.sharedInstance.battle.GetEntityWithName(Tags.battleManagerEntity);
            Debug.Log("End turn " + battleManager.battleList.units[battleManager.battleList.iterator].name.name);
            battleManager.battleList.battleAvatars[battleManager.battleList.iterator].ReplaceActiveAvatar(false);
            battleManager.battleList.units[battleManager.battleList.iterator].ReplaceActiveAvatar(false);
            battleManager.isUpdateActiveAvatar = true;
        }
    }
}