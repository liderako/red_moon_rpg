using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;

/*
** Системя для получение урона и уменьшение хп
*/

public class DamageSystem : ReactiveSystem<GameEntity>
{
    private Contexts _contexts;
    
    public DamageSystem(Contexts contexts) : base(contexts.game)
    {
        _contexts = contexts;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.Damaged);
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.hasDamaged && entity.hasHealth;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        int len = entities.Count;
        for (int i = 0; i < entities.Count; i++)
        {
            entities[i].health.value = entities[i].health.value - entities[i].damaged.value;
            entities[i].RemoveDamaged();
        }
    }
}
