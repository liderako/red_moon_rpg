using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;

namespace RedMoonRPG.Systems.Battle
{
    public class QueueBattleSystems : ReactiveSystem<BattleEntity>
    {
        public QueueBattleSystems(Contexts contexts) : base(contexts.battle)
        {
        }

        protected override ICollector<BattleEntity> GetTrigger(IContext<BattleEntity> context)
        {
            return context.CreateCollector(BattleMatcher.UpdateActiveAvatar);
        }

        protected override bool Filter(BattleEntity entity)
        {
            return entity.hasBattleList;
        }

        protected override void Execute(List<BattleEntity> entities)
        {
            if (entities.Count > 1)
            {
                Debug.LogError("QueueBattle error");
                return;
            }
            BattleEntity battleManager = entities[0];
            int i = battleManager.battleList.iterator;
            if (battleManager.battleList.iterator > battleManager.battleList.gridAvatars.Count)
            {
                battleManager.battleList.iterator = 0;
            }
            battleManager.battleList.gridAvatars[i].ReplaceActiveAvatar(true);
            battleManager.battleList.units[i].ReplaceActiveAvatar(true);
            Debug.Log("Now Active Avatar is " + battleManager.battleList.units[i].name.name);
            battleManager.battleList.iterator += 1;
        }
    }
}


/*
         private Contexts _contexts;
        
        public MovementSystem(Contexts contexts) : base(contexts.game)
        {
            _contexts = contexts;
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.MapPosition);
        }

        protected override bool Filter(GameEntity entity)
        {
            return entity.isWorldMapMovement == true && entity.hasNavMeshAgent && entity.hasAnimator && !entity.hasNextAnimation;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            GameEntity player = ESCLibrary.GetActivePlayer();
            if (player.isWorldMapMovement == true)
            {
                if (player.hasTargetPosition)
                {
                    player.navMeshAgent.agent.SetDestination(player.targetPosition.value.vector);
                    player.AddNextAnimation(AnimationTags.walk);
                    player.RemoveTargetPosition();
                    player.ReplaceMapPosition(new Position(player.navMeshAgent.agent.transform.position));
                }
                else if (Vector3.Distance(player.mapPosition.value.vector, player.navMeshAgent.agent.destination) < player.navMeshAgent.agent.stoppingDistance)
                {
                    player.isWorldMapMovement = false;
                    player.AddNextAnimation(AnimationTags.idle);
                }
                player.ReplaceMapPosition(new Position(player.navMeshAgent.agent.transform.position));
            }
        }
 * */
