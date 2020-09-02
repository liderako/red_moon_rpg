using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;
using RedMoonRPG;

public class TMP_GUI_WITHOUT_BUTTONS : MonoBehaviour
{
    private InputEntity inputEntity;

    private void Awake()
    {
        inputEntity = Contexts.sharedInstance.input.CreateEntity();
        inputEntity.AddName(Tags.inputEntity);
        inputEntity.AddInputMouse(-1);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Skip turn");
            Contexts.sharedInstance.battle.GetEntityWithName(Tags.battleManagerEntity).isSkipTurn = true;
        }
        if (Input.GetMouseButtonDown(0))
        {
            inputEntity.ReplaceInputMouse(0);
        }
    }
}
