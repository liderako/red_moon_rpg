﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;

namespace WorldMap
{
    /*
    ** Системя для передвижение самой модельки персонажа по глобальной карте
    ** между локациями. Используется NavMesh Agent;
    */
    public class MovementSystem : ReactiveSystem<GameEntity>
    {
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
            return entity.isWorldMapMovement == true;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            GameEntity player = _contexts.game.GetEntityWithName("PlayerModel");
            if (player.isWorldMapMovement == true)
            {
                if (player.hasTargetPosition)
                {
                    player.navMeshAgent.agent.SetDestination(player.targetPosition.value.vector);
                    player.RemoveTargetPosition();
                    player.ReplaceMapPosition(new Position(player.navMeshAgent.agent.transform.position));
                }
                if (Vector3.Distance(player.mapPosition.value.vector, player.navMeshAgent.agent.destination) < player.navMeshAgent.agent.stoppingDistance)
                {
                    player.isWorldMapMovement = false;
                }
                player.ReplaceMapPosition(new Position(player.navMeshAgent.agent.transform.position));
            }
        }
    }
}