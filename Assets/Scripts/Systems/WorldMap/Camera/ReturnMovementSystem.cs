using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;
using Entitas.CodeGeneration.Attributes;

namespace RedMoonRPG.Systems.WorldMap.Camera
{
    /*
    ** Система для свободного управление камерой на любой из локаций.
    */
    public class ReturnMovementSystem : ReactiveSystem<GameEntity>
    {
       // private Contexts _contexts;
        
        public ReturnMovementSystem(Contexts contexts) : base(contexts.game)
        {
        //    _contexts = contexts;
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.MapPosition);
        }

        protected override bool Filter(GameEntity entity)
        {
            return entity.hasTargetPosition &&
            entity.hasTransform &&
            entity.hasForceSpeed &&
            entity.isWorldMapMovement == true;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            for (int i = 0; i < entities.Count; i++)
            {
                entities[i].transform.value.position = Vector3.MoveTowards(
                    entities[i].transform.value.position,
                    new Vector3(entities[i].targetPosition.value.vector.x, entities[i].transform.value.position.y, entities[i].targetPosition.value.vector.z + 10),
                    entities[i].forceSpeed.value * Time.deltaTime
                );
                entities[i].ReplaceMapPosition(new Position(entities[i].transform.value.position));
                if (Vector2.Distance(
                    new Vector2(entities[i].mapPosition.value.vector.x, entities[i].mapPosition.value.vector.z),
                    new Vector2(entities[i].targetPosition.value.vector.x, entities[i].targetPosition.value.vector.z + 10)) < 1)
                {
                    entities[i].RemoveTargetPosition();
                    entities[i].isWorldMapMovement = false;
                }
            }
        }
    }
}