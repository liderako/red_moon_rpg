//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ContextsGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class Contexts : Entitas.IContexts {

    public static Contexts sharedInstance {
        get {
            if (_sharedInstance == null) {
                _sharedInstance = new Contexts();
            }

            return _sharedInstance;
        }
        set { _sharedInstance = value; }
    }

    static Contexts _sharedInstance;

    public BattleContext battle { get; set; }
    public FactionContext faction { get; set; }
    public GameContext game { get; set; }
    public InputContext input { get; set; }
    public LifeContext life { get; set; }
    public TimeContext time { get; set; }

    public Entitas.IContext[] allContexts { get { return new Entitas.IContext [] { battle, faction, game, input, life, time }; } }

    public Contexts() {
        battle = new BattleContext();
        faction = new FactionContext();
        game = new GameContext();
        input = new InputContext();
        life = new LifeContext();
        time = new TimeContext();

        var postConstructors = System.Linq.Enumerable.Where(
            GetType().GetMethods(),
            method => System.Attribute.IsDefined(method, typeof(Entitas.CodeGeneration.Attributes.PostConstructorAttribute))
        );

        foreach (var postConstructor in postConstructors) {
            postConstructor.Invoke(this, null);
        }
    }

    public void Reset() {
        var contexts = allContexts;
        for (int i = 0; i < contexts.Length; i++) {
            contexts[i].Reset();
        }
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.EntityIndexGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class Contexts {

    public const string ActiveAvatar = "ActiveAvatar";
    public const string CellPointer = "CellPointer";
    public const string Name = "Name";
    public const string Persona = "Persona";
    public const string TypeFaction = "TypeFaction";

    [Entitas.CodeGeneration.Attributes.PostConstructor]
    public void InitializeEntityIndices() {
        battle.AddEntityIndex(new Entitas.EntityIndex<BattleEntity, bool>(
            ActiveAvatar,
            battle.GetGroup(BattleMatcher.ActiveAvatar),
            (e, c) => ((ActiveAvatarComponent)c).value));
        game.AddEntityIndex(new Entitas.EntityIndex<GameEntity, bool>(
            ActiveAvatar,
            game.GetGroup(GameMatcher.ActiveAvatar),
            (e, c) => ((ActiveAvatarComponent)c).value));

        battle.AddEntityIndex(new Entitas.PrimaryEntityIndex<BattleEntity, bool>(
            CellPointer,
            battle.GetGroup(BattleMatcher.CellPointer),
            (e, c) => ((CellPointerComponent)c).value));

        game.AddEntityIndex(new Entitas.PrimaryEntityIndex<GameEntity, string>(
            Name,
            game.GetGroup(GameMatcher.Name),
            (e, c) => ((RedMoonRPG.NameComponent)c).name));
        time.AddEntityIndex(new Entitas.PrimaryEntityIndex<TimeEntity, string>(
            Name,
            time.GetGroup(TimeMatcher.Name),
            (e, c) => ((RedMoonRPG.NameComponent)c).name));
        battle.AddEntityIndex(new Entitas.PrimaryEntityIndex<BattleEntity, string>(
            Name,
            battle.GetGroup(BattleMatcher.Name),
            (e, c) => ((RedMoonRPG.NameComponent)c).name));
        input.AddEntityIndex(new Entitas.PrimaryEntityIndex<InputEntity, string>(
            Name,
            input.GetGroup(InputMatcher.Name),
            (e, c) => ((RedMoonRPG.NameComponent)c).name));
        faction.AddEntityIndex(new Entitas.PrimaryEntityIndex<FactionEntity, string>(
            Name,
            faction.GetGroup(FactionMatcher.Name),
            (e, c) => ((RedMoonRPG.NameComponent)c).name));

        game.AddEntityIndex(new Entitas.EntityIndex<GameEntity, string>(
            Persona,
            game.GetGroup(GameMatcher.Persona),
            (e, c) => ((RedMoonRPG.PersonaComponent)c).value));

        battle.AddEntityIndex(new Entitas.EntityIndex<BattleEntity, Factions>(
            TypeFaction,
            battle.GetGroup(BattleMatcher.TypeFaction),
            (e, c) => ((TypeFactionComponent)c).value));
    }
}

public static class ContextsExtensions {

    public static System.Collections.Generic.HashSet<BattleEntity> GetEntitiesWithActiveAvatar(this BattleContext context, bool value) {
        return ((Entitas.EntityIndex<BattleEntity, bool>)context.GetEntityIndex(Contexts.ActiveAvatar)).GetEntities(value);
    }

    public static System.Collections.Generic.HashSet<GameEntity> GetEntitiesWithActiveAvatar(this GameContext context, bool value) {
        return ((Entitas.EntityIndex<GameEntity, bool>)context.GetEntityIndex(Contexts.ActiveAvatar)).GetEntities(value);
    }

    public static BattleEntity GetEntityWithCellPointer(this BattleContext context, bool value) {
        return ((Entitas.PrimaryEntityIndex<BattleEntity, bool>)context.GetEntityIndex(Contexts.CellPointer)).GetEntity(value);
    }

    public static GameEntity GetEntityWithName(this GameContext context, string name) {
        return ((Entitas.PrimaryEntityIndex<GameEntity, string>)context.GetEntityIndex(Contexts.Name)).GetEntity(name);
    }

    public static TimeEntity GetEntityWithName(this TimeContext context, string name) {
        return ((Entitas.PrimaryEntityIndex<TimeEntity, string>)context.GetEntityIndex(Contexts.Name)).GetEntity(name);
    }

    public static BattleEntity GetEntityWithName(this BattleContext context, string name) {
        return ((Entitas.PrimaryEntityIndex<BattleEntity, string>)context.GetEntityIndex(Contexts.Name)).GetEntity(name);
    }

    public static InputEntity GetEntityWithName(this InputContext context, string name) {
        return ((Entitas.PrimaryEntityIndex<InputEntity, string>)context.GetEntityIndex(Contexts.Name)).GetEntity(name);
    }

    public static FactionEntity GetEntityWithName(this FactionContext context, string name) {
        return ((Entitas.PrimaryEntityIndex<FactionEntity, string>)context.GetEntityIndex(Contexts.Name)).GetEntity(name);
    }

    public static System.Collections.Generic.HashSet<GameEntity> GetEntitiesWithPersona(this GameContext context, string value) {
        return ((Entitas.EntityIndex<GameEntity, string>)context.GetEntityIndex(Contexts.Persona)).GetEntities(value);
    }

    public static System.Collections.Generic.HashSet<BattleEntity> GetEntitiesWithTypeFaction(this BattleContext context, Factions value) {
        return ((Entitas.EntityIndex<BattleEntity, Factions>)context.GetEntityIndex(Contexts.TypeFaction)).GetEntities(value);
    }
}
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.VisualDebugging.CodeGeneration.Plugins.ContextObserverGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class Contexts {

#if (!ENTITAS_DISABLE_VISUAL_DEBUGGING && UNITY_EDITOR)

    [Entitas.CodeGeneration.Attributes.PostConstructor]
    public void InitializeContexObservers() {
        try {
            CreateContextObserver(battle);
            CreateContextObserver(faction);
            CreateContextObserver(game);
            CreateContextObserver(input);
            CreateContextObserver(life);
            CreateContextObserver(time);
        } catch(System.Exception) {
        }
    }

    public void CreateContextObserver(Entitas.IContext context) {
        if (UnityEngine.Application.isPlaying) {
            var observer = new Entitas.VisualDebugging.Unity.ContextObserver(context);
            UnityEngine.Object.DontDestroyOnLoad(observer.gameObject);
        }
    }

#endif
}
