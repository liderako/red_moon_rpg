//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class BattleEntity {

    public MapPositionComponent mapPosition { get { return (MapPositionComponent)GetComponent(BattleComponentsLookup.MapPosition); } }
    public bool hasMapPosition { get { return HasComponent(BattleComponentsLookup.MapPosition); } }

    public void AddMapPosition(Position newValue) {
        var index = BattleComponentsLookup.MapPosition;
        var component = CreateComponent<MapPositionComponent>(index);
        component.value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceMapPosition(Position newValue) {
        var index = BattleComponentsLookup.MapPosition;
        var component = CreateComponent<MapPositionComponent>(index);
        component.value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveMapPosition() {
        RemoveComponent(BattleComponentsLookup.MapPosition);
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityInterfaceGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class BattleEntity : IMapPosition { }

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentMatcherGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed partial class BattleMatcher {

    static Entitas.IMatcher<BattleEntity> _matcherMapPosition;

    public static Entitas.IMatcher<BattleEntity> MapPosition {
        get {
            if (_matcherMapPosition == null) {
                var matcher = (Entitas.Matcher<BattleEntity>)Entitas.Matcher<BattleEntity>.AllOf(BattleComponentsLookup.MapPosition);
                matcher.componentNames = BattleComponentsLookup.componentNames;
                _matcherMapPosition = matcher;
            }

            return _matcherMapPosition;
        }
    }
}
