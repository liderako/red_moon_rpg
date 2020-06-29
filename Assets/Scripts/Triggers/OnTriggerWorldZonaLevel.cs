using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;

public class OnTriggerWorldZonaLevel : MonoBehaviour
{
    [SerializeField] private string nameNextLevel;
    [SerializeField] private GameObject button;
    [SerializeField] private ButtonOnTriggerLevel script;

    private void OnTriggerEnter(Collider other)
    {
        button.SetActive(true);
        script.nameNextLevel = nameNextLevel;
    }
    
    private void OnTriggerExit(Collider other)
    {
        button.SetActive(false);
    }
}
