//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class CharacterEntity {

    static readonly UpdateRangedDamageComponent updateRangedDamageComponent = new UpdateRangedDamageComponent();

    public bool isUpdateRangedDamage {
        get { return HasComponent(CharacterComponentsLookup.UpdateRangedDamage); }
        set {
            if (value != isUpdateRangedDamage) {
                var index = CharacterComponentsLookup.UpdateRangedDamage;
                if (value) {
                    var componentPool = GetComponentPool(index);
                    var component = componentPool.Count > 0
                            ? componentPool.Pop()
                            : updateRangedDamageComponent;

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
public sealed partial class CharacterMatcher {

    static Entitas.IMatcher<CharacterEntity> _matcherUpdateRangedDamage;

    public static Entitas.IMatcher<CharacterEntity> UpdateRangedDamage {
        get {
            if (_matcherUpdateRangedDamage == null) {
                var matcher = (Entitas.Matcher<CharacterEntity>)Entitas.Matcher<CharacterEntity>.AllOf(CharacterComponentsLookup.UpdateRangedDamage);
                matcher.componentNames = CharacterComponentsLookup.componentNames;
                _matcherUpdateRangedDamage = matcher;
            }

            return _matcherUpdateRangedDamage;
        }
    }
}