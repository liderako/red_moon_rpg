using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;

public class OnTriggerZonaLevel : MonoBehaviour
{
    [SerializeField] private string nameNextLevel;

    private void OnTriggerEnter(Collider other)
    {
        Contexts.sharedInstance.game.GetEntityWithName("Level").AddNextLevelName(nameNextLevel);
    }
}
