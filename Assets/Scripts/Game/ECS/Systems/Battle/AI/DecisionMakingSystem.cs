using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;
using RedMoonRPG.Systems.Battle.Grid;
using TGS;

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
            Debug.Log("Desicion");
            if (entities.Count > 1)
            {
                Debug.LogError("DecisionMakingSystem error");
                return;
            }
            BattleEntity avatar = entities[0];
            if (!IsAvailabeBaseAttack(avatar))
            {
                Debug.Log("doesn't have action point for attack");
                Contexts.sharedInstance.battle.GetEntityWithName(Tags.battleManagerEntity).isSkipTurn = true;
                return;
            }
            if (avatar.hasTargetEnemy)
            {
                if (CheckRadiusForAttack(avatar.terrainGrid.value, avatar.mapPosition.value.vector, avatar.targetEnemy.value.mapPosition.value.vector, avatar.radiusAttack.value))
                {
                    avatar.isEstimateInflictedDamage = true;
                    avatar.ReplaceTargetEnemy(avatar.targetEnemy.value);
                    avatar.isAttack = true;
                    return;
                }
            }
            BattleEntity targetEnemy = GetTargetEnemy(FindEnemies(avatar), avatar);
            Cell cell = avatar.terrainGrid.value.CellGetAtPosition(avatar.mapPosition.value.vector, true);
            Cell cellEnemy = targetEnemy.terrainGrid.value.CellGetAtPosition(targetEnemy.mapPosition.value.vector, true);
            targetEnemy.terrainGrid.value.CellSetCanCross(targetEnemy.terrainGrid.value.CellGetIndex(cellEnemy), true);
            avatar.terrainGrid.value.CellSetCanCross(avatar.terrainGrid.value.CellGetIndex(cell), true);
            if (CheckRadiusForAttack(avatar.terrainGrid.value, avatar.mapPosition.value.vector, targetEnemy.mapPosition.value.vector, avatar.radiusAttack.value))
            {
                avatar.isEstimateInflictedDamage = true;
                avatar.AddTargetEnemy(targetEnemy);
                avatar.isAttack = true;
                avatar.terrainGrid.value.CellSetCanCross(avatar.terrainGrid.value.CellGetIndex(cell), false);
            }
            else
            {
                List<int> path = GetPathForNearPositionForAttack(avatar, targetEnemy);
                GameEntity unit = Contexts.sharedInstance.game.GetEntityWithName(avatar.name.name);
                avatar.ReplacePath(path, 0);
                avatar.ReplaceMapPosition(avatar.mapPosition.value);
                if (!unit.hasNextAnimation)
                {
                    unit.AddNextAnimation(AnimationTags.run);
                }
                else
                {
                    unit.ReplaceNextAnimation(AnimationTags.run);
                }
            }
            targetEnemy.terrainGrid.value.CellSetCanCross(targetEnemy.terrainGrid.value.CellGetIndex(cellEnemy), false);
        }

        private bool IsAvailabeBaseAttack(BattleEntity avatar)
        {
            int point = avatar.typeAttack.value[AnimationTags.SwordAttack];
            if (point <= avatar.actionPoint.value)
            {
                return true;
            }
            // HashSet<CharacterEntity> array = Contexts.sharedInstance.character.GetEntitiesWithPersona(avatar.name.name);
            // foreach (CharacterEntity item in array)
            // {
            //     if (item.hasActionPoint && item.hasNameItem)
            //     {
            //         if (item.actionPoint.value <= avatar.actionPoint.value)
            //         {
            //             Debug.Log("Return TRUE");
            //             return true;
            //         }
            //         return false;
            //     }
            // }
            return false;
        }

        // метод взависимости от фильтра врага возвращает приоритетную цель из списка врагов
        private BattleEntity GetTargetEnemy(List<BattleEntity> listEnemy, BattleEntity avatar)
        {
            // Debug.Log("My target near unit is " + listEnemy[FindNearEnemy(listEnemy, avatar)].name.name);
            // Debug.Log("My target far unit is " + listEnemy[FindFarEnemy(listEnemy, avatar)].name.name);
            // Debug.Log("My target with min hp is " + listEnemy[FindEnemyWithMinHP(listEnemy)].name.name);
            // Debug.Log("My target with max hp is " + listEnemy[FindEnemyWithMaxHP(listEnemy)].name.name);
            return listEnemy[FindNearEnemy(listEnemy, avatar)];
        }
    
        private bool CheckRadiusForAttack(TerrainGridSystem tgs, Vector3 a, Vector3 b, float radius)
        {
            if (Vector3.Distance(a, b) <= radius * 2)
            {
                return true;
                // Cell cellA = tgs.CellGetAtPosition(a, true);
                // Cell cellB = tgs.CellGetAtPosition(b, true);
                // List<int> tmp = tgs.FindPath(tgs.CellGetIndex(cellA), tgs.CellGetIndex(cellB), (int)radius, 0, -1);
                // if (tmp != null)
                // {
                //     return true;
                // }
            }
            return false;
        }
        
        private List<int> GetPathForNearPositionForAttack(BattleEntity avatar, BattleEntity enemy)
        {
            TerrainGridSystem tgs = avatar.terrainGrid.value;
            int endCell = tgs.CellGetIndex(tgs.CellGetAtPosition(enemy.mapPosition.value.vector, true));
            int startCell = tgs.CellGetIndex(tgs.CellGetAtPosition(avatar.mapPosition.value.vector, true));
            List<int> moveList = tgs.FindPath(startCell, endCell, 0, 0, -1);
            int len = moveList.Count;
            int point = avatar.actionPoint.value;
            List<int> path = new List<int>();
            for (int i = 0; i < len - 1; i++)
            {
                if (point == 0) break;
                path.Add((moveList[i]));
                point--;
            }
            if (path.Count != 0)
            {
                avatar.ReplaceActionPoint(point);
                return path;
            }
            Debug.LogError(avatar.name.name + ": I can't find near position for attack. It's a system error.");
            return null;
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
                hp = Contexts.sharedInstance.character.GetEntityWithName(enemies[i].name.name).health.value;
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
            int maxHp = 0
            ;
            int maxIterator = 0;
            int hp;
            for (int i = 0; i < len; i++)
            {
                hp = Contexts.sharedInstance.character.GetEntityWithName(enemies[i].name.name).health.value;
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
}