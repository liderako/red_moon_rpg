//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public RedMoonRPG.NextLevelNameComponent nextLevelName { get { return (RedMoonRPG.NextLevelNameComponent)GetComponent(GameComponentsLookup.NextLevelName); } }
    public bool hasNextLevelName { get { return HasComponent(GameComponentsLookup.NextLevelName); } }

    public void AddNextLevelName(string newValue) {
        var index = GameComponentsLookup.NextLevelName;
        var component = CreateComponent<RedMoonRPG.NextLevelNameComponent>(index);
        component.value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceNextLevelName(string newValue) {
        var index = GameComponentsLookup.NextLevelName;
        var component = CreateComponent<RedMoonRPG.NextLevelNameComponent>(index);
        component.value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveNextLevelName() {
        RemoveComponent(GameComponentsLookup.NextLevelName);
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

    static Entitas.IMatcher<GameEntity> _matcherNextLevelName;

    public static Entitas.IMatcher<GameEntity> NextLevelName {
        get {
            if (_matcherNextLevelName == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.NextLevelName);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherNextLevelName = matcher;
            }

            return _matcherNextLevelName;
        }
    }
}
