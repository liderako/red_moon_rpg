using System.Collections;
using System.Collections.Generic;
using Entitas;
using UnityEngine;


namespace RedMoonRPG.Systems.Battle
{
    public class DamageTakingSystem : ReactiveSystem<BattleEntity>
    {
        public DamageTakingSystem(Contexts contexts) : base(contexts.battle)
        {
        }
        
        protected override ICollector<BattleEntity> GetTrigger(IContext<BattleEntity> context)
        {
            return context.CreateCollector(BattleMatcher.EndAttack);
        }
        
        protected override bool Filter(BattleEntity entity)
        {
            return entity.isEndAttack && entity.isBattle;
        }
        
        protected override void Execute(List<BattleEntity> avatar)
        {
            if (avatar.Count > 1)
            {
                Debug.LogError("DamageTakingSystem error");
            }
            CharacterEntity enemy = Contexts.sharedInstance.character.GetEntityWithName(avatar[0].targetEnemy.value.name.name);
            enemy.AddDamaged(avatar[0].inflictedDamage.value);
            avatar[0].RemoveInflictedDamage();
            avatar[0].isEndAttack = false;
        }
    }
}