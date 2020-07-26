using RedMoonRPG.Utils;
using RedMoonRPG.Settings.Objects;
using System.IO;
using System.Diagnostics;
using UnityEngine.SceneManagement;
using UnityEngine;

namespace RedMoonRPG.Settings
{
    public class GameData : Singleton<GameData>
    {
        public Data Model;

        public LevelSettings LevelSettings;

        public void Start()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
            //LoadingData();
            //SavedData();
            OnSceneLoaded(SceneManager.GetActiveScene(), LoadSceneMode.Single);
            DontDestroyOnLoad(gameObject);
        }

        public void SavedData()
        {
            FileReader reader = new FileReader();
            reader.SaveData(Model);
        }

        public void LoadingData() // потом нужно будет по айди разные даты загружать =)
        {
            FileReader reader = new FileReader();
            Model = reader.LoadSavedData();
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            LevelSettings = Resources.Load<LevelSettings>(Tags.levelSettings + scene.name);
        }
    }
}