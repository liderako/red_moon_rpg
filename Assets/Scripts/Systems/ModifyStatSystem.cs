using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;

/*
** Система для модификации основных параметров персонажа
** Система будет использоваться при создании персонажа либо редактировании основных параметров.
*/
public class ModifyStatSystem : ReactiveSystem<GameEntity>
{
    private Contexts _contexts;
    
    public ModifyStatSystem(Contexts contexts) : base(contexts.game)
    {
        _contexts = contexts;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.ModifiedStat);
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.hasModifiedStat;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        int len = entities.Count;
        for (int i = 0; i < entities.Count; i++)
        {
            ChangeStat(entities[i]);
            entities[i].RemoveModifiedStat();
        }
    }

    private void ChangeStat(GameEntity entity)
    {
        switch(entity.modifiedStat.stat)
        {
            case Stat.Intellect:
                entity.ReplaceIntellect(entity.modifiedStat.newValue);
                break;
            case Stat.Endurance:
                entity.ReplaceEndurance(entity.modifiedStat.newValue);
                break;
            case Stat.Luck:
                entity.ReplaceLuck(entity.modifiedStat.newValue);
                break;
            case Stat.Strength:
                entity.ReplaceStrength(entity.modifiedStat.newValue);
                break;
            case Stat.Dexterity:
                entity.ReplaceDexterity(entity.modifiedStat.newValue);
                break;
            case Stat.Attention:
                entity.ReplaceAttention(entity.modifiedStat.newValue);
                break;
            case Stat.Personality:
                entity.ReplacePersonality(entity.modifiedStat.newValue);
                break;
        }
    }
}