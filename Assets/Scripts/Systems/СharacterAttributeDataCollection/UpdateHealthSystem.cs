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
    public class UpdateHealthSystem : ReactiveSystem<GameEntity>
    {
        private Contexts _contexts;
        private int _lvlBonus;

        public UpdateHealthSystem(Contexts contexts, GameBalanceSettings gmt) : base(contexts.game)
        {
            _contexts = contexts;
            _lvlBonus = gmt.RangedDamageForDexterity;
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.HealthUpdate);
        }

        protected override bool Filter(GameEntity entity)
        {
            return entity.hasName && entity.isHealthUpdate && entity.hasPersona;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            GameContext g = _contexts.game;
            int len = entities.Count;
            for (int i = 0; i < entities.Count; i++)
            {
                HashSet<GameEntity> array = _contexts.game.GetEntitiesWithPersona(entities[i].persona.value);
                int hp = 0;
                foreach (GameEntity gn in array)
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