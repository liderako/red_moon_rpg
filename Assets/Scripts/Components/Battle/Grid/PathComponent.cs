using Entitas;
using System.Collections;
using System.Collections.Generic;
using TGS;

[Grid]
public class PathComponent : IComponent
{
    public List<int> gridPath;
    public int iterator;
}