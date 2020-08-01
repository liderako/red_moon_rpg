using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using RedMoonRPG.Settings;

namespace Tests
{
    public class TestUpdateHealthSystem
    {
        public TestUpdateHealthSystem()
        {
            if (GameData.Instance != null) return;
            GameObject go = new GameObject();
            go.AddComponent<GameData>();
        }

        /*
         * Проверяеем базовую инициализацию хп
         */
        [UnityTest]
        public IEnumerator TestUpdateHealthSystemBaseInit()
        {
            yield return new WaitForSeconds(0.1f);
            Contexts contexts = Contexts.sharedInstance;
            Entitas.Systems systems = new Feature("Game")
                .Add(new RedMoonRPG.Systems.UpdateHealthSystem(contexts, GameData.Instance.GameBalanceSettings));
            systems.Initialize();
            GameEntity entity = contexts.game.CreateEntity();
            entity.AddName("Asvirido");
            entity.AddPersona("Asvirido");
            entity.AddHealth(100, 100);
            entity.isHealthUpdate = true;
            yield return new WaitForSeconds(0.1f);
            systems.Execute();
            Assert.IsTrue(!entity.isHealthUpdate);
            systems.DeactivateReactiveSystems();
            entity.Destroy();
            contexts.Reset();
        }

        /*
         *  Проверяем базовое влияение выносливости на хп
         */
        [UnityTest]
        public IEnumerator TestUpdateHealthSystemBaseInitMaxHpTest()
        {
            yield return new WaitForSeconds(0.1f);
            Contexts contexts = Contexts.sharedInstance;
            Entitas.Systems systems = new Feature("Game")
                .Add(new RedMoonRPG.Systems.UpdateHealthSystem(contexts, GameData.Instance.GameBalanceSettings));
            systems.Initialize();
            GameEntity entity = contexts.game.CreateEntity();
            entity.AddName("TestUpdateHealthSystemBaseInitMaxHpTest");
            entity.AddPersona("TestUpdateHealthSystemBaseInitMaxHpTest");
            entity.AddEndurance(5);
            entity.AddHealth(0, 0);

            entity.isHealthUpdate = true;
            yield return new WaitForSeconds(0.1f);
            systems.Execute();
            Assert.IsTrue(!entity.isHealthUpdate);
            Assert.AreEqual(entity.health.maxValue, entity.endurance.value * GameData.Instance.GameBalanceSettings.HpForLevelEndurance);
            Assert.AreEqual(entity.health.value, entity.health.maxValue);
            systems.DeactivateReactiveSystems();
            contexts.Reset();
        }


        /*
         * Проверяем базовое влияние выносливости + бонусы от других сущностей на максимальное значение хп персонажа 
         */
        [UnityTest]
        public IEnumerator TestUpdateHealthSystemBaseBonusMaxHpTest()
        {
            yield return new WaitForSeconds(0.1f);
            Contexts contexts = Contexts.sharedInstance;
            Entitas.Systems systems = new Feature("Game")
                .Add(new RedMoonRPG.Systems.UpdateHealthSystem(contexts, GameData.Instance.GameBalanceSettings));
            systems.Initialize();
            GameEntity entity = contexts.game.CreateEntity();
            entity.AddName("TestUpdateHealthSystemBaseBonusMaxHpTest");
            entity.AddPersona("TestUpdateHealthSystemBaseBonusMaxHpTest");
            entity.AddEndurance(5);
            entity.AddHealth(0, 0);

            GameEntity entity_stamina = contexts.game.CreateEntity();
            entity_stamina.AddEndurance(5);
            entity_stamina.AddPersona("TestUpdateHealthSystemBaseBonusMaxHpTest");
            entity.isHealthUpdate = true;
            yield return new WaitForSeconds(0.1f);
            systems.Execute();
            Assert.IsTrue(!entity.isHealthUpdate);
            Assert.AreEqual(entity.health.maxValue, (entity.endurance.value + entity_stamina.endurance.value) * GameData.Instance.GameBalanceSettings.HpForLevelEndurance);
            systems.DeactivateReactiveSystems();
            contexts.Reset();
        }


        /*
         * Проверяем базовое влияние дебафа выносливости на максимальное значение хп 
         */
        [UnityTest]
        public IEnumerator TestUpdateHealthSystemBaseDebufTest()
        {
            yield return new WaitForSeconds(0.1f);
            Contexts contexts = Contexts.sharedInstance;
            Entitas.Systems systems = new Feature("Game")
                .Add(new RedMoonRPG.Systems.UpdateHealthSystem(contexts, GameData.Instance.GameBalanceSettings));
            systems.Initialize();
            GameEntity entity = contexts.game.CreateEntity();
            entity.AddName("TestUpdateHealthSystemBaseDebufTest");
            entity.AddPersona("TestUpdateHealthSystemBaseDebufTest");
            entity.AddEndurance(5);
            entity.AddHealth(0, 0);

            GameEntity entity_stamina = contexts.game.CreateEntity();
            entity_stamina.AddEndurance(5);
            entity_stamina.AddPersona("TestUpdateHealthSystemBaseDebufTest");
            entity.isHealthUpdate = true;

            GameEntity entity_debuf = contexts.game.CreateEntity();
            entity_debuf.AddEndurance(-1);
            entity_debuf.AddPersona("TestUpdateHealthSystemBaseDebufTest");
            yield return new WaitForSeconds(0.1f);
            systems.Execute();
            Assert.IsTrue(!entity.isHealthUpdate);
            Assert.AreEqual(
                entity.health.maxValue,
                (entity.endurance.value + entity_stamina.endurance.value + entity_debuf.endurance.value) *
                GameData.Instance.GameBalanceSettings.HpForLevelEndurance);
            systems.DeactivateReactiveSystems();
            contexts.Reset();
        }
    }
}
