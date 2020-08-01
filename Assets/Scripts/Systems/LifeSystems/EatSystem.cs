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
    public class EatSystem : ReactiveSystem<LifeEntity>
    {

        public EatSystem(Contexts contexts) : base(contexts.life)
        {
        }

        protected override ICollector<LifeEntity> GetTrigger(IContext<LifeEntity> context)
        {
            return context.CreateCollector(LifeMatcher.Calory);
        }

        protected override bool Filter(LifeEntity entity)
        {
            return entity.hasCalory && entity.hasHunger;
        }

        protected override void Execute(List<LifeEntity> entities)
        {
            int len = entities.Count;
            for (int i = 0; i < len; i++)
            {
                entities[i].hunger.value -= entities[i].calory.value;
                if (entities[i].hunger.value < 0)
                {
                    entities[i].hunger.value = 0;
                }
                else if (entities[i].hunger.value > entities[i].hunger.maxValue)
                {
                    entities[i].hunger.value = entities[i].hunger.maxValue;
                }
                entities[i].RemoveCalory();
            }
        }
    }
}