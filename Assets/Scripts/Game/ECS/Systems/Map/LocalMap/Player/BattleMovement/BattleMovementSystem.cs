using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;

namespace RedMoonRPG.Systems.LocalMap.Player.Battle
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
            BattleEntity entityBattle = entities[0];
            GameEntity entityPlayer = _contexts.game.GetEntityWithName(entityBattle.name.name);
            Move(entityPlayer, entityBattle, entityBattle.terrainGrid.value.CellGetPosition(entityBattle.path.gridPath[entityBattle.path.iterator]));
            if (entityBattle.path.iterator >= entityBattle.path.gridPath.Count)
            {
                entityBattle.terrainGrid.value.CellGetAtPosition(entityPlayer.transform.value.position, true).canCross = false;
                entityBattle.path.gridPath.Clear();
                entityBattle.path.iterator = 0;
                entityPlayer.ReplaceActiveAvatar(true);
                entityBattle.ReplaceActiveAvatar(true);
                entityBattle.ReplaceActionPoint(entityBattle.actionPoint.value);
                entityPlayer.AddNextAnimation(AnimationTags.idle);
                BattleEntity e = Contexts.sharedInstance.battle.GetEntityWithCellPointer(true);
                Object.Destroy(e.transform.value.gameObject);
                e.Destroy();
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