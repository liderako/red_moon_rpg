using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;

namespace RedMoonRPG.Systems.InitializeSystems
{
    public class CreateFactionSystem : IInitializeSystem
    {
        public void Initialize()
        {
            // создаем сущность для хранение отношений между фракциями
            FactionEntity factionEntity = Contexts.sharedInstance.faction.CreateEntity();
            factionEntity.AddName(Tags.factionData);
            
            List<Factions> factions = new List<Factions>();
            // создаем лист возможных фракций
            factions.Add(Factions.Player);
            factions.Add(Factions.TribesOfHorde);
            factions.Add(Factions.KingdomOfMetozan);
            // добавляем для хранение активных фракций массив в сущность
            factionEntity.AddFactions(factions);
            
            factionEntity.AddFactionRelations(Factions.Player, InitPlayerFaction());
            factionEntity.AddFactionRelations(Factions.TribesOfHorde, InitHordeFaction());
            factionEntity.AddFactionRelations(Factions.KingdomOfMetozan, InitKingdomMetozan());
        }

        public Dictionary<Factions, bool> InitPlayerFaction()
        {
            Dictionary<Factions, bool> dictionaryRelationsPlayer = new Dictionary<Factions, bool>();
            dictionaryRelationsPlayer[Factions.Player] = true;
            dictionaryRelationsPlayer[Factions.TribesOfHorde] = false;
            dictionaryRelationsPlayer[Factions.KingdomOfMetozan] = true;
            return dictionaryRelationsPlayer;
        }

        public Dictionary<Factions, bool> InitHordeFaction()
        {
            Dictionary<Factions, bool> dictionaryRelationsPlayer = new Dictionary<Factions, bool>();
            dictionaryRelationsPlayer[Factions.Player] = false;
            dictionaryRelationsPlayer[Factions.TribesOfHorde] = true;
            dictionaryRelationsPlayer[Factions.KingdomOfMetozan] = false;
            return dictionaryRelationsPlayer;
        }
        
        public Dictionary<Factions, bool> InitKingdomMetozan()
        {
            Dictionary<Factions, bool> dictionaryRelationsPlayer = new Dictionary<Factions, bool>();
            dictionaryRelationsPlayer[Factions.Player] = false;
            dictionaryRelationsPlayer[Factions.TribesOfHorde] = true;
            dictionaryRelationsPlayer[Factions.KingdomOfMetozan] = false;
            return dictionaryRelationsPlayer;
        }
    }
}

/*
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