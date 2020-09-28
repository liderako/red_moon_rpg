using System.Collections;
using System.Collections.Generic;
using Entitas;
using UnityEngine;


namespace RedMoonRPG.Systems.Battle
{
    public class EstimateInflictedDamageSystem : ReactiveSystem<BattleEntity>
    {
        public EstimateInflictedDamageSystem(Contexts contexts) : base(contexts.battle)
        {
        }
        
        protected override ICollector<BattleEntity> GetTrigger(IContext<BattleEntity> context)
        {
            return context.CreateCollector(BattleMatcher.EstimateInflictedDamage);
        }
        
        protected override bool Filter(BattleEntity entity)
        {
            return entity.isEstimateInflictedDamage && entity.isBattle;
        }
        
        protected override void Execute(List<BattleEntity> avatar)
        {
            if (avatar.Count > 1)
            {
                Debug.LogError("EstimateInflictedDamageSystem error");
            }
            HashSet<CharacterEntity> array = Contexts.sharedInstance.character.GetEntitiesWithPersona(avatar[0].name.name);
            // CharacterEntity enemy = Contexts.sharedInstance.character.GetEntityWithName(avatar[0].targetEnemy.value.name.name); нужно чтобы считать попадаем или нет
            CharacterEntity character = Contexts.sharedInstance.character.GetEntityWithName(avatar[0].name.name);
            foreach (CharacterEntity item in array)
            {
                if (item.hasIndexMeleeDamage && item.hasNameItem)
                {
                    avatar[0].AddInflictedDamage(Random.Range(character.indexMeleeDamage.minValue, character.indexMeleeDamage.maxValue));
                }
            }
        }
    }
}