//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class BattleEntity {

    public FilterTargetComponent filterTarget { get { return (FilterTargetComponent)GetComponent(BattleComponentsLookup.FilterTarget); } }
    public bool hasFilterTarget { get { return HasComponent(BattleComponentsLookup.FilterTarget); } }

    public void AddFilterTarget(FilterTarget newValue) {
        var index = BattleComponentsLookup.FilterTarget;
        var component = CreateComponent<FilterTargetComponent>(index);
        component.value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceFilterTarget(FilterTarget newValue) {
        var index = BattleComponentsLookup.FilterTarget;
        var component = CreateComponent<FilterTargetComponent>(index);
        component.value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveFilterTarget() {
        RemoveComponent(BattleComponentsLookup.FilterTarget);
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

    static Entitas.IMatcher<BattleEntity> _matcherFilterTarget;

    public static Entitas.IMatcher<BattleEntity> FilterTarget {
        get {
            if (_matcherFilterTarget == null) {
                var matcher = (Entitas.Matcher<BattleEntity>)Entitas.Matcher<BattleEntity>.AllOf(BattleComponentsLookup.FilterTarget);
                matcher.componentNames = BattleComponentsLookup.componentNames;
                _matcherFilterTarget = matcher;
            }

            return _matcherFilterTarget;
        }
    }
}
