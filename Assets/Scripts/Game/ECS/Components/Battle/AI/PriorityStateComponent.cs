using System.Collections.Generic;
using Entitas;

[Battle]
public class PriorityStatesComponent : IComponent
{
    public Dictionary<int, FilterTarget> value;
}
