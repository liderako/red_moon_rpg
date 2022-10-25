//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class BattleEntity {

    public PriorityStatesComponent priorityStates { get { return (PriorityStatesComponent)GetComponent(BattleComponentsLookup.PriorityStates); } }
    public bool hasPriorityStates { get { return HasComponent(BattleComponentsLookup.PriorityStates); } }

    public void AddPriorityStates(System.Collections.Generic.Dictionary<int, FilterTarget> newValue) {
        var index = BattleComponentsLookup.PriorityStates;
        var component = CreateComponent<PriorityStatesComponent>(index);
        component.value = newValue;
        AddComponent(index, component);
    }

    public void ReplacePriorityStates(System.Collections.Generic.Dictionary<int, FilterTarget> newValue) {
        var index = BattleComponentsLookup.PriorityStates;
        var component = CreateComponent<PriorityStatesComponent>(index);
        component.value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemovePriorityStates() {
        RemoveComponent(BattleComponentsLookup.PriorityStates);
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

    static Entitas.IMatcher<BattleEntity> _matcherPriorityStates;

    public static Entitas.IMatcher<BattleEntity> PriorityStates {
        get {
            if (_matcherPriorityStates == null) {
                var matcher = (Entitas.Matcher<BattleEntity>)Entitas.Matcher<BattleEntity>.AllOf(BattleComponentsLookup.PriorityStates);
                matcher.componentNames = BattleComponentsLookup.componentNames;
                _matcherPriorityStates = matcher;
            }

            return _matcherPriorityStates;
        }
    }
}
