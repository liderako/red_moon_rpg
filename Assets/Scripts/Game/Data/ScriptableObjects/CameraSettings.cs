using UnityEngine;

[CreateAssetMenu(fileName = "CameraData", menuName = "ScriptableObjects/CameraSettings")]
public class CameraSettings : ScriptableObject
{
    [Space(10)]
    [Header("Camera main Settings")]
    [Space(10)]
    public float speed;
    public float ForceSpeed;
    public float BorderThickness;
}
