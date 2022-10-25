using Entitas.CodeGeneration.Attributes;
using Entitas;

[Battle]
public class CellPointerComponent : IComponent
{
    [PrimaryEntityIndex]
    public bool value;
}