using System.Collections.Generic;
using Entitas;

[Battle]
public class PriorityFilterTargetComponent : IComponent
{
    public Dictionary<int, FilterTarget> value;
}