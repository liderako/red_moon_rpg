#if UNITY_TEST
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using RedMoonRPG.Settings;

namespace Tests
{
    public class TestUpdateRangedDamageSystem
    {
        public TestUpdateRangedDamageSystem()
        {
            if (GameData.Instance != null) return;
            GameObject go = new GameObject();
            go.AddComponent<GameData>();
        }

        /*
         * Проверяем базовую инициализацию без бонусов
         */
        [UnityTest]
        public IEnumerator TestUpdateIndexRangedDamageBaseInit()
        {
            yield return new WaitForSeconds(0.1f);
            Contexts contexts = Contexts.sharedInstance;
            Entitas.Systems systems = new Feature("Game")
                .Add(new RedMoonRPG.Systems.UpdateRangedDamageSystem(contexts, GameData.Instance.GameBalanceSettings));
            systems.Initialize();
            GameEntity entity = contexts.game.CreateEntity();
            entity.AddName("TestUpdateIndexRangedDamageBaseInit");
            entity.AddPersona("TestUpdateIndexRangedDamageBaseInit");
            entity.AddDexterity(0);
            entity.AddIndexRangedDamage(0, 0);
            entity.isUpdateRangedDamage = true;
            yield return new WaitForSeconds(0.1f);
            systems.Execute();
            Assert.IsTrue(!entity.isUpdateRangedDamage);
            systems.DeactivateReactiveSystems();
            entity.Destroy();
            contexts.Reset();
        }
        ///*
        // * Проверяем индекс максимального и минимального физического урона без доп бонусов
        // */
        [UnityTest]
        public IEnumerator TestUpdateIndexRangedDamageWithoutWeapon()
        {
            yield return new WaitForSeconds(0.1f);
            Contexts contexts = Contexts.sharedInstance;
            Entitas.Systems systems = new Feature("Game")
                .Add(new RedMoonRPG.Systems.UpdateRangedDamageSystem(contexts, GameData.Instance.GameBalanceSettings));
            systems.Initialize();
            GameEntity entity = contexts.game.CreateEntity();
            entity.AddName("TestUpdateIndexRangedDamageWithoutWeapon");
            entity.AddPersona("TestUpdateIndexRangedDamageWithoutWeapon");
            entity.AddDexterity(5);
            entity.AddIndexRangedDamage(0, 0);
            entity.isUpdateRangedDamage = true;
            yield return new WaitForSeconds(0.1f);
            systems.Execute();
            int amount = (entity.dexterity.value * GameData.Instance.GameBalanceSettings.RangedDamageForDexterity);
            Assert.AreEqual(entity.indexRangedDamage.maxValue, amount);
            Assert.AreEqual(entity.indexRangedDamage.minValue,
                amount -
                (int)(amount * GameData.Instance.GameBalanceSettings.RangeRangedDamage));
            systems.DeactivateReactiveSystems();
            entity.Destroy();
            contexts.Reset();
        }

        ///*
        // * Проверяем индекс максимального и минимального урона в дальнем бою c бонусом от оружия
        // */
        [UnityTest]
        public IEnumerator TestUpdateIndexRanngedDamageWithWeapon()
        {
            yield return new WaitForSeconds(0.1f);
            Contexts contexts = Contexts.sharedInstance;
            Entitas.Systems systems = new Feature("Game")
                .Add(new RedMoonRPG.Systems.UpdateRangedDamageSystem(contexts, GameData.Instance.GameBalanceSettings));
            systems.Initialize();
            GameEntity entity = contexts.game.CreateEntity();
            entity.AddName("TestUpdateIndexRanngedDamageWithWeapon");
            entity.AddPersona("TestUpdateIndexRanngedDamageWithWeapon");
            entity.AddDexterity(5);
            entity.AddIndexRangedDamage(0, 0);
            entity.isUpdateRangedDamage = true;
            GameEntity entity_weapon = contexts.game.CreateEntity();
            entity_weapon.AddPersona("TestUpdateIndexRanngedDamageWithWeapon");
            entity_weapon.AddIndexRangedDamage(3, 9);
            yield return new WaitForSeconds(0.1f);
            systems.Execute();
            int amount = (entity.dexterity.value * GameData.Instance.GameBalanceSettings.RangedDamageForDexterity) + entity_weapon.indexRangedDamage.maxValue;
            Assert.AreEqual(entity.indexRangedDamage.maxValue, amount);
            Assert.AreEqual(entity.indexRangedDamage.minValue,
                amount -
                (int)(amount * GameData.Instance.GameBalanceSettings.RangeRangedDamage) + entity_weapon.indexRangedDamage.minValue);
            systems.DeactivateReactiveSystems();
            entity.Destroy();
            contexts.Reset();
        }
    }
}
#endif