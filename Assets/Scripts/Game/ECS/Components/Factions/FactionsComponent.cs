using System.Collections.Generic;
using Entitas;

/*
 * Данный компонент хранинит активные игровые фракции.
 */
[Faction]
public class FactionsComponent : IComponent
{
    public List<FactionStruct> value;
}

public struct FactionStruct
{
    public Factions name;
    public Dictionary<Factions, bool> relations;
}