using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;

namespace RedMoonRPG.Systems.WorldMap.Camera
{
    /*
    ** Система необходимая для того чтобы камера следовала за фигуркой игрока на глобальной карте
    */
    public class CameraFollowerMovementSystem : ReactiveSystem<GameEntity>
    {
        private Contexts _contexts;
        
        public CameraFollowerMovementSystem(Contexts contexts) : base(contexts.game)
        {
            _contexts = contexts;
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.MapPosition);
        }

        protected override bool Filter(GameEntity entity)
        {
            return entity.isWorldMapMovement &&
            _contexts.game.GetEntityWithName(Tags.camera).isFreeCamera == false; // и тут проверка если игрок начал двигать мышкой то слежка прекратится за игроком.
        }

        protected override void Execute(List<GameEntity> entities)
        {
            // TO DO точно ли нужно оставить по таким тегам искать?
            // может поменять на теги по типу констант в файлике
            GameEntity entityCamera = _contexts.game.GetEntityWithName(Tags.camera);
            GameEntity entityPlayer = _contexts.game.GetEntityWithName(Tags.playerAvatar);
            entityCamera.transform.value.position = Vector3.MoveTowards(
                entityCamera.transform.value.position,
                new Vector3(entityPlayer.transform.value.position.x, entityCamera.transform.value.position.y, entityPlayer.transform.value.position.z + 10),
                entityCamera.forceSpeed.value * Time.deltaTime
            );
        }
    }
}