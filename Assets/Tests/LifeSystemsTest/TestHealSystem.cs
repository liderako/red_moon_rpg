#if UNITY_TEST
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class TestHealSystem
    {
        [UnityTest]
        public IEnumerator TestEatSystemBaseInit()
        {
            Contexts contexts = Contexts.sharedInstance;
            Entitas.Systems systems = new Feature("Game")
                .Add(new RedMoonRPG.Systems.Life.HealSystem(contexts));
            systems.Initialize();
            GameEntity entity = contexts.game.CreateEntity();
            entity.AddHealth(50, 100);
            entity.AddHealed(0);
            yield return new WaitForSeconds(0.1f);
            systems.Execute();
            Assert.IsTrue(!entity.hasHealed);
            systems.DeactivateReactiveSystems();
            contexts.Reset();
        }


        [UnityTest]
        public IEnumerator TestEatSystemBaseHealCase()
        {
            Contexts contexts = Contexts.sharedInstance;
            Entitas.Systems systems = new Feature("Game")
                .Add(new RedMoonRPG.Systems.Life.HealSystem(contexts));
            systems.Initialize();
            GameEntity entity = contexts.game.CreateEntity();
            entity.AddHealth(50, 100);
            entity.AddHealed(25);
            yield return new WaitForSeconds(0.1f);
            systems.Execute();
            Assert.AreEqual(entity.health.value, 75);
            Assert.IsTrue(!entity.hasHealed);
            systems.DeactivateReactiveSystems();
            contexts.Reset();
        }

        [UnityTest]
        public IEnumerator TestEatSystemBaseHealMaxCase()
        {
            Contexts contexts = Contexts.sharedInstance;
            Entitas.Systems systems = new Feature("Game")
                .Add(new RedMoonRPG.Systems.Life.HealSystem(contexts));
            systems.Initialize();
            GameEntity entity = contexts.game.CreateEntity();
            entity.AddHealth(50, 100);
            entity.AddHealed(75);
            yield return new WaitForSeconds(0.1f);
            systems.Execute();
            Assert.AreEqual(entity.health.value, 100);
            Assert.IsTrue(!entity.hasHealed);
            systems.DeactivateReactiveSystems();
            contexts.Reset();
        }
    }
}
#endif