using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;

namespace RedMoonRPG.Systems.InitializeSystems
{
    /*
     * Система для инициализации камеры на уровне
     */
    public class CameraInitEntitySystem : IInitializeSystem
    {
        public void Initialize()
        {
            CameraSettings cameraSettings = Resources.Load<CameraSettings>(Tags.cameraSettings);
            GameEntity cameraEntity = Contexts.sharedInstance.game.CreateEntity();
            cameraEntity.AddTransform(Camera.main.gameObject.transform);
            cameraEntity.AddName(Tags.camera);
            cameraEntity.AddSpeed(cameraSettings.speed);
            cameraEntity.AddForceSpeed(cameraSettings.ForceSpeed);
            cameraEntity.AddBorderThickness(cameraSettings.BorderThickness);
            cameraEntity.AddMapPosition(new Position(Vector3.zero));
            //cameraEntity.isTeleportCamera = true;
        }
        
    }
}