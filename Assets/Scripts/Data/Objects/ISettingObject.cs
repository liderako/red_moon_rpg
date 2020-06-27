using UnityEngine;

namespace RedMoonRPG.Settings.Objects
{
    public interface ISettingObject
    {
        string NameSettingsFile {get;}
        void UpdateData(Data data, TextAsset textAsset);
    }
}
