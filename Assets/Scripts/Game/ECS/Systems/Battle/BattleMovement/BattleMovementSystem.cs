using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;
using TGS;

namespace RedMoonRPG.Systems.Battle.Movement
{
    public class BattleMovementSystem : ReactiveSystem<BattleEntity>
    {
        private Contexts _contexts;
    
        public BattleMovementSystem(Contexts contexts) : base(contexts.battle)
        {
            _contexts = contexts;
        }
    
        protected override ICollector<BattleEntity> GetTrigger(IContext<BattleEntity> context)
        {
            return context.CreateCollector(BattleMatcher.MapPosition);
        }
    
        protected override bool Filter(BattleEntity entity)
        {
            return entity.activeAvatar.value && entity.hasPath && entity.path.iterator < entity.path.gridPath.Count && entity.isBattle;
        }
    
        protected override void Execute(List<BattleEntity> entities)
        {
            if (entities.Count > 1)
            {
                Debug.LogError("Во время битвы сущности ходят по очереди!\nСейчас есть ошибка с количеством сущностей действующим за раз.\n Антонио исправь это.");
            }
            BattleEntity avatar = entities[0];
            GameEntity unit = _contexts.game.GetEntityWithName(avatar.name.name);
            Move(unit, avatar, avatar.terrainGrid.value.CellGetPosition(avatar.path.gridPath[avatar.path.iterator]));
            if (avatar.path.iterator >= avatar.path.gridPath.Count)
            {
                Cell cell = avatar.terrainGrid.value.CellGetAtPosition(avatar.mapPosition.value.vector, true);
                avatar.terrainGrid.value.CellSetCanCross(avatar.terrainGrid.value.CellGetIndex(cell), false);
                avatar.path.gridPath.Clear();
                avatar.path.iterator = 0;
                unit.ReplaceActiveAvatar(true);
                avatar.ReplaceActiveAvatar(true);
                avatar.ReplaceActionPoint(avatar.actionPoint.value);
                if (avatar.actionPoint.value == 0)
                {
                    avatar.isEndTurn = true;
                }
                unit.AddNextAnimation(AnimationTags.idle);
                if (unit.isPlayer)
                {
                    BattleEntity e = Contexts.sharedInstance.battle.GetEntityWithCellPointer(true);
                    Object.Destroy(e.transform.value.gameObject);
                    e.Destroy();   
                }
            }
        }
    
        private void Move(GameEntity unit, BattleEntity avatar, Vector3 targetPosition)
        {
            // Двигаем персонажа и его аватара
            unit.transform.value.position = Vector3.MoveTowards(unit.transform.value.position, targetPosition, avatar.speed.value * Time.deltaTime);
            unit.ReplaceMapPosition(new Position(unit.transform.value.position));
            avatar.ReplaceMapPosition(new Position(unit.mapPosition.value.vector));
    
            // Поварачиваем персонажа
            Quaternion OriginalRot = unit.transform.value.rotation;
            unit.transform.value.LookAt(targetPosition);
            Quaternion NewRot = unit.transform.value.rotation;
            unit.transform.value.rotation = OriginalRot;
            unit.transform.value.rotation = Quaternion.Lerp(unit.transform.value.rotation, NewRot, avatar.rotateSpeed.value * Time.deltaTime);
    
            // проверка дошел ли персонаж до след клетки, если да то увел итератор
            if (Vector3.Distance(unit.transform.value.position, targetPosition) <= 0.001f)
            {
                if (unit.activeAvatar.value == false && avatar.activeAvatar.value == false)
                {
                    avatar.path.gridPath.Clear();
                    avatar.path.iterator = 0;
                    return;
                }
                avatar.path.iterator += 1;
            }
        }
    }
}