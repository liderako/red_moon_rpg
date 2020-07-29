using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;

namespace RedMoonRPG.Systems.Animations
{
    /*
     *  Система для переключение анимаций буловским способом
     */
    public class BoolAnimationSystem : ReactiveSystem<GameEntity>
    {
        public BoolAnimationSystem(Contexts contexts) : base(contexts.game)
        {
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.NextAnimation);
        }

        protected override bool Filter(GameEntity entity)
        {
            return entity.hasAnimator && entity.hasActiveAnimation && entity.hasNextAnimation;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            for (int i = 0; i < entities.Count; i++)
            {
                GameEntity entity = entities[i];
                entity.animator.value.SetBool(entity.activeAnimation.name, false);
                entity.animator.value.SetBool(entity.nextAnimation.name, true);
                entity.ReplaceActiveAnimation(entity.nextAnimation.name);
                entity.RemoveNextAnimation();
            }
        }
    }
}