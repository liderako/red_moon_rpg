using Entitas;
using Entitas.CodeGeneration.Attributes;

namespace RedMoonRPG
{
    [Game][Time][Grid]
    public class NameComponent : IComponent
    {
        [PrimaryEntityIndex]
        public string name;
    }
}