using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/GameBalanceSettings")]
public class GameBalanceSettings : ScriptableObject
{
    [Space(10)]
    [Header("Damage balance Settings")]
    [Space(10)]
    public int MagicDamageForIntellect; // используется для подсчета бонуса к магическому урону за каждую единицу интелекта
    public float RangeMagicDamage; // процент помехи между максимальным и минимальным магическим уроном
    public int MeleeDamageForStrength; // используется для подсчета бонуса к физическому урону за каждую единицу силы
    public float RangeMeleeDamage;  // процент помехи между максимальным и минимальным физическому уроном в ближнем бою
    public int RangedDamageForDexterity; // используется для подсчета бонуса к физическому урону дальнего боя за каждую единицу ловкости
    public float RangeRangedDamage; // процент помехи между максимальным и минимальным физическому уроном в дальнем бою
    public int ManaForLevelIntellect; // используется для подсчета бонуса к максимальному значению мани за каждую единицу интелекта
    public int HpForLevelEndurance; // используется для подсчета бонуса к максимальному значению хп за каждую единицу выносливости
}