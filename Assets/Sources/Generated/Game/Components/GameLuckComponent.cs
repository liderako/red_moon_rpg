//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public LuckComponent luck { get { return (LuckComponent)GetComponent(GameComponentsLookup.Luck); } }
    public bool hasLuck { get { return HasComponent(GameComponentsLookup.Luck); } }

    public void AddLuck(int newValue) {
        var index = GameComponentsLookup.Luck;
        var component = CreateComponent<LuckComponent>(index);
        component.value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceLuck(int newValue) {
        var index = GameComponentsLookup.Luck;
        var component = CreateComponent<LuckComponent>(index);
        component.value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveLuck() {
        RemoveComponent(GameComponentsLookup.Luck);
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentMatcherGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed partial class GameMatcher {

    static Entitas.IMatcher<GameEntity> _matcherLuck;

    public static Entitas.IMatcher<GameEntity> Luck {
        get {
            if (_matcherLuck == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.Luck);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherLuck = matcher;
            }

            return _matcherLuck;
        }
    }
}