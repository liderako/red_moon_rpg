#if UNITY_TEST
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using RedMoonRPG.Settings;

namespace Tests
{
    public class TestUpdateMeleeDamageSystem
    {
        public TestUpdateMeleeDamageSystem()
        {
            if (GameData.Instance != null) return;
            GameObject go = new GameObject();
            go.AddComponent<GameData>();
        }

        /*
         * Проверяем базовую инициализацию без бонусов
         */
        [UnityTest]
        public IEnumerator TestUpdateIndexMeeleDamageBaseInit()
        {
            yield return new WaitForSeconds(0.1f);
            Contexts contexts = Contexts.sharedInstance;
            Entitas.Systems systems = new Feature("Game")
                .Add(new RedMoonRPG.Systems.UpdateMeleeDamageSystem(contexts, GameData.Instance.GameBalanceSettings));
            systems.Initialize();
            GameEntity entity = contexts.game.CreateEntity();
            entity.AddName("TestUpdateIndexMeeleDamageBaseInit");
            entity.AddPersona("TestUpdateIndexMeeleDamageBaseInit");
            entity.AddStrength(0);
            entity.AddIndexMeleeDamage(0, 0);
            entity.isUpdateMeleeDamage = true;
            yield return new WaitForSeconds(0.1f);
            systems.Execute();
            Assert.IsTrue(!entity.isUpdateMeleeDamage);
            systems.DeactivateReactiveSystems();
            entity.Destroy();
            contexts.Reset();
        }

        ///*
        // * Проверяем индекс максимального и минимального физического урона без доп бонусов
        // */
        [UnityTest]
        public IEnumerator TestUpdateIndexMeeleDamageWithoutWeapon()
        {
            yield return new WaitForSeconds(0.1f);
            Contexts contexts = Contexts.sharedInstance;
            Entitas.Systems systems = new Feature("Game")
                .Add(new RedMoonRPG.Systems.UpdateMeleeDamageSystem(contexts, GameData.Instance.GameBalanceSettings));
            systems.Initialize();
            GameEntity entity = contexts.game.CreateEntity();
            entity.AddName("TestUpdateIndexMeeleDamageWithoutWeapon");
            entity.AddPersona("TestUpdateIndexMeeleDamageWithoutWeapon");
            entity.AddStrength(5);
            entity.AddIndexMeleeDamage(0, 0);
            entity.isUpdateMeleeDamage = true;
            yield return new WaitForSeconds(0.1f);
            systems.Execute();
            int amount = (entity.strength.value * GameData.Instance.GameBalanceSettings.MeleeDamageForStrength);
            Assert.AreEqual(entity.indexMeleeDamage.maxValue, amount);
            Assert.AreEqual(entity.indexMeleeDamage.minValue,
                amount -
                (int)(amount * GameData.Instance.GameBalanceSettings.RangeMeleeDamage));
            systems.DeactivateReactiveSystems();
            entity.Destroy();
            contexts.Reset();
        }

        ///*
        // * Проверяем индекс максимального и минимального физического урона без доп бонусов
        // */
        [UnityTest]
        public IEnumerator TestUpdateIndexMeeleDamageWithWeapon()
        {
            yield return new WaitForSeconds(0.1f);
            Contexts contexts = Contexts.sharedInstance;
            Entitas.Systems systems = new Feature("Game")
                .Add(new RedMoonRPG.Systems.UpdateMeleeDamageSystem(contexts, GameData.Instance.GameBalanceSettings));
            systems.Initialize();
            GameEntity entity = contexts.game.CreateEntity();
            entity.AddName("TestUpdateIndexMeeleDamageWithWeapon");
            entity.AddPersona("TestUpdateIndexMeeleDamageWithWeapon");
            entity.AddStrength(5);
            entity.AddIndexMeleeDamage(0, 0);
            entity.isUpdateMeleeDamage = true;

            GameEntity entity_weapon = contexts.game.CreateEntity();
            entity_weapon.AddPersona("TestUpdateIndexMeeleDamageWithWeapon");
            entity_weapon.AddIndexMeleeDamage(3, 9);
            yield return new WaitForSeconds(0.1f);
            systems.Execute();
            int amount = (entity.strength.value * GameData.Instance.GameBalanceSettings.MeleeDamageForStrength) + entity_weapon.indexMeleeDamage.maxValue;
            Assert.AreEqual(entity.indexMeleeDamage.maxValue, amount);
            Assert.AreEqual(entity.indexMeleeDamage.minValue,
                amount -
                (int)(amount * GameData.Instance.GameBalanceSettings.RangeMeleeDamage) + entity_weapon.indexMeleeDamage.minValue);
            systems.DeactivateReactiveSystems();
            entity.Destroy();
            contexts.Reset();
        }
    }
}
#endif