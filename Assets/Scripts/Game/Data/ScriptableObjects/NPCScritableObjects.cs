using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameItems", menuName = "ScriptableObjects/NPC")]
public class NPCScriptableObjects : ScriptableObject
{
    public string name;
    public GameObject model;
    public Factions faction; 
    
    /*
    * Основные характеристики возможные у любого предмета типа Weapon, Armor, Interactive
    */
    public int attention = 5;
    public int dexterity = 5;
    public int endurance = 5;
    public int intellect = 5;
    public int luck = 5;
    public int personality = 5;
    public int strength = 5;

    // move
    public float speed = 2.0f;
    public float rotateSpeed = 5.0f;
    
    // ресурс для оружия
    public ItemScriptableObjects weapon;

    // battle
    public float radiusAttack = 1.0f;
}
