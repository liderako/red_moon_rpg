using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;
using RedMoonRPG;

public class TMP_GUI_WITHOUT_BUTTONS : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Skip turn");
            Contexts.sharedInstance.battle.GetEntityWithName(Tags.battleManagerEntity).isSkipTurn = true;
        }
    }
}
