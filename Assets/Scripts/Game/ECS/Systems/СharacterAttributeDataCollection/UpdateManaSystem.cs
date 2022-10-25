using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;
using RedMoonRPG.Settings;

/*
** Система обновляет данные про максимальное значение маны у персонажа
** собирая данные со всех бафоф и артефактов
*/
namespace RedMoonRPG.Systems
{
    public class UpdateManaSystem : ReactiveSystem<CharacterEntity>
    {
        private Contexts _contexts;
        private int _lvlBonus;
    
        public UpdateManaSystem(Contexts contexts, GameBalanceSettings gmt) : base(contexts.character)
        {
            _contexts = contexts;
            _lvlBonus = gmt.ManaForLevelIntellect;
        }
    
        protected override ICollector<CharacterEntity> GetTrigger(IContext<CharacterEntity> context)
        {
            return context.CreateCollector(CharacterMatcher.ManaUpdate);
        }
    
        /*
        ** Система будет вызиваться только один раз на персонажа благодаря тому
        ** что Имя может быть только у одного Entity на персонажа
        */
        protected override bool Filter(CharacterEntity entity)
        {
            return entity.isManaUpdate && entity.hasPersona;
        }
    
        protected override void Execute(List<CharacterEntity> entities)
        {
            int len = entities.Count;
            for (int i = 0; i < len; i++)
            {
                HashSet<CharacterEntity> array = _contexts.character.GetEntitiesWithPersona(entities[i].persona.value);
                int mana = 0;
                foreach (CharacterEntity gn in array)
                {
                    if (gn.hasIntellect)
                    {
                        mana += (gn.intellect.value * _lvlBonus);
                    }
                }
                entities[i].mana.maxValue = mana;
                if (entities[i].mana.value > entities[i].mana.maxValue)
                {
                    entities[i].mana.value -= (entities[i].mana.value - entities[i].mana.maxValue);
                }
                else if (entities[i].mana.value == 0)
                {
                    entities[i].mana.value = mana;
                }
                entities[i].isManaUpdate = false;
            }
        }
    }
}