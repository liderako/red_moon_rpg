using UnityEngine;
using Newtonsoft.Json;

namespace RedMoonRPG.Settings.Objects
{
    [System.Serializable]
    public class MainSettings : ISettingObject
    {
        public int MagicDamageForIntellect; // используется для подсчета бонуса к магическому урону за каждую единицу интелекта
        public float RangeMagicDamage; // процент помехи между максимальным и минимальным магическим уроном
        public int MeleeDamageForStrength; // используется для подсчета бонуса к физическому урону за каждую единицу силы
        public float RangeMeleeDamage;  // процент помехи между максимальным и минимальным физическому уроном в ближнем бою
        public int RangedDamageForDexterity; // используется для подсчета бонуса к физическому урону дальнего боя за каждую единицу ловкости
        public float RangeRangedDamage; // процент помехи между максимальным и минимальным физическому уроном в дальнем бою
        public int ManaForLevelIntellect; // используется для подсчета бонуса к максимальному значению мани за каждую единицу интелекта
        public int HpForLevelEndurance; // используется для подсчета бонуса к максимальному значению хп за каждую единицу выносливости

        public void UpdateData(Data data, TextAsset textAsset)
        {
            data.MainData = JsonConvert.DeserializeObject<MainSettings>(textAsset.text);
        }

        public string NameSettingsFile { get {return "MainSettings";}}
    }
}