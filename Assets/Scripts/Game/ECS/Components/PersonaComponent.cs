using Entitas;
using Entitas.CodeGeneration.Attributes;

namespace RedMoonRPG
{
    [Game][Character]
    public class PersonaComponent : IComponent
    {
        [EntityIndex]
        public string value;
    }
}