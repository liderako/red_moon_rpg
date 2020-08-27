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
                GameEntity[] gameGroup = Contexts.sharedInstance.game.GetGroup(GameMatcher.ActiveAvatar).GetEntities();
                List<GameEntity> gameEntities = new List<GameEntity>();
                List<GridEntity> gridsEntities = new List<GridEntity>();
                if (gameGroup.Length != gridGroup.Length)
                {
                    Debug.LogError("Длинна массива доступных юнитов и аватаров не равная.");
                    return;
                }
                gameGroup = SortForDExterity(gameGroup);
                Debug.Log("Порядок ходов:");
                foreach (var e in gameGroup)
                {
                    Debug.Log(e.name.name);
                }
                for (int i = 0; i < gameGroup.Length; i++)
                {
                    GridEntity g = Contexts.sharedInstance.grid.GetEntityWithName(gameGroup[i].name.name);
                    g.isBattle = true;
                    g.ReplaceActiveAvatar(false);
                    gameGroup[i].ReplaceActiveAvatar(false);
                    gameEntities.Add(gameGroup[i]);
                    gameGroup[i].AddNextAnimation(AnimationTags.idle);
                    gridsEntities.Add(g);
                }
                BattleEntity battleManager = Contexts.sharedInstance.battle.CreateEntity();
                battleManager.isUpdateActiveAvatar = true;
                battleManager.AddBattleList(gameEntities, gridsEntities, 0);
            }
        }

        private GameEntity[] SortForDExterity(GameEntity[] group)
        {
            for (int i = 0; i < group.Length - 1; i++)
            {
                for (int j = i + 1; j < group.Length; j++)
                {
                    // compare array element with  
                    // all next element 
                    if (group[i].dexterity.value < group[j].dexterity.value)
                    {
                        GameEntity tmp = group[i];
                        group[i] = group[j];
                        group[j] = tmp;
                    }
                }
            }
            return group;
        }
    }
}