using Entitas;
using Entitas.CodeGeneration.Attributes;

[Grid][Game]
public class ActiveAvatarComponent : IComponent
{
    [EntityIndex]
    public bool value;
}
