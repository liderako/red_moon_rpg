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
    public class TeleportSystem : ReactiveSystem<GameEntity>
    {
        // private Contexts _contexts;

        public TeleportSystem(Contexts contexts) : base(contexts.game)
        {
            //    _contexts = contexts;
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.TeleportCamera);
        }

        protected override bool Filter(GameEntity entity)
        {
            return entity.hasTransform;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            GameEntity entity = Contexts.sharedInstance.game.GetEntityWithName(Tags.playerAvatar);
            GameEntity entityCamera = Contexts.sharedInstance.game.GetEntityWithName(Tags.camera);
            entityCamera.transform.value.position = Vector3.MoveTowards(
                                entityCamera.transform.value.position,
                                new Vector3(entity.transform.value.position.x, entityCamera.transform.value.position.y, entity.transform.value.position.z + 10),
                                100
                            );
            entityCamera.isTeleportCamera = false;
        }
    }
}