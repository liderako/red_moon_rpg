using Entitas;
using Entitas.CodeGeneration.Attributes;

[Battle][Game]
public class ActiveAvatarComponent : IComponent
{
    [EntityIndex]
    public bool value;
}
