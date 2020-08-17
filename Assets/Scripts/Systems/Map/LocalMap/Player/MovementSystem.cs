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
            return entity.activeAvatar.value == true && entity.hasPath && entity.path.iterator < entity.path.gridPath.Count;
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

        private void Move(GameEntity player, GridEntity entityGrid, Vector3 targetPosition)
        {

            // TO DO УБРАТЬ СУКА МАГИЧЕСКИЕ ЧИСЛА
            float speed = 2;
            float step = speed * Time.deltaTime;

            // target position must account the sphere height since the cellGetPosition will return the center of the cell which is at floor.
            targetPosition.y += player.transform.value.localScale.y * 0.5f;
            player.transform.value.position = Vector3.MoveTowards(player.transform.value.position, targetPosition, step);
            player.ReplaceMapPosition(new Position(player.transform.value.position));
            entityGrid.ReplaceMapPosition(new Position(player.mapPosition.value.vector));

            // Rotate
            Quaternion OriginalRot = player.transform.value.rotation;
            player.transform.value.LookAt(targetPosition, new Vector3(0, 1, 0));
            Quaternion NewRot = player.transform.value.rotation;
            player.transform.value.rotation = OriginalRot;
            player.transform.value.rotation = Quaternion.Lerp(player.transform.value.rotation, NewRot, 5 * Time.deltaTime);

            // Check if character has reached next cell (we use a small threshold to avoid floating point comparison; also we check only xz plane since the character y position could be adjusted or limited
            // by the slope of the terrain).
            float dist = Vector2.Distance(new Vector2(player.transform.value.position.x, player.transform.value.position.z), new Vector2(targetPosition.x, targetPosition.z));
            
            if (dist <= 0.001f)
            {
                entityGrid.path.iterator += 1;
            }
        }
    }
}