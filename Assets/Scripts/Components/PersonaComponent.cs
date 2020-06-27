using Entitas;
using Entitas.CodeGeneration.Attributes;

namespace RedMoonRPG
{
    [Game]
    public class PersonaComponent : IComponent
    {
        [EntityIndex]
        public string value;
    }
}