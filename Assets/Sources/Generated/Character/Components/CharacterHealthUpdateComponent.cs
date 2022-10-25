//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class CharacterEntity {

    static readonly HealthUpdateComponent healthUpdateComponent = new HealthUpdateComponent();

    public bool isHealthUpdate {
        get { return HasComponent(CharacterComponentsLookup.HealthUpdate); }
        set {
            if (value != isHealthUpdate) {
                var index = CharacterComponentsLookup.HealthUpdate;
                if (value) {
                    var componentPool = GetComponentPool(index);
                    var component = componentPool.Count > 0
                            ? componentPool.Pop()
                            : healthUpdateComponent;

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

    static Entitas.IMatcher<CharacterEntity> _matcherHealthUpdate;

    public static Entitas.IMatcher<CharacterEntity> HealthUpdate {
        get {
            if (_matcherHealthUpdate == null) {
                var matcher = (Entitas.Matcher<CharacterEntity>)Entitas.Matcher<CharacterEntity>.AllOf(CharacterComponentsLookup.HealthUpdate);
                matcher.componentNames = CharacterComponentsLookup.componentNames;
                _matcherHealthUpdate = matcher;
            }

            return _matcherHealthUpdate;
        }
    }
}
