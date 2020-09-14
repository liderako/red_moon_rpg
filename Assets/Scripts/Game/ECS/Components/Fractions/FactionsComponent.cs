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
