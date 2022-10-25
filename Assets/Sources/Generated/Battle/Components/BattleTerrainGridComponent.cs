//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class BattleEntity {

    public TerrainGridComponent terrainGrid { get { return (TerrainGridComponent)GetComponent(BattleComponentsLookup.TerrainGrid); } }
    public bool hasTerrainGrid { get { return HasComponent(BattleComponentsLookup.TerrainGrid); } }

    public void AddTerrainGrid(TGS.TerrainGridSystem newValue) {
        var index = BattleComponentsLookup.TerrainGrid;
        var component = CreateComponent<TerrainGridComponent>(index);
        component.value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceTerrainGrid(TGS.TerrainGridSystem newValue) {
        var index = BattleComponentsLookup.TerrainGrid;
        var component = CreateComponent<TerrainGridComponent>(index);
        component.value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveTerrainGrid() {
        RemoveComponent(BattleComponentsLookup.TerrainGrid);
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
public sealed partial class BattleMatcher {

    static Entitas.IMatcher<BattleEntity> _matcherTerrainGrid;

    public static Entitas.IMatcher<BattleEntity> TerrainGrid {
        get {
            if (_matcherTerrainGrid == null) {
                var matcher = (Entitas.Matcher<BattleEntity>)Entitas.Matcher<BattleEntity>.AllOf(BattleComponentsLookup.TerrainGrid);
                matcher.componentNames = BattleComponentsLookup.componentNames;
                _matcherTerrainGrid = matcher;
            }

            return _matcherTerrainGrid;
        }
    }
}
