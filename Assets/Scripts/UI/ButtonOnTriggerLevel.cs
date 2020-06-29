using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;

public class ButtonOnTriggerLevel : MonoBehaviour
{
    public string nameNextLevel;

    public void OnClick()
    {
        // Debug.Log(nameNextLevel);
        Contexts.sharedInstance.game.GetEntityWithName("Level").AddNextLevelName(nameNextLevel);
        // entity.AddPersona("allsalaslslasa");
    }
}
