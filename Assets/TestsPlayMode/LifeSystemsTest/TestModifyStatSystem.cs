using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using RedMoonRPG.Systems;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class TestModifyStatSystem
    {
        [UnityTest]
        public IEnumerator TestModifyStatSystemBaseInit()
        {
            Contexts contexts = Contexts.sharedInstance;
            Entitas.Systems systems = new Feature("Game")
                .Add(new ModifyStatSystem(contexts));
            systems.Initialize();
            GameEntity entity = contexts.game.CreateEntity();
            entity.AddModifiedStat(Stat.Dexterity, 0);
            entity.AddDexterity(0);
            yield return new WaitForSeconds(0.1f);
            systems.Execute();
            Assert.IsTrue(!entity.hasModifiedStat);
            systems.DeactivateReactiveSystems();
            contexts.Reset();
        }

        [UnityTest]
        public IEnumerator TestModifyStatSystemBaseChangeDexterity()
        {
            Contexts contexts = Contexts.sharedInstance;
            Entitas.Systems systems = new Feature("Game")
                .Add(new ModifyStatSystem(contexts));
            systems.Initialize();
            GameEntity entity = contexts.game.CreateEntity();
            entity.AddModifiedStat(Stat.Dexterity, 4);
            entity.AddDexterity(5);
            yield return new WaitForSeconds(0.1f);
            systems.Execute();
            Assert.AreEqual(entity.dexterity.value, 4);
            systems.DeactivateReactiveSystems();
            contexts.Reset();
        }

        [UnityTest]
        public IEnumerator TestModifyStatSystemBaseChangeStrength()
        {
            Contexts contexts = Contexts.sharedInstance;
            Entitas.Systems systems = new Feature("Game")
                .Add(new ModifyStatSystem(contexts));
            systems.Initialize();
            GameEntity entity = contexts.game.CreateEntity();
            entity.AddModifiedStat(Stat.Strength, 4);
            entity.AddStrength(5);
            yield return new WaitForSeconds(0.1f);
            systems.Execute();
            Assert.AreEqual(entity.strength.value, 4);
            systems.DeactivateReactiveSystems();
            contexts.Reset();
        }

        [UnityTest]
        public IEnumerator TestModifyStatSystemBaseChangeIntellect()
        {
            Contexts contexts = Contexts.sharedInstance;
            Entitas.Systems systems = new Feature("Game")
                .Add(new ModifyStatSystem(contexts));
            systems.Initialize();
            GameEntity entity = contexts.game.CreateEntity();
            entity.AddModifiedStat(Stat.Intellect, 4);
            entity.AddIntellect(5);
            yield return new WaitForSeconds(0.1f);
            systems.Execute();
            Assert.AreEqual(entity.intellect.value, 4);
            systems.DeactivateReactiveSystems();
            contexts.Reset();
        }

        [UnityTest]
        public IEnumerator TestModifyStatSystemBaseChangeLuck()
        {
            Contexts contexts = Contexts.sharedInstance;
            Entitas.Systems systems = new Feature("Game")
                .Add(new ModifyStatSystem(contexts));
            systems.Initialize();
            GameEntity entity = contexts.game.CreateEntity();
            entity.AddModifiedStat(Stat.Luck, 4);
            entity.AddLuck(5);
            yield return new WaitForSeconds(0.1f);
            systems.Execute();
            Assert.AreEqual(entity.luck.value, 4);
            systems.DeactivateReactiveSystems();
            contexts.Reset();
        }

        [UnityTest]
        public IEnumerator TestModifyStatSystemBaseChangePersonality()
        {
            Contexts contexts = Contexts.sharedInstance;
            Entitas.Systems systems = new Feature("Game")
                .Add(new ModifyStatSystem(contexts));
            systems.Initialize();
            GameEntity entity = contexts.game.CreateEntity();
            entity.AddModifiedStat(Stat.Personality, 4);
            entity.AddPersonality(5);
            yield return new WaitForSeconds(0.1f);
            systems.Execute();
            Assert.AreEqual(entity.personality.value, 4);
            systems.DeactivateReactiveSystems();
            contexts.Reset();
        }

        [UnityTest]
        public IEnumerator TestModifyStatSystemBaseChangeAttention()
        {
            Contexts contexts = Contexts.sharedInstance;
            Entitas.Systems systems = new Feature("Game")
                .Add(new ModifyStatSystem(contexts));
            systems.Initialize();
            GameEntity entity = contexts.game.CreateEntity();
            entity.AddModifiedStat(Stat.Attention, 4);
            entity.AddAttention(5);
            yield return new WaitForSeconds(0.1f);
            systems.Execute();
            Assert.AreEqual(entity.attention.value, 4);
            systems.DeactivateReactiveSystems();
            contexts.Reset();
        }

        [UnityTest]
        public IEnumerator TestModifyStatSystemBaseChangeEndurance()
        {
            Contexts contexts = Contexts.sharedInstance;
            Entitas.Systems systems = new Feature("Game")
                .Add(new ModifyStatSystem(contexts));
            systems.Initialize();
            GameEntity entity = contexts.game.CreateEntity();
            entity.AddModifiedStat(Stat.Endurance, 4);
            entity.AddEndurance(5);
            yield return new WaitForSeconds(0.1f);
            systems.Execute();
            Assert.AreEqual(entity.endurance.value, 4);
            systems.DeactivateReactiveSystems();
            contexts.Reset();
        }
    }
}
