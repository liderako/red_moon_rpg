using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using RedMoonRPG.Settings;

namespace Tests
{
    public class TestUpdateManaSystem
    {
        public TestUpdateManaSystem()
        {
            if (GameData.Instance == null)
            {
                GameObject go = new GameObject();
                go.AddComponent<GameData>();
            }
        }

        /*
         * В этом тесте проверяем базовую инициализацию маны
         */
        [UnityTest]
        public IEnumerator TestUpdateManaSystemBaseInit()
        {
            yield return new WaitForSeconds(0.1f);
            Contexts contexts = Contexts.sharedInstance;
            Entitas.Systems systems = new Feature("Game")
                .Add(new RedMoonRPG.Systems.UpdateManaSystem(contexts, GameData.Instance.GameBalanceSettings));
            systems.Initialize();
            GameEntity entity = contexts.game.CreateEntity();
            entity.AddName("TestUpdateManaSystemBaseInit");
            entity.AddPersona("TestUpdateManaSystemBaseInit");
            entity.AddMana(0, 0);
            entity.isManaUpdate = true;
            yield return new WaitForSeconds(0.1f);
            systems.Execute();
            Assert.IsTrue(!entity.isManaUpdate);
            systems.DeactivateReactiveSystems();
            entity.Destroy();
            contexts.Reset();
        }

        /*
         * В этом тесте проверяем базовую инициализацию маны
         * Смотрим правильно ли работает установка максимального значение маны без доп бонусов в других сущностей
         */
        [UnityTest]
        public IEnumerator TestUpdateManaSystemBaseInitMaxManaTest()
        {
            yield return new WaitForSeconds(0.1f);
            Contexts contexts = Contexts.sharedInstance;
            Entitas.Systems systems = new Feature("Game")
                .Add(new RedMoonRPG.Systems.UpdateManaSystem(contexts, GameData.Instance.GameBalanceSettings));
            systems.Initialize();
            GameEntity entity = contexts.game.CreateEntity();
            entity.AddName("TestUpdateManaSystemBaseInitMaxManaTest");
            entity.AddPersona("TestUpdateManaSystemBaseInitMaxManaTest");
            entity.AddIntellect(5);
            entity.AddMana(0, 0);

            entity.isManaUpdate = true;
            yield return new WaitForSeconds(0.1f);
            systems.Execute();
            Assert.IsTrue(!entity.isManaUpdate);
            Assert.AreEqual(entity.mana.maxValue, entity.intellect.value * GameData.Instance.GameBalanceSettings.ManaForLevelIntellect);
            Assert.AreEqual(entity.mana.value, entity.mana.maxValue);
            systems.DeactivateReactiveSystems();
            contexts.Reset();
        }

        /*
         * В этом тесте проверяем влияют ли дополнительные сущности на максимальное значение
         */
        [UnityTest]
        public IEnumerator TestUpdateManaSystemBaseBonusMaxHpTest()
        {
            yield return new WaitForSeconds(0.1f);
            Contexts contexts = Contexts.sharedInstance;
            Entitas.Systems systems = new Feature("Game")
                .Add(new RedMoonRPG.Systems.UpdateManaSystem(contexts, GameData.Instance.GameBalanceSettings));
            systems.Initialize();
            GameEntity entity = contexts.game.CreateEntity();
            entity.AddName("TestUpdateManaSystemBaseBonusMaxHpTest");
            entity.AddPersona("TestUpdateManaSystemBaseBonusMaxHpTest");
            entity.AddIntellect(5);
            entity.AddMana(0, 0);

            entity.isManaUpdate = true;
            GameEntity entity_bonus = contexts.game.CreateEntity();
            entity_bonus.AddPersona("TestUpdateManaSystemBaseBonusMaxHpTest");
            entity_bonus.AddIntellect(5);
            yield return new WaitForSeconds(0.1f);
            systems.Execute();
            Assert.IsTrue(!entity.isManaUpdate);
            Assert.AreEqual(entity.mana.maxValue,
                (entity.intellect.value + entity.intellect.value) 
                * GameData.Instance.GameBalanceSettings.ManaForLevelIntellect);
            systems.DeactivateReactiveSystems();
            contexts.Reset();
        }

        /*
         * Проверяем работает ли логика дебафов на интелект
         */
        [UnityTest]
        public IEnumerator TestUpdateManaSystemBaseDebufTest()
        {
            yield return new WaitForSeconds(0.1f);
            Contexts contexts = Contexts.sharedInstance;
            Entitas.Systems systems = new Feature("Game")
                .Add(new RedMoonRPG.Systems.UpdateManaSystem(contexts, GameData.Instance.GameBalanceSettings));
            systems.Initialize();
            GameEntity entity = contexts.game.CreateEntity();
            entity.AddName("TestUpdateManaSystemBaseBonusMaxHpTest");
            entity.AddPersona("TestUpdateManaSystemBaseBonusMaxHpTest");
            entity.AddIntellect(5);
            entity.AddMana(0, 0);

            entity.isManaUpdate = true;
            GameEntity entity_bonus = contexts.game.CreateEntity();
            entity_bonus.AddPersona("TestUpdateManaSystemBaseBonusMaxHpTest");
            entity_bonus.AddIntellect(5);

            GameEntity entity_debuf = contexts.game.CreateEntity();
            entity_debuf.AddPersona("TestUpdateManaSystemBaseBonusMaxHpTest");
            entity_debuf.AddIntellect(-2);

            yield return new WaitForSeconds(0.1f);
            systems.Execute();
            Assert.IsTrue(!entity.isManaUpdate);
            Assert.AreEqual(entity.mana.maxValue,
                (entity.intellect.value + entity.intellect.value + entity_debuf.intellect.value)
                * GameData.Instance.GameBalanceSettings.ManaForLevelIntellect);
            systems.DeactivateReactiveSystems();
            contexts.Reset();
        }
    }
}
