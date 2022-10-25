using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;

namespace RedMoonRPG.Systems
{
    /*
    ** Система для модификации основных параметров персонажа
    ** Система будет использоваться при создании персонажа либо редактировании основных параметров.
    */
    public class ModifyStatSystem : ReactiveSystem<CharacterEntity>
    {
        public ModifyStatSystem(Contexts contexts) : base(contexts.character)
        {
        }
    
        protected override ICollector<CharacterEntity> GetTrigger(IContext<CharacterEntity> context)
        {
            return context.CreateCollector(CharacterMatcher.ModifiedStat);
        }
    
        protected override bool Filter(CharacterEntity entity)
        {
            return entity.hasModifiedStat;
        }
    
        protected override void Execute(List<CharacterEntity> entities)
        {
            int len = entities.Count;
            for (int i = 0; i < len; i++)
            {
                ChangeStat(entities[i]);
                entities[i].RemoveModifiedStat();
            }
        }
    
        private void ChangeStat(CharacterEntity entity)
        {
            switch (entity.modifiedStat.stat)
            {
                case Stat.Intellect:
                    entity.ReplaceIntellect(entity.modifiedStat.newValue);
                    break;
                case Stat.Endurance:
                    entity.ReplaceEndurance(entity.modifiedStat.newValue);
                    break;
                case Stat.Luck:
                    entity.ReplaceLuck(entity.modifiedStat.newValue);
                    break;
                case Stat.Strength:
                    entity.ReplaceStrength(entity.modifiedStat.newValue);
                    break;
                case Stat.Dexterity:
                    entity.ReplaceDexterity(entity.modifiedStat.newValue);
                    break;
                case Stat.Attention:
                    entity.ReplaceAttention(entity.modifiedStat.newValue);
                    break;
                case Stat.Personality:
                    entity.ReplacePersonality(entity.modifiedStat.newValue);
                    break;
            }
        }
    }
}