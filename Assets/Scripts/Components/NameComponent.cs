using Entitas;
using Entitas.CodeGeneration.Attributes;

namespace RedMoonRPG
{
    [Game][Time]
    public class NameComponent : IComponent
    {
        [PrimaryEntityIndex]
        public string name;
    }
}