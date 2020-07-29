using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;

namespace RedMoonRPG.Systems.Life
{    /*
     * Система для понижение уровня жажды
     * Вызивается когда персонаж выпьет воду.
     * Когда персонаж выпьет воду будет вешаться компонент воды на персонажа.
     */
    public class DrinkSystem : ReactiveSystem<GameEntity>
    {

        public DrinkSystem(Contexts contexts) : base(contexts.game)
        {
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.Water);
        }

        protected override bool Filter(GameEntity entity)
        {
            return entity.hasWater && entity.hasThirst;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            int len = entities.Count;
            for (int i = 0; i < entities.Count; i++)
            {
                entities[i].thirst.value -= entities[i].water.value;
                if (entities[i].thirst.value < 0)
                {
                    entities[i].thirst.value = 0;
                }
                else if (entities[i].thirst.value > entities[i].thirst.maxValue)
                {
                    entities[i].thirst.value = entities[i].thirst.maxValue;
                }
                entities[i].RemoveWater();
            }
        }
    }
}