using System.Collections;
using System.Collections.Generic;
using Entitas;
using UnityEngine;

namespace RedMoonRPG.Systems.Battle
{
    public class BaseAttackSystem : ReactiveSystem<BattleEntity>
    {
        public BaseAttackSystem(Contexts contexts) : base(contexts.battle)
        {
        }
        
        protected override ICollector<BattleEntity> GetTrigger(IContext<BattleEntity> context)
        {
            return context.CreateCollector(BattleMatcher.Attack);
        }
        
        protected override bool Filter(BattleEntity entity)
        {
            return entity.isAttack;
        }
        
        protected override void Execute(List<BattleEntity> avatar)
        {
            if (avatar.Count > 1)
            {
                Debug.LogError("BaseAttack error");
            }
            GameEntity unit = Contexts.sharedInstance.game.GetEntityWithName(avatar[0].name.name);
            unit.AddNextAnimation(AnimationTags.SwordAttack);
            avatar[0].isAttack = false;
            avatar[0].ReplaceActionPoint(0);
        }
    }
}