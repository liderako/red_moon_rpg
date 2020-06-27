using UnityEngine;
using RedMoonRPG.Settings.Objects;

namespace RedMoonRPG.Settings
{
    public class FileReader
    {
        private const string _pathGameSettings = "GameSettings/";
        private ISettingObject[] _settingObjects = new ISettingObject[] {new MainSettings()};

        public Data GetGameSettings()
        {
            Data data = new Data();
            UpdateData(data);
            return data;
        }

        private void UpdateData(Data data)
        {
            int len = _settingObjects.Length;
            for (int i = 0; i < len; i++)
            {
                _settingObjects[i].UpdateData(data, ReadSettings(_settingObjects[i].NameSettingsFile));
            }
        }

        private TextAsset ReadSettings(string nameSettingsFile)
        {
            TextAsset textAsset = (TextAsset)Resources.Load(_pathGameSettings + nameSettingsFile);
            return textAsset;
        }
    }
}
