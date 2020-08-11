using UnityEngine;
using RedMoonRPG.Settings.Objects;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace RedMoonRPG.Settings
{
    public class FileReader
    {
        //private string path = Application.dataPath + Tags.ResourcesPathGridSettings;
        public void SaveData(Data data)
        {
#if UNITY_EDITOR
            Debug.Log(Application.persistentDataPath);
#endif
            File.WriteAllText(Application.persistentDataPath + "/" + Tags.mainData + ".json", JsonConvert.SerializeObject(data));
        }
        
        public Data LoadSavedData()
        {
            return JsonConvert.DeserializeObject<Data>(ReadSettings(Tags.mainData));
        }

        private string ReadSettings(string nameFile)
        {
            string s = File.ReadAllText(Application.persistentDataPath + "/" + nameFile + ".json");
            return s;
        }

        // метод для загрузки с json файла грид системы
        public List<int> LoadGridJson(string levelName)
        {
            if (!ExistsLevelGridSystem(levelName + ".json"))
            {
                return null;
            }
            string fileName = levelName;
            TextAsset asset = Resources.Load(Tags.PathGridSettings + fileName) as TextAsset;
            string s = asset.text;
            return JsonConvert.DeserializeObject<List<int>>(s);
        }

        // метод для сохранение в json грид систему уровня
        public void SaveGridSystem(string levelName, List<int> cells)
        {
            string fileName = levelName + ".json";
            File.WriteAllText(Application.dataPath + Tags.ResourcesPathGridSettings + fileName, JsonConvert.SerializeObject(cells));
        }

        private bool ExistsLevelGridSystem(string fileName)
        {
            if ((File.Exists(Application.dataPath + Tags.ResourcesPathGridSettings + fileName)) == false)
            {
                return false;
            }
            return true;
        }
    }
}