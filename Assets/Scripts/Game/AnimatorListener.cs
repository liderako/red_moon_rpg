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
        }

        public void SetUnit(GameEntity unit)
        {
            this.unit = unit;
        }
    }
}