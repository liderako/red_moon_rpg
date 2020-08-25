using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;

namespace RedMoonRPG.Triggers
{
    public class OnTriggerBattle : MonoBehaviour
    {

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == Tags.PlayerLayer)
            {
                GameEntity[] gameGroup = Contexts.sharedInstance.game.GetGroup(GameMatcher.ActiveAvatar).GetEntities();
                GridEntity[] gridGroup = Contexts.sharedInstance.grid.GetGroup(GridMatcher.ActiveAvatar).GetEntities();
                for (int i = 0; i < gridGroup.Length; i++)
                {
                    if (gridGroup[i].isBattle == true) return; // защита от постоянного вхождение в битву в случаях когда анимация юнита крутит тригерр
                    gridGroup[i].ReplaceActiveAvatar(false);
                    gridGroup[i].isBattle = true;
                }
                for (int i = 0; i < gameGroup.Length; i++)
                {
                    gameGroup[i].ReplaceActiveAvatar(false);
                    Debug.Log(gameGroup[i].persona.value + " now in battle");
                }
            }
        }

        private void ActivateBattle(HashSet<GridEntity> array)
        {
            foreach (var value in array)
            {
                value.isBattle = true;
                value.ReplaceActiveAvatar(false);
            }
        }
        private void ActivateBattle(HashSet<GameEntity> array)
        {
            foreach (var value in array)
            {
                value.ReplaceActiveAvatar(false);
            }
        }
    }
}