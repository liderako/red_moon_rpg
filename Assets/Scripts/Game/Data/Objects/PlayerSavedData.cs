using UnityEngine;
using Newtonsoft.Json;

namespace RedMoonRPG.Settings.Objects
{
    /*
     * по факту класс необходимо будет использовать для сохранение не только позиции игрока а для сохранение всех настроек каждого персонажа или сопартийца
     */

    [System.Serializable]
    public class PlayerSavedData
    {
        // Transform сохранить нельзя.
        public Vector3 position;
    }
}