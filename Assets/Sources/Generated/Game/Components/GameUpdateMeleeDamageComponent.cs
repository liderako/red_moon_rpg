//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    static readonly UpdateMeleeDamageComponent updateMeleeDamageComponent = new UpdateMeleeDamageComponent();

    public bool isUpdateMeleeDamage {
        get { return HasComponent(GameComponentsLookup.UpdateMeleeDamage); }
        set {
            if (value != isUpdateMeleeDamage) {
                var index = GameComponentsLookup.UpdateMeleeDamage;
                if (value) {
                    var componentPool = GetComponentPool(index);
                    var component = componentPool.Count > 0
                            ? componentPool.Pop()
                            : updateMeleeDamageComponent;

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
public sealed partial class GameMatcher {

    static Entitas.IMatcher<GameEntity> _matcherUpdateMeleeDamage;

    public static Entitas.IMatcher<GameEntity> UpdateMeleeDamage {
        get {
            if (_matcherUpdateMeleeDamage == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.UpdateMeleeDamage);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherUpdateMeleeDamage = matcher;
            }

            return _matcherUpdateMeleeDamage;
        }
    }
}
