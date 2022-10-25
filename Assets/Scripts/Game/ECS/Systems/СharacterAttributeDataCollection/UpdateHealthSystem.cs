using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;
/*
** Система обновляет данные про максимальное значение хп у персонажа
** собирая данные со всех бафоф и артефактов
*/
namespace RedMoonRPG.Systems
{
    public class UpdateHealthSystem : ReactiveSystem<CharacterEntity>
    {
        private Contexts _contexts;
        private int _lvlBonus;
    
        public UpdateHealthSystem(Contexts contexts, GameBalanceSettings gmt) : base(contexts.character)
        {
            _contexts = contexts;
            _lvlBonus = gmt.HpForLevelEndurance;
        }
    
        protected override ICollector<CharacterEntity> GetTrigger(IContext<CharacterEntity> context)
        {
            return context.CreateCollector(CharacterMatcher.HealthUpdate);
        }
    
        protected override bool Filter(CharacterEntity entity)
        {
            return entity.isHealthUpdate && entity.hasPersona;
        }
    
        protected override void Execute(List<CharacterEntity> entities)
        {
            int len = entities.Count;
            for (int i = 0; i < len; i++)
            {
                HashSet<CharacterEntity> array = _contexts.character.GetEntitiesWithPersona(entities[i].persona.value);
                int hp = 0;
                foreach (CharacterEntity gn in array)
                {
                    if (gn.hasEndurance)
                    {
                        hp += (gn.endurance.value * _lvlBonus);
                    }
                }
                entities[i].health.maxValue = hp;
                if (entities[i].health.value > entities[i].health.maxValue)
                {
                    entities[i].health.value -= (entities[i].health.value - entities[i].health.maxValue);
                }
                else if (entities[i].health.value == 0)
                {
                    entities[i].health.value = hp;
                }
                entities[i].isHealthUpdate = false;
            }
        }
    }
}