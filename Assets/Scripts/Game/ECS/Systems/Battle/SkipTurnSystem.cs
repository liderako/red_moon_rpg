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
            BattleEntity battleManager = Contexts.sharedInstance.battle.GetEntityWithName(Tags.battleManagerEntity);
            Debug.Log("Skip turn " + battleManager.battleList.units[battleManager.battleList.iterator].name.name);
            battleManager.battleList.battleAvatars[battleManager.battleList.iterator].ReplaceActiveAvatar(false);
            battleManager.battleList.units[battleManager.battleList.iterator].ReplaceActiveAvatar(false);
            battleManager.isUpdateActiveAvatar = true;
            battleManager.isSkipTurn = false;
            if (entities[0].battleList.battleAvatars[battleManager.battleList.iterator].hasPathDisplay)
            {
                DisplayAvailableGridSystem.BzeroCell(entities[0].battleList.battleAvatars[battleManager.battleList.iterator].terrainGrid.value,
                    entities[0].battleList.battleAvatars[battleManager.battleList.iterator].pathDisplay.value);
            }
        }
    }
}