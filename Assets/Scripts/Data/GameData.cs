using RedMoonRPG.Utils;
using RedMoonRPG.Settings.Objects;
using System.IO;
using System.Diagnostics;

namespace RedMoonRPG.Settings
{
    public class GameData : Singleton<GameData>
    {
        public Data Model;
        public void Start()
        {
            DontDestroyOnLoad(gameObject);
            //LoadingData();
            //SavedData();
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
    }
}