using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;

/*
** Система обновляен данные про магический урон для игрового персонажа
** собирая данные со всех бафоф и артефактов
*/
namespace RedMoonRPG.Systems
{
    public class UpdateMagicDamageSystem : ReactiveSystem<CharacterEntity>
    {
        private Contexts _contexts;
        private int _lvlBonus;
        private float _range;
    
        public UpdateMagicDamageSystem(Contexts contexts, GameBalanceSettings gmt) : base(contexts.character)
        {
            _contexts = contexts;
            _lvlBonus = gmt.MagicDamageForIntellect;
            _range = gmt.RangeMagicDamage;
        }
    
        protected override ICollector<CharacterEntity> GetTrigger(IContext<CharacterEntity> context)
        {
            return context.CreateCollector(CharacterMatcher.UpdateMagicDamage);
        }
    
        /*
        ** Система будет вызиваться только один раз на персонажа благодаря тому
        ** что Имя может быть только у одного Entity на персонажа
        */
        protected override bool Filter(CharacterEntity entity)
        {
            return entity.isUpdateMagicDamage && entity.hasPersona;
        }
    
        protected override void Execute(List<CharacterEntity> entities)
        {
            int len = entities.Count;
            for (int i = 0; i < len; i++)
            {
                HashSet<CharacterEntity> array = _contexts.character.GetEntitiesWithPersona(entities[i].persona.value);
                int amount = 0;
                int minAmount = 0;
                foreach (CharacterEntity gn in array)
                {
                    if (gn.hasIntellect) // если это стата персонажа или баф от шмоток или тд зайдет сюда
                    {
                        amount += (gn.intellect.value * _lvlBonus); // подсчитываем бонусный урон от всего интелекта персонажа
                    }
                    if (gn.hasIndexMagicDamage && !gn.hasNameItem) // если это итем то зайдет сюда
                    {
                        amount += (gn.indexMagicDamage.maxValue); // бонус от оружия к максимальному урону
                        minAmount += (gn.indexMagicDamage.minValue); // бонус от оружия к минимальному урону
                    }
                }
                entities[i].indexMagicDamage.maxValue = amount;
                entities[i].indexMagicDamage.minValue = amount - (int)(amount * _range) + minAmount;
                entities[i].isUpdateMagicDamage = false;
            }
        }
    }
}