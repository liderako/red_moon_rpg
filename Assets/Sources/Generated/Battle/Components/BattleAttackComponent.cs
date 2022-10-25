//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class BattleEntity {

    static readonly AttackComponent attackComponent = new AttackComponent();

    public bool isAttack {
        get { return HasComponent(BattleComponentsLookup.Attack); }
        set {
            if (value != isAttack) {
                var index = BattleComponentsLookup.Attack;
                if (value) {
                    var componentPool = GetComponentPool(index);
                    var component = componentPool.Count > 0
                            ? componentPool.Pop()
                            : attackComponent;

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

    static Entitas.IMatcher<BattleEntity> _matcherAttack;

    public static Entitas.IMatcher<BattleEntity> Attack {
        get {
            if (_matcherAttack == null) {
                var matcher = (Entitas.Matcher<BattleEntity>)Entitas.Matcher<BattleEntity>.AllOf(BattleComponentsLookup.Attack);
                matcher.componentNames = BattleComponentsLookup.componentNames;
                _matcherAttack = matcher;
            }

            return _matcherAttack;
        }
    }
}
