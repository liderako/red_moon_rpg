using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;
using Entitas.CodeGeneration.Attributes;

namespace RedMoonRPG.Systems.WorldMap.Player
{
    /*
    ** Система необходимая для управление передвижением игрока на глобальной карте
    */
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
                Ray ray = UnityEngine.Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    GameEntity entityPlayer = ESCLibrary.GetActivePlayer();
                    GameEntity entityCamera = _contexts.game.GetEntityWithName(Tags.camera);
                    entityCamera.isFreeCamera = false;
                    entityPlayer.isWorldMapMovement = true;
                    entityPlayer.AddTargetPosition(new Position(hit.point));
                    entityPlayer.ReplaceMapPosition(new Position(Vector3.zero));
                }
            }
        }
    }
}