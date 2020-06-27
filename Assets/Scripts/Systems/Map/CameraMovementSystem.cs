using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;
using Entitas.CodeGeneration.Attributes;

namespace WorldMap
{
    public class CameraMovementSystem : IExecuteSystem
    {
        private Contexts _contexts;

        public CameraMovementSystem(Contexts contexts)
        {
            _contexts = contexts;
        }

        public void Execute()
        {
            GameEntity camera = _contexts.game.GetEntityWithName("Camera");
            GameEntity level = _contexts.game.GetEntityWithName("Level");
            Vector3 pos = camera.transform.value.position;
            float speed = (Input.GetKey("left shift") ? camera.forceSpeed.value : camera.speed.value);
            if (Input.GetKey("w") || Input.mousePosition.y >= Screen.height - camera.borderThickness.value)
            {
                pos.z += speed * Time.deltaTime;
                camera.isFreeCamera = true;
            }
            if (Input.GetKey("s") || Input.mousePosition.y <= camera.borderThickness.value)
            {
                pos.z -= speed * Time.deltaTime;
                camera.isFreeCamera = true;
            }
            if (Input.GetKey("d") || Input.mousePosition.x >= Screen.width - camera.borderThickness.value)
            {
                pos.x += speed * Time.deltaTime;
                camera.isFreeCamera = true;
            }
            if (Input.GetKey("a") || Input.mousePosition.x <= camera.borderThickness.value)
            {
                pos.x -= speed * Time.deltaTime;
                camera.isFreeCamera = true;
            }
            if (Input.GetAxis("Mouse ScrollWheel") !=0 )
            {
                float t = (Input.GetAxis("Mouse ScrollWheel") > 0 ? -1 : 1);
                pos.y += (camera.forceSpeed.value * t) * Time.deltaTime;
                pos.y = Mathf.Clamp(pos.y, level.limitMap.axisY.x, level.limitMap.axisY.y);
            }
            if (pos.x != camera.transform.value.position.x || pos.z != camera.transform.value.position.z || pos.y != camera.transform.value.position.y)
            {
                pos.x = Mathf.Clamp(pos.x, level.limitMap.axisX.x, level.limitMap.axisX.y);
                pos.z = Mathf.Clamp(pos.z, level.limitMap.axisZ.x, level.limitMap.axisZ.y);
                camera.transform.value.position = pos;
            }
        }
    }
}