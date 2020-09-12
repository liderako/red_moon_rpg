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
    public class MovementSystem : IExecuteSystem
    {
        private Contexts _contexts;

        public MovementSystem(Contexts contexts)
        {
            _contexts = contexts;
        }

        public void Execute()
        {
            GameEntity camera = _contexts.game.GetEntityWithName(Tags.camera);
            GameEntity level = _contexts.game.GetEntityWithName(Tags.level);
            Vector3 pos = camera.transform.value.localPosition;
            float speed = (Input.GetKey("left shift") ? camera.forceSpeed.value : camera.speed.value);
            if (Input.GetKey("w") || Input.mousePosition.y >= Screen.height - camera.borderThickness.value)
            {
                float y = pos.y;
                pos += camera.transform.value.forward.normalized * speed * Time.deltaTime;
                pos.y = y;
                camera.isFreeCamera = true;
            }
            if (Input.GetKey("s") || Input.mousePosition.y <= camera.borderThickness.value)
            {
                float y = pos.y;
                pos -= camera.transform.value.forward * speed * Time.deltaTime;
                pos.y = y;
                camera.isFreeCamera = true;
            }
            if (Input.GetKey("d") || Input.mousePosition.x >= Screen.width - camera.borderThickness.value)
            {
                float y = pos.y;
                pos += camera.transform.value.right * speed * Time.deltaTime;
                pos.y = y;
                camera.isFreeCamera = true;
            }
            if (Input.GetKey("a") || Input.mousePosition.x <= camera.borderThickness.value)
            {
                float y = pos.y;
                pos -= camera.transform.value.right * speed * Time.deltaTime;
                pos.y = y;
                camera.isFreeCamera = true;
            }
            if (Input.GetAxis("Mouse ScrollWheel") != 0)
            {
                float t = (Input.GetAxis("Mouse ScrollWheel") > 0 ? -1 : 1);
                pos.y += (camera.forceSpeed.value * t) * Time.deltaTime;
                pos.y = Mathf.Clamp(pos.y, level.limitMap.axisY.x, level.limitMap.axisY.y);
            }
            if (pos.x != camera.transform.value.localPosition.x || pos.z != camera.transform.value.localPosition.z || pos.y != camera.transform.value.localPosition.y)
            {
                pos.x = Mathf.Clamp(pos.x, level.limitMap.axisX.x, level.limitMap.axisX.y);
                pos.z = Mathf.Clamp(pos.z, level.limitMap.axisZ.x, level.limitMap.axisZ.y);
                camera.transform.value.localPosition = pos;
            }
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                GameEntity entity = ESCLibrary.GetActivePlayer();
                GameEntity entityCamera = _contexts.game.GetEntityWithName(Tags.camera);
                entityCamera.isFreeCamera = false;
                entityCamera.isWorldMapMovement = true;
                entityCamera.ReplaceMapPosition(new Position(entityCamera.transform.value.position));
                if (entityCamera.hasTargetPosition)
                {
                    entityCamera.ReplaceTargetPosition(entity.mapPosition.value);
                }
                else
                {
                    entityCamera.AddTargetPosition(entity.mapPosition.value);
                }
            }
        }
    }
}