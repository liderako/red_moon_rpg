//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public MapPositionComponent mapPosition { get { return (MapPositionComponent)GetComponent(GameComponentsLookup.MapPosition); } }
    public bool hasMapPosition { get { return HasComponent(GameComponentsLookup.MapPosition); } }

    public void AddMapPosition(Position newValue) {
        var index = GameComponentsLookup.MapPosition;
        var component = CreateComponent<MapPositionComponent>(index);
        component.value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceMapPosition(Position newValue) {
        var index = GameComponentsLookup.MapPosition;
        var component = CreateComponent<MapPositionComponent>(index);
        component.value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveMapPosition() {
        RemoveComponent(GameComponentsLookup.MapPosition);
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

    static Entitas.IMatcher<GameEntity> _matcherMapPosition;

    public static Entitas.IMatcher<GameEntity> MapPosition {
        get {
            if (_matcherMapPosition == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.MapPosition);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherMapPosition = matcher;
            }

            return _matcherMapPosition;
        }
    }
}
