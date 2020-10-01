using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RedMoonRPG
{
    public class AnimatorListener : MonoBehaviour
    {
        private GameEntity unit;
        
        public void Hit()
        {
            unit.AddNextAnimation(AnimationTags.idle);
            Contexts.sharedInstance.battle.GetEntityWithName(this.unit.name.name).isEndAttack = true;
        }

        public void EndAttackAnimation()
        {
            if (Contexts.sharedInstance.battle.GetEntityWithName(this.unit.name.name).actionPoint.value == 0)
            {
                Contexts.sharedInstance.battle.GetEntityWithName(this.unit.name.name).isEndTurn = true;
            }
            else
            {
                StartCoroutine(wait());
            }
        }

        private IEnumerator wait()
        {
            yield return  new WaitForSeconds(0.01f);
            Contexts.sharedInstance.battle.GetEntityWithName(unit.name.name).ReplaceActiveAvatar(true);
        }

        public void SetUnit(GameEntity unit)
        {
            this.unit = unit;
        }
    }
}