using UnityEngine;
using RedMoonRPG.Settings.Objects;
using System.IO;
using Newtonsoft.Json;

namespace RedMoonRPG.Settings
{
    public class FileReader
    {
        public void SaveData(Data data)
        {
#if UNITY_EDITOR
            Debug.Log(Application.persistentDataPath);
#endif
            File.WriteAllText(Application.persistentDataPath + "/" + Tags.mainData + ".json", JsonConvert.SerializeObject(data));
        }
        
        public Data LoadSavedData()
        {
            return JsonConvert.DeserializeObject<Data>(ReadSettings(Tags.mainData)); ;
        }

        private string ReadSettings(string nameFile)
        {
            string s = File.ReadAllText(Application.persistentDataPath + "/" + nameFile + ".json");
            return s;
        }
    }
}