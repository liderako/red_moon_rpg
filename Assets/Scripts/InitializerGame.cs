using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializerGame : MonoBehaviour
{
    public void Start()
    {
        StartCoroutine(wait());
    }

    private IEnumerator wait()
    {
        yield return new WaitForSeconds(0.01f);
        Contexts.sharedInstance.game.GetEntityWithName("Level").AddNextLevelName("Map");
    }
}
