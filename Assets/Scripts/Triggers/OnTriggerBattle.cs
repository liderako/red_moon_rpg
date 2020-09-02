using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;
using System.Linq;

namespace RedMoonRPG.Triggers
{
    public class OnTriggerBattle : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == Tags.PlayerLayer)
            {
                GridEntity[] gridGroup = Contexts.sharedInstance.grid.GetGroup(GridMatcher.ActiveAvatar).GetEntities();
                for (int i = 0; i < gridGroup.Length; i++)
                {
                    if (gridGroup[i].isBattle) // защита от постоянного вхождение в битву в случаях когда анимация юнита крутит тригерр
                        return;
                }
                BattleEntity battleManager = Contexts.sharedInstance.battle.CreateEntity();
                battleManager.isAwakeBattle = true;
            }   
        }
    }
}