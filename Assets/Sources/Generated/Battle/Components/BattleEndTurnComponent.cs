//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class BattleEntity {

    static readonly EndTurnComponent endTurnComponent = new EndTurnComponent();

    public bool isEndTurn {
        get { return HasComponent(BattleComponentsLookup.EndTurn); }
        set {
            if (value != isEndTurn) {
                var index = BattleComponentsLookup.EndTurn;
                if (value) {
                    var componentPool = GetComponentPool(index);
                    var component = componentPool.Count > 0
                            ? componentPool.Pop()
                            : endTurnComponent;

                    AddComponent(index, component);
                } else {
                    RemoveComponent(index);
                }
            }
        }
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

    static Entitas.IMatcher<BattleEntity> _matcherEndTurn;

    public static Entitas.IMatcher<BattleEntity> EndTurn {
        get {
            if (_matcherEndTurn == null) {
                var matcher = (Entitas.Matcher<BattleEntity>)Entitas.Matcher<BattleEntity>.AllOf(BattleComponentsLookup.EndTurn);
                matcher.componentNames = BattleComponentsLookup.componentNames;
                _matcherEndTurn = matcher;
            }

            return _matcherEndTurn;
        }
    }
}