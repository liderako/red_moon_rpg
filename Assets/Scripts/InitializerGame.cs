using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RedMoonRPG
{
    public class InitializerGame : MonoBehaviour
    {
        [SerializeField] private GameObject _gameController;

        private void Awake()
        {
            if (LoaderController.Instance == null)
            {
                gameObject.AddComponent<LoaderController>();
            }
            if (SceneManager.GetActiveScene().name == Tags.init)
            {
                StartCoroutine(StartGame());
            }
            if (_gameController != null)
            {
                _gameController.SetActive(true);
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
