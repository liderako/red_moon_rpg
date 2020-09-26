using Entitas;
using Entitas.CodeGeneration.Attributes;

namespace RedMoonRPG
{
    [Game][Time][Battle][Input][Faction][Character]
    public class NameComponent : IComponent
    {
        [PrimaryEntityIndex]
        public string name;
    }
}