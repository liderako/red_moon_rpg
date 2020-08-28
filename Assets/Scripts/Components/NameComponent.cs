using Entitas;
using Entitas.CodeGeneration.Attributes;

namespace RedMoonRPG
{
    [Game][Time][Grid][Battle]
    public class NameComponent : IComponent
    {
        [PrimaryEntityIndex]
        public string name;
    }
}