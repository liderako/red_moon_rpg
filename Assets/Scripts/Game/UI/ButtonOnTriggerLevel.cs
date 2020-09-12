using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;

namespace RedMoonRPG.UI
{
    public class ButtonOnTriggerLevel : MonoBehaviour
    {
        public string nameNextLevel;

        public void OnClick()
        {
            Contexts.sharedInstance.game.GetEntityWithName(Tags.level).AddNextLevelName(nameNextLevel);
        }
    }
}