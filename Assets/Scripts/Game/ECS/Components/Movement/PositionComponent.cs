﻿using Entitas.CodeGeneration.Attributes;
using UnityEngine;

[Game,Battle, CustomComponentName("TargetPosition", "MapPosition")]
public class Position
{
    public Position(Vector3 p)
    {
        vector = p;
    }

    public UnityEngine.Vector3 vector;
}
