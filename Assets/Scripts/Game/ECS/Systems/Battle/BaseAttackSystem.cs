using System.Collections;
using System.Collections.Generic;
using Entitas;
using UnityEngine;

namespace RedMoonRPG.Systems.Battle
{
    public class BaseAttackSystem : ReactiveSystem<BattleEntity>
    {
        public BaseAttackSystem(Contexts contexts) : base(contexts.battle)
        {
        }
        
        protected override ICollector<BattleEntity> GetTrigger(IContext<BattleEntity> context)
        {
            return context.CreateCollector(BattleMatcher.Attack);
        }
        
        protected override bool Filter(BattleEntity entity)
        {
            return entity.isAttack;
        }
        
        protected override void Execute(List<BattleEntity> avatar)
        {
            if (avatar.Count > 1)
            {
                Debug.LogError("BaseAttack error");
            }
            GameEntity unit = Contexts.sharedInstance.game.GetEntityWithName(avatar[0].name.name);
            unit.AddNextAnimation(AnimationTags.SwordAttack);
            avatar[0].isAttack = false;
            avatar[0].ReplaceActionPoint(avatar[0].actionPoint.value - GetPriceForBaseAttack(avatar[0]));
            Quaternion OriginalRot = unit.transform.value.rotation;
            unit.transform.value.LookAt(avatar[0].targetEnemy.value.mapPosition.value.vector);
            Quaternion NewRot = unit.transform.value.rotation;
            unit.transform.value.rotation = OriginalRot;
            unit.transform.value.rotation = Quaternion.Lerp(unit.transform.value.rotation, NewRot, avatar[0].rotateSpeed.value);
        }
        
        private int GetPriceForBaseAttack(BattleEntity avatar)
        {
            return avatar.typeAttack.value[AnimationTags.SwordAttack];
        }
    }
}