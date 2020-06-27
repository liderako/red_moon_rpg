using Entitas;
using Entitas.CodeGeneration.Attributes;

namespace RedMoonRPG
{
    [Game]
    public class NameComponent : IComponent
    {
        [PrimaryEntityIndex]
        public string name;
    }
}