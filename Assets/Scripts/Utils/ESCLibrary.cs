using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;

namespace RedMoonRPG
{
    public static class ESCLibrary
    {
        public static GameEntity GetActivePlayer()
        {
            HashSet<GameEntity> sets = Contexts.sharedInstance.game.GetEntitiesWithActiveAvatar(true);
            foreach (var e in sets)
            {
                if (e.hasName && e.isPlayer)
                {
                    return e;
                }
            }
            Debug.LogError("Active avatar doesn't found");
            return null;
        }
    }
}
