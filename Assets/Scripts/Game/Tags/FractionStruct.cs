using System.Collections.Generic;

public struct FactionStruct
{
    public Factions name; // сама фракция
    public Dictionary<Factions, bool> relations; // если тру тогда союзник, если фалс тогда враг

    public FactionStruct(Factions name, Dictionary<Factions, bool> relations)
    {
        this.name = name;
        this.relations = relations;
    }
}