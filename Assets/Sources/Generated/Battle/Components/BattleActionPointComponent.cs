//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class BattleEntity {

    public ActionPointComponent actionPoint { get { return (ActionPointComponent)GetComponent(BattleComponentsLookup.ActionPoint); } }
    public bool hasActionPoint { get { return HasComponent(BattleComponentsLookup.ActionPoint); } }

    public void AddActionPoint(int newValue) {
        var index = BattleComponentsLookup.ActionPoint;
        var component = CreateComponent<ActionPointComponent>(index);
        component.value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceActionPoint(int newValue) {
        var index = BattleComponentsLookup.ActionPoint;
        var component = CreateComponent<ActionPointComponent>(index);
        component.value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveActionPoint() {
        RemoveComponent(BattleComponentsLookup.ActionPoint);
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

    static Entitas.IMatcher<BattleEntity> _matcherActionPoint;

    public static Entitas.IMatcher<BattleEntity> ActionPoint {
        get {
            if (_matcherActionPoint == null) {
                var matcher = (Entitas.Matcher<BattleEntity>)Entitas.Matcher<BattleEntity>.AllOf(BattleComponentsLookup.ActionPoint);
                matcher.componentNames = BattleComponentsLookup.componentNames;
                _matcherActionPoint = matcher;
            }

            return _matcherActionPoint;
        }
    }
}