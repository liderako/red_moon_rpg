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
    public class UpdateMagicDamageSystem : ReactiveSystem<GameEntity>
    {
        private Contexts _contexts;
        private int _lvlBonus;
        private float _range;

        public UpdateMagicDamageSystem(Contexts contexts, GameBalanceSettings gmt) : base(contexts.game)
        {
            _contexts = contexts;
            _lvlBonus = gmt.MagicDamageForIntellect;
            _range = gmt.RangeMagicDamage;
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.UpdateMagicDamage);
        }

        /*
        ** Система будет вызиваться только один раз на персонажа благодаря тому
        ** что Имя может быть только у одного Entity на персонажа
        */
        protected override bool Filter(GameEntity entity)
        {
            return entity.hasName && entity.isUpdateMagicDamage && entity.hasPersona;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            int len = entities.Count;
            for (int i = 0; i < len; i++)
            {
                HashSet<GameEntity> array = _contexts.game.GetEntitiesWithPersona(entities[i].persona.value);
                int amount = 0;
                int minAmount = 0;
                foreach (GameEntity gn in array)
                {
                    if (gn.hasIntellect) // если это стата персонажа или баф от шмоток или тд зайдет сюда
                    {
                        amount += (gn.intellect.value * _lvlBonus); // подсчитываем бонусный урон от всего интелекта персонажа
                    }
                    if (gn.hasIndexMagicDamage && !gn.hasName) // если это оружие то зайдет сюда
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