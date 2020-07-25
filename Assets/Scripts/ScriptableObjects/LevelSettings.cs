using UnityEngine;

[CreateAssetMenu(fileName = "LevelSettings", menuName = "ScriptableObjects/LevelSettings", order = 3)]
public class LevelSettings : ScriptableObject
{
    public Vector2 axisX;
    public Vector2 axisY;
    public Vector2 axisZ;
}
