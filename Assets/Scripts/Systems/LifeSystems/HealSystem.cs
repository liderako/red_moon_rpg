using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;

/*
** Системя для получение лечение и увеличение значение хп.
** в теории можно использовать и для прочности предметов.
*/

namespace RedMoonRPG.Systems.Life
{
    public class HealSystem : ReactiveSystem<GameEntity>
    {
        
        public HealSystem(Contexts contexts) : base(contexts.game)
        {
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.Healed);
        }

        protected override bool Filter(GameEntity entity)
        {
            return entity.hasHealed && entity.hasHealth;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            int len = entities.Count;
            for (int i = 0; i < entities.Count; i++)
            {
                entities[i].health.value = entities[i].health.value + entities[i].healed.value;
                entities[i].RemoveHealed();
            }
        }
    }
}