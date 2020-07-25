using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;

/*
** Система обновляен данные про урон в ближнем бою для игрового персонажа
** собирая данные со всех бафоф и артефактов
*/
namespace RedMoonRPG.Systems
{
    public class UpdateMeleeDamageSystem : ReactiveSystem<GameEntity>
    {
        private Contexts _contexts;
        private int _lvlBonus;
        private float _range;

        public UpdateMeleeDamageSystem(Contexts contexts, GameBalanceSettings gmt) : base(contexts.game)
        {
            _contexts = contexts;
            _lvlBonus = gmt.MeleeDamageForStrength;
            _range = gmt.RangeMeleeDamage;
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.UpdateMeleeDamage);
        }

        /*
        ** Система будет вызиваться только один раз на персонажа благодаря тому
        ** что Имя может быть только у одного Entity на персонажа
        */
        protected override bool Filter(GameEntity entity)
        {
            return entity.hasName && entity.isUpdateMeleeDamage && entity.hasPersona;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            GameContext g = _contexts.game;
            int len = entities.Count;
            for (int i = 0; i < entities.Count; i++)
            {
                HashSet<GameEntity> array = _contexts.game.GetEntitiesWithPersona(entities[i].persona.value);
                int amount = 0;
                int minAmount = 0;
                foreach (GameEntity gn in array)
                {
                    if (gn.hasStrength) // если это стата персонажа или баф от шмоток или тд зайдет сюда
                    {
                        amount += (gn.strength.value * _lvlBonus); // подсчитываем бонусный урон от всей силы персонажа
                    }
                    if (gn.hasIndexMeleeDamage && !gn.hasName) // если это оружие то зайдет сюда
                    {
                        amount += (gn.indexMeleeDamage.maxValue); // бонус от оружия к максимальному урону
                        minAmount += (gn.indexMeleeDamage.minValue); // бонус от оружия к минимальному урону
                    }
                }
                entities[i].indexMeleeDamage.maxValue = amount;
                entities[i].indexMeleeDamage.minValue = amount - (int)(amount * _range) + minAmount;
            }
        }
    }
}