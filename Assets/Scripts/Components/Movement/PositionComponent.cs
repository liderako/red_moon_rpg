using Entitas.CodeGeneration.Attributes;
using UnityEngine;

[Game, CustomComponentName("TargetPosition", "MapPosition")]
public class Position
{
    public Position(Vector3 p)
    {
        vector = p;
    }

    public UnityEngine.Vector3 vector;
}

