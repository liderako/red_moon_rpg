﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;

namespace RedMoonRPG.Systems.TimeG
{
    /*
     * Система для реагирование состояние тела персонажа на время и увеличение уровня голода
     */
    public class TimeHungredSystem : ReactiveSystem<TimeEntity>
    {
        public TimeHungredSystem(Contexts contexts) : base(contexts.time)
        {
        }

        protected override ICollector<TimeEntity> GetTrigger(IContext<TimeEntity> context)
        {
            return context.CreateCollector(TimeMatcher.Hour);
        }

        protected override bool Filter(TimeEntity entity)
        {
            return entity.hasHour;
        }

        protected override void Execute(List<TimeEntity> entities)
        {
            LifeEntity[] array = Contexts.sharedInstance.life.GetEntities(LifeMatcher.Hunger);
            for (int i = 0; i < array.Length; i++)
            {
                array[i].AddCalory(-50);
            }
        }
    }
}