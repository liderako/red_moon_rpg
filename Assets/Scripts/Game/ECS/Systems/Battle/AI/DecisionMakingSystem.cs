using System.Collections;
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
            FindEnemies(avatar);
            // 1. Шаг первый оценить текущих противников
            // МассивВрагов массивВрагов = ОценкаВсехВрагов(avatar)
            // 2. Шаг второй выбрать приоритетное состояние
            // состояние AI = ОценкаСостояние(Аватар, массивВрагов)
        }

        public void FindEnemies(BattleEntity avatar)
        {
            FactionEntity factionsManager = Contexts.sharedInstance.faction.GetEntityWithName(Tags.factionData);
            FactionStruct ownerFaction = new FactionStruct();
            for (int i = 0; i < factionsManager.factions.value.Count; i++)
            {
                if (factionsManager.factions.value[i].name == avatar.typeFaction.value)
                {
                    ownerFaction = factionsManager.factions.value[i];
                    break;
                }
            }
            List<BattleEntity> listEnemies = new List<BattleEntity>();
            for (int i = 0; i < factionsManager.factions.value.Count; i++)
            {
                if (ownerFaction.relations[factionsManager.factions.value[i].name] == false)
                {
                    HashSet<BattleEntity> enemiesAvatar = Contexts.sharedInstance.battle.GetEntitiesWithTypeFaction(factionsManager.factions.value[i].name);
                    foreach (var enemy in enemiesAvatar)
                    {
                        listEnemies.Add(enemy);
                    }
                }
            }
            Debug.Log("Enemy for " + avatar.name.name + " is ->");
            for (int i = 0; i < listEnemies.Count; i++)
            {
                Debug.Log(listEnemies[i].name.name);
            }
            Debug.Log("_______________________________");
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
}