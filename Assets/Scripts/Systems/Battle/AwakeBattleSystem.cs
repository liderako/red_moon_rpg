using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;

namespace RedMoonRPG.Systems.Battle
{
    public class AwakeBattleSystem : ReactiveSystem<BattleEntity>
    {
        public AwakeBattleSystem(Contexts contexts) : base(contexts.battle)
        {
        }

        protected override ICollector<BattleEntity> GetTrigger(IContext<BattleEntity> context)
        {
            return context.CreateCollector(BattleMatcher.AwakeBattle);
        }

        protected override bool Filter(BattleEntity entity)
        {
            return entity.isAwakeBattle;
        }

        protected override void Execute(List<BattleEntity> entities)
        {
            if (entities.Count != 1)
            {
                Debug.LogError("Awake Battle может существовать только в одном экземпляре");
                return;
            }
            Awake(entities[0]);
        }

        private void Awake(BattleEntity manager)
        {
            List<GameEntity> gameEntities = new List<GameEntity>();
            List<GridEntity> gridsEntities = new List<GridEntity>();
            InitEntities(gameEntities, gridsEntities);
            StartBattle(manager, gameEntities, gridsEntities);
        }

        private void InitEntities(List<GameEntity> gameEntities, List<GridEntity> gridsEntities)
        {
            GridEntity[] gridGroup = Contexts.sharedInstance.grid.GetGroup(GridMatcher.ActiveAvatar).GetEntities();
            GameEntity[] gameGroup = Contexts.sharedInstance.game.GetGroup(GameMatcher.ActiveAvatar).GetEntities();
            if (gameGroup.Length != gridGroup.Length)
            {
                Debug.LogError("Длинна массива доступных юнитов и аватаров не равная.");
                return;
            }
            gameGroup = SortForDExterity(gameGroup);
            Debug.Log("Порядок ходов:");
            foreach (var e in gameGroup)
            {
                Debug.Log(e.name.name);
            }
            for (int i = 0; i < gameGroup.Length; i++)
            {
                GridEntity g = Contexts.sharedInstance.grid.GetEntityWithName(gameGroup[i].name.name);
                g.isBattle = true;
                g.ReplaceActiveAvatar(false);
                g.RemovePath();
                gameGroup[i].ReplaceActiveAvatar(false);
                gameEntities.Add(gameGroup[i]);
                if (gameGroup[i].hasNextAnimation)
                {
                    gameGroup[i].ReplaceNextAnimation(AnimationTags.idle);
                }
                else
                {
                    gameGroup[i].AddNextAnimation(AnimationTags.idle);
                }
                gridsEntities.Add(g);
            }
        }

        private void StartBattle(BattleEntity manager, List<GameEntity> gameEntities, List<GridEntity> gridsEntities)
        {
            manager.isUpdateActiveAvatar = true;
            manager.AddName(Tags.battleManagerEntity);
            manager.isAwakeBattle = false;
            manager.AddBattleList(gameEntities, gridsEntities, -1);
            manager.AddRound(0);
            Contexts.sharedInstance.input.GetEntityWithName(Tags.inputEntity).isBattle = true;
        }

        private GameEntity[] SortForDExterity(GameEntity[] group)
        {
            int len = group.Length;
            for (int i = 0; i < len - 1; i++)
            {
                for (int j = i + 1; j < len; j++)
                {
                    // compare array element with  
                    // all next element 
                    if (group[i].dexterity.value < group[j].dexterity.value)
                    {
                        GameEntity tmp = group[i];
                        group[i] = group[j];
                        group[j] = tmp;
                    }
                }
            }
            return group;
        }
    }
}