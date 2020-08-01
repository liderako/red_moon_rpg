#if UNITY_TEST
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using RedMoonRPG.Settings;

namespace Tests
{
    public class TestUpdateMagicDamageSystem
    {
        public TestUpdateMagicDamageSystem()
        {
            if (GameData.Instance != null) return;
            GameObject go = new GameObject();
            go.AddComponent<GameData>();
        }

        /*
         * Проверяем базовую инициализацию без бонусов
         */
        [UnityTest]
        public IEnumerator TestUpdateIndexMagicDamageBaseInit()
        {
            yield return new WaitForSeconds(0.1f);
            Contexts contexts = Contexts.sharedInstance;
            Entitas.Systems systems = new Feature("Game")
                .Add(new RedMoonRPG.Systems.UpdateMagicDamageSystem(contexts, GameData.Instance.GameBalanceSettings));
            systems.Initialize();
            GameEntity entity = contexts.game.CreateEntity();
            entity.AddName("TestUpdateIndexMagicDamageBaseInit");
            entity.AddPersona("TestUpdateIndexMagicDamageBaseInit");
            entity.AddIntellect(0);
            entity.AddIndexMagicDamage(0, 0);
            entity.isUpdateMagicDamage = true;
            yield return new WaitForSeconds(0.1f);
            systems.Execute();
            Assert.IsTrue(!entity.isUpdateMagicDamage);
            systems.DeactivateReactiveSystems();
            entity.Destroy();
            contexts.Reset();
        }

        /*
         * Проверяем индекс максимального и минимального магического урона без доп бонусов
         */
        [UnityTest]
        public IEnumerator TestUpdateIndexMagicDamageWithoutWeapon()
        {
            yield return new WaitForSeconds(0.1f);
            Contexts contexts = Contexts.sharedInstance;
            Entitas.Systems systems = new Feature("Game")
                .Add(new RedMoonRPG.Systems.UpdateMagicDamageSystem(contexts, GameData.Instance.GameBalanceSettings));
            systems.Initialize();
            GameEntity entity = contexts.game.CreateEntity();
            entity.AddName("TestUpdateIndexMagicDamageWithoutWeapon");
            entity.AddPersona("TestUpdateIndexMagicDamageWithoutWeapon");
            entity.AddIntellect(5);
            entity.AddIndexMagicDamage(0, 0);
            entity.isUpdateMagicDamage = true;
            yield return new WaitForSeconds(0.1f);
            systems.Execute();
            int amount = (entity.intellect.value * GameData.Instance.GameBalanceSettings.MagicDamageForIntellect);
            // ОбщийУрон - (int)(ОбщийУрон * _range) + минимальныйБонусОружия формула любого типа урона
            Assert.AreEqual(entity.indexMagicDamage.maxValue, amount);
            Assert.AreEqual(entity.indexMagicDamage.minValue,
                amount -
                (int)(amount * GameData.Instance.GameBalanceSettings.RangeMagicDamage));
            systems.DeactivateReactiveSystems();
            entity.Destroy();
            contexts.Reset();
        }

        /*
         * Проверяем индекс максимального и минимального магического урона c бонусом от оружия
         */
        [UnityTest]
        public IEnumerator TestUpdateIndexMagicDamageWithWeapon()
        {
            yield return new WaitForSeconds(0.1f);
            Contexts contexts = Contexts.sharedInstance;
            Entitas.Systems systems = new Feature("Game")
                .Add(new RedMoonRPG.Systems.UpdateMagicDamageSystem(contexts, GameData.Instance.GameBalanceSettings));
            systems.Initialize();
            GameEntity entity = contexts.game.CreateEntity();
            entity.AddName("TestUpdateIndexMagicDamageWithWeapon");
            entity.AddPersona("TestUpdateIndexMagicDamageWithWeapon");
            entity.AddIntellect(5);
            entity.AddIndexMagicDamage(0, 0);
            entity.isUpdateMagicDamage = true;

            GameEntity entity_weapon = contexts.game.CreateEntity();
            entity_weapon.AddPersona("TestUpdateIndexMagicDamageWithWeapon");
            entity_weapon.AddIndexMagicDamage(3, 9);
            yield return new WaitForSeconds(0.1f);
            systems.Execute();
            // ОбщийУрон - (int)(ОбщийУрон * _range) + минимальныйБонусОружия формула любого типа урона
            int amount = (entity.intellect.value * GameData.Instance.GameBalanceSettings.MagicDamageForIntellect);
            amount += entity_weapon.indexMagicDamage.maxValue;
            Assert.AreEqual(entity.indexMagicDamage.maxValue, amount);
            Assert.AreEqual(entity.indexMagicDamage.minValue,
                amount -
                (int)(amount * GameData.Instance.GameBalanceSettings.RangeMagicDamage) + entity_weapon.indexMagicDamage.minValue);
            systems.DeactivateReactiveSystems();
            entity.Destroy();
            contexts.Reset();
        }
    }
}
#endif