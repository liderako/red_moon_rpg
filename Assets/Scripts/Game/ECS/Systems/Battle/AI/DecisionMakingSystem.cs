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
            return entity.isAI && entity.activeAvatar.value;
        }

        protected override void Execute(List<BattleEntity> entities)
        {
            if (entities.Count > 1)
            {
                Debug.LogError("DecisionMakingSystem error");
                return;
            }
            BattleEntity avatar = entities[0];
            // 1. Шаг первый оценить текущих противников
            // МассивВрагов массивВрагов = ОценкаВсехВрагов(avatar)
            // 2. Шаг второй выбрать приоритетное состояние
            // состояние AI = ОценкаСостояние(Аватар, массивВрагов)
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