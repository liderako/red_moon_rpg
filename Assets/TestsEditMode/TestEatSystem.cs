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
            //Contexts contexts = Contexts.sharedInstance;
            //Entitas.Systems systems = new Feature("Game")
            //    .Add(new RedMoonRPG.Systems.Life.EatSystem(contexts));
            //GameEntity entity = contexts.game.CreateEntity();
            //entity.AddHunger(0, 100);
            //entity.AddCalory(10);
            //systems.Initialize();
            yield return null;
            //Assert.AreEqual(entity.hunger.value, 0);
            ////Assert.IsTrue(!entity.hasCalory);
            //systems.Execute();
            //entity.AddCalory(10);
            //Assert.IsTrue(!entity.hasCalory);
        }
    }
}
