﻿using RedMoonRPG.Settings;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RedMoonRPG.InitializeSystems
{
    public class InitializerGame : MonoBehaviour
    {
        [SerializeField] private GameObject _gameControllerWorldMap;
        [SerializeField] private GameObject _gameControllerLocalMap;
        [SerializeField] private bool WorldMap;

        private void Awake()
        {
            if (LoaderController.Instance == null)
            {
                Debug.Log("init loader controller");
                GameObject go = new GameObject();
                go.AddComponent<LoaderController>();
                go.name = "LoaderController";
            }
            if (GameData.Instance == null)
            {
                Debug.Log("init game data");
                GameObject go = new GameObject();
                go.AddComponent<GameData>();
                go.name = "GameData";
            }
            if (SceneManager.GetActiveScene().name == Tags.init)
            {
                StartCoroutine(StartGame());
            }
            if (WorldMap)
            {
                _gameControllerWorldMap.SetActive(true);
            }
            else
            {
                _gameControllerLocalMap.SetActive(true);
            }
        }

        private IEnumerator StartGame()
        {
            yield return new WaitForSeconds(1f);
            Contexts.sharedInstance.game.GetEntityWithName(Tags.level).AddNextLevelName(Scenes.baseMap);
            Destroy(this);
        }
    }
}
