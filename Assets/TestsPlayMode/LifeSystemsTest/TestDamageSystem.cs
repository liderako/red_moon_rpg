using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class TestDamageSystem
    {
        [UnityTest]
        public IEnumerator TestEatSystemBaseInit()
        {
            Contexts contexts = Contexts.sharedInstance;
            Entitas.Systems systems = new Feature("Game")
                .Add(new RedMoonRPG.Systems.Life.DamageSystem(contexts));
            systems.Initialize();
            GameEntity entity = contexts.game.CreateEntity();
            entity.AddHealth(50, 100);
            entity.AddDamaged(0);
            yield return new WaitForSeconds(0.1f);
            systems.Execute();
            Assert.IsTrue(!entity.hasDamaged);
            systems.DeactivateReactiveSystems();
            contexts.Reset();
        }


        [UnityTest]
        public IEnumerator TestEatSystemBaseDamageCase()
        {
            Contexts contexts = Contexts.sharedInstance;
            Entitas.Systems systems = new Feature("Game")
                .Add(new RedMoonRPG.Systems.Life.DamageSystem(contexts));
            systems.Initialize();
            GameEntity entity = contexts.game.CreateEntity();
            entity.AddHealth(50, 100);
            entity.AddDamaged(14);
            yield return new WaitForSeconds(0.1f);
            systems.Execute();
            Assert.AreEqual(entity.health.value, 36);
            Assert.IsTrue(!entity.hasDamaged);
            systems.DeactivateReactiveSystems();
            contexts.Reset();
        }

        [UnityTest]
        public IEnumerator TestEatSystemBaseDamageZeroCase()
        {
            Contexts contexts = Contexts.sharedInstance;
            Entitas.Systems systems = new Feature("Game")
                .Add(new RedMoonRPG.Systems.Life.DamageSystem(contexts));
            systems.Initialize();
            GameEntity entity = contexts.game.CreateEntity();
            entity.AddHealth(50, 100);
            entity.AddDamaged(75);
            yield return new WaitForSeconds(0.1f);
            systems.Execute();
            Assert.AreEqual(entity.health.value, 0);
            Assert.IsTrue(!entity.hasDamaged);
            systems.DeactivateReactiveSystems();
            contexts.Reset();
        }
    }
}
