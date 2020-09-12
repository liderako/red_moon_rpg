﻿using Entitas;
using Entitas.CodeGeneration.Attributes;
using System.Collections.Generic;
using UnityEngine;

[Battle]
public class BattleListComponent : IComponent
{
    public List<GameEntity> units;
    public List<BattleEntity> battleAvatars;
    public int iterator;
}