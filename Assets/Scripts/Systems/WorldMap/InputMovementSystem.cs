using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;
using Entitas.CodeGeneration.Attributes;

namespace WorldMap
{
    public class InputMovementSystem : IExecuteSystem
    {
        private Contexts _contexts;

        public InputMovementSystem(Contexts contexts)
        {
            _contexts = contexts;
        }

        public void Execute()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    GameEntity entity = _contexts.game.GetEntityWithName("PlayerModel");
                    GameEntity entityCamera = _contexts.game.GetEntityWithName("Camera");
                    entityCamera.isFreeCamera = false;
                    entity.isWorldMapMovement = true;
                    entity.AddTargetPosition(new Position(hit.point));
                    entity.ReplaceMapPosition(new Position(Vector3.zero));
                }
            }
        }
    }
}