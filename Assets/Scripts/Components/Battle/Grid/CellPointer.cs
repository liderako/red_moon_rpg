using Entitas.CodeGeneration.Attributes;
using Entitas;

[Grid]
public class CellPointerComponent : IComponent
{
    [PrimaryEntityIndex]
    public bool value;
}