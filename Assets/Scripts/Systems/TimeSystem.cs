using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;
using Entitas.CodeGeneration.Attributes;

namespace RedMoonRPG.Systems.TimeG
{
    /*
    ** Система для течение времени
    */
    public class TimeSystem : IExecuteSystem
    {
        private Contexts _contexts;

        public TimeSystem(Contexts contexts)
        {
            _contexts = contexts;
        }

        public void Execute()
        {
            TimeEntity entity = _contexts.time.GetEntityWithName("Time");
            entity.ReplaceSecond(entity.second.value + entity.speedTime.value);
            if (entity.second.value >= 60)
            {
                entity.ReplaceMinute((int)(entity.minute.value + (entity.second.value / 60)));
                entity.ReplaceSecond(0);
            }
            if (entity.minute.value >= 60)
            {
                entity.ReplaceHour((int)(entity.hour.value + (entity.minute.value / 60)));
                entity.ReplaceMinute(0);
            }
            if (entity.hour.value >= 24)
            {
                entity.ReplaceDay((int)entity.day.value + 1);
                entity.ReplaceHour(0);
            }
        }
    }
}