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
            List<BattleEntity> battleEntities = new List<BattleEntity>();
            InitEntities(gameEntities, battleEntities);
            StartBattle(manager, gameEntities, battleEntities);
        }
    
        private void InitEntities(List<GameEntity> gameEntities, List<BattleEntity> gridsEntities)
        {
            BattleEntity[] battleGroup = Contexts.sharedInstance.battle.GetGroup(BattleMatcher.ActiveAvatar).GetEntities();
            GameEntity[] gameGroup = Contexts.sharedInstance.game.GetGroup(GameMatcher.ActiveAvatar).GetEntities();
            if (gameGroup.Length != battleGroup.Length)
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
                BattleEntity avatar = Contexts.sharedInstance.battle.GetEntityWithName(gameGroup[i].name.name);
                avatar.isBattle = true;
                avatar.ReplaceActiveAvatar(false);
                MoveUnitToCenterCell(gameGroup[i], avatar);
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
                gridsEntities.Add(avatar);
            }
        }
    
        private void MoveUnitToCenterCell(GameEntity unit, BattleEntity avatar)
        {
            Vector3 targetPosition = avatar.terrainGrid.value.CellGetPosition(avatar.terrainGrid.value.CellGetAtPosition(unit.transform.value.position, true), true);
            unit.transform.value.position = targetPosition;
            unit.ReplaceMapPosition(new Position(targetPosition));
            avatar.ReplaceMapPosition(new Position(targetPosition));
            avatar.RemovePath();
        }
    
        private void StartBattle(BattleEntity manager, List<GameEntity> gameEntities, List<BattleEntity> battleEntities)
        {
            manager.isUpdateActiveAvatar = true;
            manager.AddName(Tags.battleManagerEntity);
            manager.isAwakeBattle = false;
            manager.AddBattleList(gameEntities, battleEntities, -1);
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
                    CharacterEntity iEntity = Contexts.sharedInstance.character.GetEntityWithName(group[i].name.name);
                    CharacterEntity jEntity = Contexts.sharedInstance.character.GetEntityWithName(group[i].name.name);
                    if (iEntity.dexterity.value < jEntity.dexterity.value)
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