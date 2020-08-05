using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class TestTimeSystem
    {
        [UnityTest]
        public IEnumerator TestTimeSystemOneMinute()
        {
            Contexts contexts = Contexts.sharedInstance;
            Entitas.Systems systems = new Feature("Time")
                .Add(new RedMoonRPG.Systems.TimeG.TimeSystem(contexts));
            TimeEntity time = contexts.time.CreateEntity();
            time.AddName("Time");
            time.AddMinute(0);
            time.AddHour(0);
            time.AddSecond(0);
            time.AddDay(0);
            time.AddSpeedTime(10);
            systems.Initialize();
            int i = 0;
            while (i < 6)
            {
                yield return new WaitForSeconds(0.001f);
                systems.Execute();
                i++;
            }
            Assert.AreEqual(time.second.value, 0);
            Assert.AreEqual(time.minute.value, 1);
            systems.DeactivateReactiveSystems();
            contexts.Reset();
        }

        [UnityTest]
        public IEnumerator TestTimeSystemOneHour()
        {
            Contexts contexts = Contexts.sharedInstance;
            Entitas.Systems systems = new Feature("Time")
                .Add(new RedMoonRPG.Systems.TimeG.TimeSystem(contexts));
            TimeEntity time = contexts.time.CreateEntity();
            time.AddName("Time");
            time.AddMinute(0);
            time.AddHour(0);
            time.AddSecond(0);
            time.AddDay(0);
            time.AddSpeedTime(60);
            systems.Initialize();
            int i = 0;
            while (i < 60)
            {
                yield return new WaitForSeconds(0.0001f);
                systems.Execute();
                i++;
            }
            Assert.AreEqual(time.second.value, 0);
            Assert.AreEqual(time.minute.value, 0);
            Assert.AreEqual(time.hour.value, 1);
            systems.DeactivateReactiveSystems();
            contexts.Reset();
        }

        [UnityTest]
        public IEnumerator TestTimeSystemOneDay()
        {
            Contexts contexts = Contexts.sharedInstance;
            Entitas.Systems systems = new Feature("Time")
                .Add(new RedMoonRPG.Systems.TimeG.TimeSystem(contexts));
            TimeEntity time = contexts.time.CreateEntity();
            time.AddName("Time");
            time.AddMinute(0);
            time.AddHour(0);
            time.AddSecond(0);
            time.AddDay(0);
            time.AddSpeedTime(3600);
            systems.Initialize();
            int i = 0;
            while (i < 24)
            {
                yield return new WaitForSeconds(0.0001f);
                systems.Execute();
                i++;
            }
            Assert.AreEqual(time.day.value, 1);
            Assert.AreEqual(time.hour.value, 0);
            Assert.AreEqual(time.minute.value, 0);
            Assert.AreEqual(time.second.value, 0);
            systems.DeactivateReactiveSystems();
            contexts.Reset();
        }

        [UnityTest]
        public IEnumerator TestTimeSystemHalfDay()
        {
            Contexts contexts = Contexts.sharedInstance;
            Entitas.Systems systems = new Feature("Time")
                .Add(new RedMoonRPG.Systems.TimeG.TimeSystem(contexts));
            TimeEntity time = contexts.time.CreateEntity();
            time.AddName("Time");
            time.AddMinute(0);
            time.AddHour(0);
            time.AddSecond(0);
            time.AddDay(0);
            time.AddSpeedTime(21600);
            systems.Initialize();
            int i = 0;
            while (i < 2)
            {
                yield return new WaitForSeconds(0.0001f);
                systems.Execute();
                i++;
            }
            Assert.AreEqual(time.day.value, 0);
            Assert.AreEqual(time.hour.value, 12);
            Assert.AreEqual(time.minute.value, 0);
            Assert.AreEqual(time.second.value, 0);
            systems.DeactivateReactiveSystems();
            contexts.Reset();
        }
    }
}
