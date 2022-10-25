using System.Collections;
using System.Collections.Generic;
using Entitas;
using UnityEngine;

[Character]
public class DamageTypeComponent : IComponent
{
    public List<DamageType> value;
}
