using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;

/*
** Системя для получение урона и уменьшение хп
*/

namespace RedMoonRPG.Systems.Life
{
    public class DamageSystem : ReactiveSystem<GameEntity>
    {
        
        public DamageSystem(Contexts contexts) : base(contexts.game)
        {
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.Damaged);
        }

        protected override bool Filter(GameEntity entity)
        {
            return entity.hasDamaged && entity.hasHealth;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            int len = entities.Count;
            for (int i = 0; i < entities.Count; i++)
            {
                entities[i].health.value = entities[i].health.value - entities[i].damaged.value;
                if (entities[i].health.value < 0)
                {
                    entities[i].health.value = 0;
                }
                entities[i].RemoveDamaged();
            }
        }
    }
}
