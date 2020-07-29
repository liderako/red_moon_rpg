﻿using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class TestDrinkSystem
    {
        [UnityTest]
        public IEnumerator TestDrinkSystemBaseInit()
        {
            Contexts contexts = Contexts.sharedInstance;
            Entitas.Systems systems = new Feature("Game")
                .Add(new RedMoonRPG.Systems.Life.DrinkSystem(contexts));
            GameEntity entity = contexts.game.CreateEntity();
            entity.AddThirst(0, 100);
            systems.Initialize();
            entity.AddWater(10);
            yield return new WaitForSeconds(0.1f);
            systems.Execute();
            Assert.IsTrue(!entity.hasWater);
            systems.DeactivateReactiveSystems();
            contexts.Reset();
        }

        [UnityTest]
        public IEnumerator TestEatSystemAteAlittle()
        {
            Contexts contexts = Contexts.sharedInstance;
            Entitas.Systems systems = new Feature("Game")
                .Add(new RedMoonRPG.Systems.Life.DrinkSystem(contexts));
            GameEntity entity = contexts.game.CreateEntity();
            entity.AddThirst(50, 100);
            systems.Initialize();
            entity.AddWater(20);
            yield return new WaitForSeconds(0.1f);
            systems.Execute();
            Assert.AreEqual(entity.thirst.value, 30);
            Assert.IsTrue(!entity.hasWater);
            systems.DeactivateReactiveSystems();
            contexts.Reset();
        }

        [UnityTest]
        public IEnumerator TestEatSystemNegativeCase()
        {
            Contexts contexts = Contexts.sharedInstance;
            Entitas.Systems systems = new Feature("Game")
                .Add(new RedMoonRPG.Systems.Life.DrinkSystem(contexts));
            GameEntity entity = contexts.game.CreateEntity();
            entity.AddThirst(50, 100);
            systems.Initialize();
            entity.AddWater(90);
            yield return new WaitForSeconds(0.1f);
            systems.Execute();
            Assert.AreEqual(entity.thirst.value, 0);
            Assert.IsTrue(!entity.hasWater);
            systems.DeactivateReactiveSystems();
            contexts.Reset();
        }
    }
}
