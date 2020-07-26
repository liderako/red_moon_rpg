using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;
using RedMoonRPG.Settings;

namespace RedMoonRPG.InitializeSystems
{
    public class LevelInitEntitySystem : ReactiveSystem<GameEntity>
    {
        private Contexts _contexts;

        public LevelInitEntitySystem(Contexts contexts) : base(contexts.game)
        {
            _contexts = contexts;
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.LevelCreate);
        }
        protected override bool Filter(GameEntity entity)
        {
            return entity.isLevelCreate;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            GameEntity level = Contexts.sharedInstance.game.GetEntityWithName(Tags.level);
            LevelSettings levelSettings = GameData.Instance.LevelSettings;
            if (level.hasLimitMap)
            {
                level.ReplaceLimitMap(levelSettings.axisX, levelSettings.axisY, levelSettings.axisZ);
            }
            else
            {
                level.AddLimitMap(levelSettings.axisX, levelSettings.axisY, levelSettings.axisZ);
            }
            entities[0].isLevelCreate = false;
        }
    }
}