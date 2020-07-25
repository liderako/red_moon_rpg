using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "LevelSettings", menuName = "ScriptableObjects/LevelSettings", order = 3)]
public class LevelSettings : ScriptableObject
{
    [Header("Limit Map")]
    public Vector2 axisX;
    public Vector2 axisY;
    public Vector2 axisZ;
    //[Space(5)]
    //[Header("Player position")]
    //public List<Vector3> positions;
}
