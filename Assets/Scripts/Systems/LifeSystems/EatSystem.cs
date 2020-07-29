using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;

namespace RedMoonRPG.Systems.Life
{
    /*
     * Система для понижение уровня голода
     * Вызываться будет после тогоо как персонаж что-то сьест.
     * Для вызова нужно накинуть компонент калорий на персонажа.
    */
    public class EatSystem : ReactiveSystem<GameEntity>
    {

        public EatSystem(Contexts contexts) : base(contexts.game)
        {
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.Calory);
        }

        protected override bool Filter(GameEntity entity)
        {
            return entity.hasCalory && entity.hasHunger;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            int len = entities.Count;
            for (int i = 0; i < len; i++)
            {
                entities[i].hunger.value -= entities[i].calory.value;
                if (entities[i].hunger.value < 0)
                {
                    entities[i].hunger.value = 0;
                }
                entities[i].RemoveCalory();
            }
        }
    }
}