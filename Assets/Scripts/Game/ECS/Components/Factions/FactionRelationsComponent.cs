using System.Collections.Generic;
using Entitas;

/*
 * Данный компонент необходим для фракции,
 * чтобы знать кто является врагов а кто союзником в бою.
 */
[Faction]
public class FactionRelationsComponent : IComponent
{
    public Factions value; // сама фракция
    public Dictionary<Factions, bool> reputation; // если тру тогда союзник, если фалс тогда враг
}

/*
 * Например:
 * // создаем сущность для хранение отношений между фракциями
 * GameEntity factionsPlayer = new GameEntity();
 *
 * // создаем сущность для существующих фракций
 * GameEntity factionsEntity = new GameEntity();
 *
 * // создаем лист возможных фракций
 * List<Factions> factions = new Factions();
 * factions.Add(Factions.Player);
 * factions.Add(Factions.TribesOfHorde);
 * factions.Add(Factions.KingdomOfMetozan);
 * // добавляем для хранение активных фракций массив в сущность
 * factionsEntity.AddFactions(factions);
 * // составляем отношения между фракциями
 * Dictionary<Factions, bool> dictionaryRelationsPlayer = new Dictionary<Factions, bool>(); // создаем словарь отношений 
 * dictionary[Factions.Player] = true;
 * dictionary[Factions.TribesOfHorde] = false;
 * dictionary[Factions.KingdomOfMetozan] = true;
 * factionsPlayer.AddFactionRelations(Factions.Player, dictionary);
 *
 * По такому принципу все кто будут пренадлежать фракции игрока будут считать союзниками королевство метозан,
 * а врагами племена Орды.
*/