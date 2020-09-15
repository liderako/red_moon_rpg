﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;

namespace RedMoonRPG.Systems.Battle.AI
{
    public class DecisionMakingSystem : ReactiveSystem<BattleEntity>
    {
        public DecisionMakingSystem(Contexts contexts) : base(contexts.battle)
        {
        }

        protected override ICollector<BattleEntity> GetTrigger(IContext<BattleEntity> context)
        {
            return context.CreateCollector(BattleMatcher.ActiveAvatar);
        }

        protected override bool Filter(BattleEntity entity)
        {
            return entity.isBattle && entity.isAI && entity.activeAvatar.value;
        }

        protected override void Execute(List<BattleEntity> entities)
        {
            if (entities.Count > 1)
            {
                Debug.LogError("DecisionMakingSystem error");
                return;
            }

            BattleEntity avatar = entities[0];
            List<BattleEntity> listEnemy = FindEnemies(avatar);
            Debug.Log("My target near unit is " + listEnemy[FindNearEnemy(listEnemy, avatar)].name.name);
            Debug.Log("My target far unit is " + listEnemy[FindFarEnemy(listEnemy, avatar)].name.name);
            Debug.Log("My target with min hp is " + listEnemy[FindEnemyWithMinHP(listEnemy)].name.name);
            Debug.Log("My target with max hp is " + listEnemy[FindEnemyWithMaxHP(listEnemy)].name.name);
            // 1. Шаг первый оценить текущих противников
            // МассивВрагов массивВрагов = ОценкаВсехВрагов(avatar)
            // 2. Шаг второй выбрать приоритетное состояние
            // состояние AI = ОценкаСостояние(Аватар, массивВрагов)
        }

        private List<BattleEntity> FindEnemies(BattleEntity avatar)
        {
            FactionEntity factionsManager = Contexts.sharedInstance.faction.GetEntityWithName(Tags.factionData);
            FactionStruct ownerFaction = new FactionStruct();
            int len = factionsManager.factions.value.Count;
            for (int i = 0; i < len; i++)
            {
                if (factionsManager.factions.value[i].name == avatar.typeFaction.value)
                {
                    ownerFaction = factionsManager.factions.value[i];
                    break;
                }
            }

            List<BattleEntity> listEnemies = new List<BattleEntity>();
            for (int i = 0; i < len; i++)
            {
                if (ownerFaction.relations[factionsManager.factions.value[i].name] == false)
                {
                    HashSet<BattleEntity> enemiesAvatar =
                        Contexts.sharedInstance.battle.GetEntitiesWithTypeFaction(
                            factionsManager.factions.value[i].name);
                    foreach (var enemy in enemiesAvatar)
                    {
                        listEnemies.Add(enemy);
                    }
                }
            }
            return listEnemies;
        }

        private int FindEnemyWithMinHP(List<BattleEntity> enemies)
        {
            int len = enemies.Count;
            int minHp = 2147483647;
            int minIterator = 0;
            int hp;
            for (int i = 0; i < len; i++)
            {
                hp = Contexts.sharedInstance.game.GetEntityWithName(enemies[i].name.name).health.value;
                if (hp < minHp)
                {
                    minHp = hp;
                    minIterator = i;
                }
            }
            return minIterator;
        }
        
        private int FindEnemyWithMaxHP(List<BattleEntity> enemies)
        {
            int len = enemies.Count;
            int maxHp = 0;
            int maxIterator = 0;
            int hp;
            for (int i = 0; i < len; i++)
            {
                hp = Contexts.sharedInstance.game.GetEntityWithName(enemies[i].name.name).health.value;
                if (hp > maxHp)
                {
                    maxHp = hp;
                    maxIterator = i;
                }
            }
            return maxIterator;
        }

        private int FindNearEnemy(List<BattleEntity> enemies, BattleEntity avatar)
        {
            int len = enemies.Count;
            float minDistance = 2147483647;
            int minIterator = 0;
            float distance;
            for (int i = 0; i < len; i++)
            {
                distance = Vector3.Distance(enemies[i].mapPosition.value.vector, avatar.mapPosition.value.vector);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    minIterator = i;
                }
            }
            return minIterator;
        }
        
        private int FindFarEnemy(List<BattleEntity> enemies, BattleEntity avatar)
        {
            int len = enemies.Count;
            float maxDistance = 0;
            int maxIterator = 0;
            float distance;
            for (int i = 0; i < len; i++)
            {
                distance = Vector3.Distance(enemies[i].mapPosition.value.vector, avatar.mapPosition.value.vector);
                if (distance > maxDistance)
                {
                    maxDistance = distance;
                    maxIterator = i;
                }
            }
            return maxIterator;
        }
    }

    /*
        Dictionary<int, Враг> ОценкаВсехВрагов(BattleEntity avatar)
        {
            Фильтр Поиска цели = взять фильтр из avatar
            Массив врагов для текущего персонажа = вызвать метод для получение всех врагов данного аватара
            Пройтись по массиву и взависимости от фильтра поставить оценку приоритетности в словаре
            Dictionary<int, Враг> словарь = новый Словарь();
            for (int i = 0; i < массивВрагов.Длина; i++
            {
                словавь[i] = враг
            }
            вернуть словарь
        }
    */
        
        /*
         
         СостояниеAI ОценкаСостояние(Аватар аватар, Dictionary<int, Враг> dictionary)
         {
            СостояниеAI стейт;
            
            Фильтр приоритетности состояний = аватар дай мне фильтр.
            
            цикл (пока фильтр состояний не закончен)
            {
                if ()
                {
                
                }
            }
            
            return стейт;
         }
         
         */
}