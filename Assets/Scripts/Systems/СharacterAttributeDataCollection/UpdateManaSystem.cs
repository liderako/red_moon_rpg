using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;
using RedMoonRPG.Settings;

/*
** Система обновляет данные про максимальное значение маны у персонажа
** собирая данные со всех бафоф и артефактов
*/
namespace RedMoonRPG.Systems
{
    public class UpdateManaSystem : ReactiveSystem<GameEntity>
    {
        private Contexts _contexts;
        private int _lvlBonus;

        public UpdateManaSystem(Contexts contexts, GameBalanceSettings gmt) : base(contexts.game)
        {
            _contexts = contexts;
            _lvlBonus = gmt.ManaForLevelIntellect;
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.ManaUpdate);
        }

        /*
        ** Система будет вызиваться только один раз на персонажа благодаря тому
        ** что Имя может быть только у одного Entity на персонажа
        */
        protected override bool Filter(GameEntity entity)
        {
            return entity.hasName && entity.isManaUpdate && entity.hasPersona;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            GameContext g = _contexts.game;
            int len = entities.Count;
            for (int i = 0; i < entities.Count; i++)
            {
                HashSet<GameEntity> array = _contexts.game.GetEntitiesWithPersona(entities[i].persona.value);
                int amount = 0;
                foreach (GameEntity gn in array)
                {
                    if (gn.hasIntellect)
                    {
                        amount += (gn.intellect.value * _lvlBonus);
                    }
                }
                entities[i].mana.maxValue = amount;
                if (entities[i].mana.value > entities[i].mana.maxValue)
                {
                    entities[i].mana.value -= (entities[i].mana.value - entities[i].mana.maxValue);
                }
                else if (entities[i].mana.value == 0)
                {
                    entities[i].mana.value = amount;
                }
                entities[i].isManaUpdate = false;
            }
        }
    }
}