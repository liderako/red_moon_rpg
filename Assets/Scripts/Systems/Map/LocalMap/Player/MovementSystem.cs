using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;

namespace RedMoonRPG.Systems.LocalMap.Player
{
    public class MovementSystem : ReactiveSystem<GridEntity>
    {
        private Contexts _contexts;

        public MovementSystem(Contexts contexts) : base(contexts.grid)
        {
            _contexts = contexts;
        }

        protected override ICollector<GridEntity> GetTrigger(IContext<GridEntity> context)
        {
            return context.CreateCollector(GridMatcher.MapPosition);
        }

        protected override bool Filter(GridEntity entity)
        {
            //return entity.activeAvatar.value == true && entity.hasPath && entity.path.iterator < entity.path.gridPath.Count;
            return entity.hasPath && entity.path.iterator < entity.path.gridPath.Count;
        }

        protected override void Execute(List<GridEntity> entities)
        {
            for (int i = 0; i < entities.Count; i++)
            {
                GridEntity entityGrid = entities[i];
                GameEntity entityPlayer = _contexts.game.GetEntityWithName(entityGrid.name.name);
                Move(entityPlayer, entityGrid, entityGrid.terrainGrid.value.CellGetPosition(entityGrid.path.gridPath[entityGrid.path.iterator]));
                if (entityGrid.path.iterator >= entityGrid.path.gridPath.Count)
                {
                    entityGrid.path.gridPath.Clear();
                    entityGrid.path.iterator = 0;
                    entityPlayer.AddNextAnimation(AnimationTags.idle);
                    GridEntity e = Contexts.sharedInstance.grid.GetEntityWithCellPointer(true);
                    Object.Destroy(e.transform.value.gameObject);
                    e.Destroy();
                }
            }
        }

        private void Move(GameEntity player, GridEntity avatar, Vector3 targetPosition)
        {
            // Двигаем персонажа и его аватара
            player.transform.value.position = Vector3.MoveTowards(player.transform.value.position, targetPosition, avatar.speed.value * Time.deltaTime);
            player.ReplaceMapPosition(new Position(player.transform.value.position));
            avatar.ReplaceMapPosition(new Position(player.mapPosition.value.vector));

            // Поварачиваем персонажа
            Quaternion OriginalRot = player.transform.value.rotation;
            player.transform.value.LookAt(targetPosition);
            Quaternion NewRot = player.transform.value.rotation;
            player.transform.value.rotation = OriginalRot;
            player.transform.value.rotation = Quaternion.Lerp(player.transform.value.rotation, NewRot, avatar.rotateSpeed.value * Time.deltaTime);

            // проверка дошел ли персонаж до след клетки, если да то увел итератор
            if (Vector3.Distance(player.transform.value.position, targetPosition) <= 0.001f)
            {
                avatar.path.iterator += 1;
            }
        }
    }
}