using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;
using RedMoonRPG.Settings;

/*
** Система обновляен данные про урон в дальнем бою для игрового персонажа
** собирая данные со всех бафоф и артефактов
*/
public class UpdateRangedDamageSystem : ReactiveSystem<GameEntity>
{
    private Contexts _contexts;
    private int _lvlBonus;
    private float _range;
    
    public UpdateRangedDamageSystem(Contexts contexts) : base(contexts.game)
    {
        _contexts = contexts;
        _lvlBonus = GameSettings.Instance.Model.MainData.RangedDamageForDexterity;
        _range = GameSettings.Instance.Model.MainData.RangeRangedDamage;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.UpdateRangedDamage);
    }

    /*
    ** Система будет вызиваться только один раз на персонажа благодаря тому
    ** что Имя может быть только у одного Entity на персонажа
    */
    protected override bool Filter(GameEntity entity)
    {
        return entity.hasName && entity.isUpdateRangedDamage && entity.hasPersona;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        GameContext g = _contexts.game;
        int len = entities.Count;
        for (int i = 0; i < entities.Count; i++)
        {
            HashSet<GameEntity> array = _contexts.game.GetEntitiesWithPersona(entities[i].persona.value);
            int amount = 0;
            int minAmount = 0;
            foreach (GameEntity gn in array)
            {
                if (gn.hasDexterity) // если это стата персонажа или баф от шмоток или тд зайдет сюда
                {
                    amount += (gn.dexterity.value * _lvlBonus); // подсчитываем бонусный урон от всей ловкости персонажа
                }
                if (gn.hasIndexRangedDamage && !gn.hasName) // если это оружие то зайдет сюда
                {
                    amount += (gn.indexRangedDamage.maxValue); // бонус от оружия к максимальному урону
                    minAmount += (gn.indexRangedDamage.minValue); // бонус от оружия к минимальному урону
                }
            }
            entities[i].indexRangedDamage.maxValue = amount;
            entities[i].indexRangedDamage.minValue = amount - (int)(amount * _range) + minAmount;
        }
    }
}