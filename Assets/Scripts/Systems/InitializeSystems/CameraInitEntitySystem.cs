using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;

namespace RedMoonRPG.InitializeSystems
{
    /*
     * Система для инициализации камеры на уровне
     */
    public class CameraInitEntitySystem : ReactiveSystem<GameEntity>
    {
        private Contexts _contexts;

        public CameraInitEntitySystem(Contexts contexts) : base(contexts.game)
        {
            _contexts = contexts;
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.CameraCreate);
        }
        protected override bool Filter(GameEntity entity)
        {
            return entity.isCameraCreate;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            if (_contexts.game.GetEntityWithName(Tags.camera) == null)
            {
                CameraSettings cameraSettings = Resources.Load<CameraSettings>(Tags.cameraSettings);
                GameEntity cameraEntity = _contexts.game.CreateEntity();
                cameraEntity.AddTransform(Camera.main.gameObject.transform);
                cameraEntity.AddName(Tags.camera);
                cameraEntity.AddSpeed(cameraSettings.speed);
                cameraEntity.AddForceSpeed(cameraSettings.ForceSpeed);
                cameraEntity.AddBorderThickness(cameraSettings.BorderThickness);
                cameraEntity.AddMapPosition(new Position(Vector3.zero));
                //cameraEntity.isTeleportCamera = true;
                entities[0].isCameraCreate = false;
            }
        }
    }
}