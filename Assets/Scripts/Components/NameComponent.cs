using Entitas;
using Entitas.CodeGeneration.Attributes;

namespace RedMoonRPG
{
    [Game][Time][Grid][Battle][Input]
    public class NameComponent : IComponent
    {
        [PrimaryEntityIndex]
        public string name;
    }
}