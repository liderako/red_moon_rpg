using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class TestEatSystem
    {
        [UnityTest]
        public IEnumerator TestEatSystemBaseInit()
        {
            Contexts contexts = Contexts.sharedInstance;
            Entitas.Systems systems = new Feature("Game")
                .Add(new RedMoonRPG.Systems.Life.EatSystem(contexts));
            GameEntity entity = contexts.game.CreateEntity();
            entity.AddHunger(0, 100);
            systems.Initialize();
            entity.AddCalory(10);
            Assert.AreEqual(entity.hunger.value, 0);
            yield return new WaitForSeconds(0.1f);
            systems.Execute();
            Assert.IsTrue(!entity.hasCalory);
            systems.DeactivateReactiveSystems();
            contexts.Reset();
        }

        [UnityTest]
        public IEnumerator TestEatSystemAteAlittle()
        {
            Contexts contexts = Contexts.sharedInstance;
            Entitas.Systems systems = new Feature("Game")
                .Add(new RedMoonRPG.Systems.Life.EatSystem(contexts));
            GameEntity entity = contexts.game.CreateEntity();
            entity.AddHunger(50, 100);
            entity.AddCalory(25);
            systems.Initialize();
            yield return new WaitForSeconds(0.1f);
            systems.Execute();
            Assert.IsTrue(!entity.hasCalory);
            systems.DeactivateReactiveSystems();
            contexts.Reset();
        }

        [UnityTest]
        public IEnumerator TestEatSystemNegativeCase()
        {
            Contexts contexts = Contexts.sharedInstance;
            Entitas.Systems systems = new Feature("Game")
                .Add(new RedMoonRPG.Systems.Life.EatSystem(contexts));
            GameEntity entity = contexts.game.CreateEntity();
            entity.AddHunger(50, 100);
            entity.AddCalory(75);
            systems.Initialize();
            yield return new WaitForSeconds(0.1f);
            systems.Execute();
            Assert.AreEqual(entity.hunger.value, 0);
            Assert.IsTrue(!entity.hasCalory);
            systems.DeactivateReactiveSystems();
            contexts.Reset();
        }
        [UnityTest]
        public IEnumerator TestEatSystemIncreaseHungerLevelCase()
        {
            Contexts contexts = Contexts.sharedInstance;
            Entitas.Systems systems = new Feature("Game")
                .Add(new RedMoonRPG.Systems.Life.EatSystem(contexts));
            GameEntity entity = contexts.game.CreateEntity();
            entity.AddHunger(50, 100);
            entity.AddCalory(-25);
            systems.Initialize();
            yield return new WaitForSeconds(0.1f);
            systems.Execute();
            Assert.AreEqual(entity.hunger.value, 75);
            Assert.IsTrue(!entity.hasCalory);
            systems.DeactivateReactiveSystems();
            contexts.Reset();
        }
        [UnityTest]
        public IEnumerator TestEatSystemMaxIncreaseHungerLevelCase()
        {
            Contexts contexts = Contexts.sharedInstance;
            Entitas.Systems systems = new Feature("Game")
                .Add(new RedMoonRPG.Systems.Life.EatSystem(contexts));
            GameEntity entity = contexts.game.CreateEntity();
            entity.AddHunger(50, 100);
            entity.AddCalory(-125);
            systems.Initialize();
            yield return new WaitForSeconds(0.1f);
            systems.Execute();
            Assert.AreEqual(entity.hunger.value, 100);
            Assert.IsTrue(!entity.hasCalory);
            systems.DeactivateReactiveSystems();
            contexts.Reset();
        }
    }
}
