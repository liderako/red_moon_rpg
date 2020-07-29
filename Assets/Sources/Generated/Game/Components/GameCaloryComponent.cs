//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public CaloryComponent calory { get { return (CaloryComponent)GetComponent(GameComponentsLookup.Calory); } }
    public bool hasCalory { get { return HasComponent(GameComponentsLookup.Calory); } }

    public void AddCalory(int newValue) {
        var index = GameComponentsLookup.Calory;
        var component = CreateComponent<CaloryComponent>(index);
        component.value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceCalory(int newValue) {
        var index = GameComponentsLookup.Calory;
        var component = CreateComponent<CaloryComponent>(index);
        component.value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveCalory() {
        RemoveComponent(GameComponentsLookup.Calory);
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

    static Entitas.IMatcher<GameEntity> _matcherCalory;

    public static Entitas.IMatcher<GameEntity> Calory {
        get {
            if (_matcherCalory == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.Calory);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherCalory = matcher;
            }

            return _matcherCalory;
        }
    }
}
