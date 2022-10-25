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
    public class LevelInitEntitySystem : IInitializeSystem
    {
        public void Initialize()
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
        }
    }
}