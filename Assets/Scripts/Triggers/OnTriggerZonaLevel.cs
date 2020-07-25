using UnityEngine;
using Entitas;

namespace RedMoonRPG.Triggers
{
    public class OnTriggerZonaLevel : MonoBehaviour
    {
        [SerializeField] private string nameNextLevel;

        private void OnTriggerEnter(Collider other)
        {
            Contexts.sharedInstance.game.GetEntityWithName(Tags.level).AddNextLevelName(nameNextLevel);
        }
    }
}