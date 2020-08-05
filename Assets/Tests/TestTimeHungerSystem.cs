using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class TestHungerTimeSystem
    {

        [UnityTest]
        public IEnumerator TestTimeSystemOneHour()
        {
            Contexts contexts = Contexts.sharedInstance;
            Entitas.Systems systems = new Feature("Time")
                .Add(new RedMoonRPG.Systems.TimeG.TimeSystem(contexts))
                .Add(new RedMoonRPG.Systems.TimeG.TimeHungredSystem(contexts));
            TimeEntity time = contexts.time.CreateEntity();
            time.AddName("Time");
            time.AddMinute(0);
            time.AddHour(0);
            time.AddSecond(0);
            time.AddDay(0);
            time.AddSpeedTime(60);
            systems.Initialize();


            Contexts contextsLife = Contexts.sharedInstance;
            Entitas.Systems systemsLife = new Feature("Life")
            .Add(new RedMoonRPG.Systems.Life.EatSystem(contexts));
            LifeEntity entityPlayer = contextsLife.life.CreateEntity();
            entityPlayer.AddHunger(-50, 100);
            systemsLife.Initialize();

            int i = 0;
            while (i < 60)
            {
                yield return new WaitForSeconds(0.0001f);
                systems.Execute();
                systemsLife.Execute();
                i++;
            }
            Assert.AreEqual(entityPlayer.hunger.value, 50);
            Assert.AreEqual(time.hour.value, 1);
            systems.DeactivateReactiveSystems();
            systemsLife.DeactivateReactiveSystems();
            contexts.Reset();
            contextsLife.Reset();
        }

        [UnityTest]
        public IEnumerator TestTimeSystemTwoHour()
        {
            Contexts contexts = Contexts.sharedInstance;
            Entitas.Systems systems = new Feature("Time")
                .Add(new RedMoonRPG.Systems.TimeG.TimeSystem(contexts))
                .Add(new RedMoonRPG.Systems.TimeG.TimeHungredSystem(contexts));
            TimeEntity time = contexts.time.CreateEntity();
            time.AddName("Time");
            time.AddMinute(0);
            time.AddHour(0);
            time.AddSecond(0);
            time.AddDay(0);
            time.AddSpeedTime(60);
            systems.Initialize();


            Contexts contextsLife = Contexts.sharedInstance;
            Entitas.Systems systemsLife = new Feature("Life")
            .Add(new RedMoonRPG.Systems.Life.EatSystem(contexts));
            LifeEntity entityPlayer = contextsLife.life.CreateEntity();
            entityPlayer.AddHunger(-50, 100);
            systemsLife.Initialize();

            int i = 0;
            while (i < 120)
            {
                yield return new WaitForSeconds(0.0001f);
                systems.Execute();
                systemsLife.Execute();
                i++;
            }
            Assert.AreEqual(entityPlayer.hunger.value, 100);
            Assert.AreEqual(time.hour.value, 2);
            systems.DeactivateReactiveSystems();
            systemsLife.DeactivateReactiveSystems();
            contexts.Reset();
            contextsLife.Reset();
        }
    }
}
