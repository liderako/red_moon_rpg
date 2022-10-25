//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class BattleEntity {

    public RoundComponent round { get { return (RoundComponent)GetComponent(BattleComponentsLookup.Round); } }
    public bool hasRound { get { return HasComponent(BattleComponentsLookup.Round); } }

    public void AddRound(int newValue) {
        var index = BattleComponentsLookup.Round;
        var component = CreateComponent<RoundComponent>(index);
        component.value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceRound(int newValue) {
        var index = BattleComponentsLookup.Round;
        var component = CreateComponent<RoundComponent>(index);
        component.value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveRound() {
        RemoveComponent(BattleComponentsLookup.Round);
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

    static Entitas.IMatcher<BattleEntity> _matcherRound;

    public static Entitas.IMatcher<BattleEntity> Round {
        get {
            if (_matcherRound == null) {
                var matcher = (Entitas.Matcher<BattleEntity>)Entitas.Matcher<BattleEntity>.AllOf(BattleComponentsLookup.Round);
                matcher.componentNames = BattleComponentsLookup.componentNames;
                _matcherRound = matcher;
            }

            return _matcherRound;
        }
    }
}
