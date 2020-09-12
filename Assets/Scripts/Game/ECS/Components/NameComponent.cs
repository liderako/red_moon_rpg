using Entitas;
using Entitas.CodeGeneration.Attributes;

namespace RedMoonRPG
{
    [Game][Time][Battle][Input][Faction]
    public class NameComponent : IComponent
    {
        [PrimaryEntityIndex]
        public string name;
    }
}