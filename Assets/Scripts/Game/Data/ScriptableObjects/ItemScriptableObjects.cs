using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameItems", menuName = "ScriptableObjects/Items")]
public class ItemScriptableObjects : ScriptableObject
{
    public string name;
    public ItemType itemType;
    public WeaponType type;
    
    // визуализация
    public GameObject itemPrefab;
    public Sprite itemIcon;

    // default stat
    public int weigth;
    public int defaultPrice;
    public int maxStackableAmount;
    
    /*
    * Основные характеристики возможные у любого предмета типа Weapon, Armor, Interactive
    */
    public int attention;
    public int dexterity;
    public int endurance;
    public int intellect;
    public int luck;
    public int personality;
    public int strength;
    
    // дополнительные характеристики для оружия
    public Vector2Int meeleDamage;
    public Vector2Int rangedDamage;
    public Vector2Int magicDamage;
    
    public List<DamageType> damageTypes;

        // дополнительные характеристики для интерактивных обьектов
    public int calory;
    public int water;
    public float duration;
    
    // tags
    public bool isStackable;
    public bool isQuestable;
    public bool isDestroyOnUse;
}
