using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;
using RedMoonRPG.Settings;

namespace RedMoonRPG.Systems.InitializeSystems
{
    /*
     * Система для инициализации сущности уровня и подгрузки параметров уровня
     */
    public class LevelInitEntitySystem : ReactiveSystem<GameEntity>
    {

        public LevelInitEntitySystem(Contexts contexts) : base(contexts.game)
        {
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