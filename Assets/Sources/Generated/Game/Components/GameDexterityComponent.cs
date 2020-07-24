//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public DexterityComponent dexterity { get { return (DexterityComponent)GetComponent(GameComponentsLookup.Dexterity); } }
    public bool hasDexterity { get { return HasComponent(GameComponentsLookup.Dexterity); } }

    public void AddDexterity(int newValue) {
        var index = GameComponentsLookup.Dexterity;
        var component = CreateComponent<DexterityComponent>(index);
        component.value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceDexterity(int newValue) {
        var index = GameComponentsLookup.Dexterity;
        var component = CreateComponent<DexterityComponent>(index);
        component.value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveDexterity() {
        RemoveComponent(GameComponentsLookup.Dexterity);
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

    static Entitas.IMatcher<GameEntity> _matcherDexterity;

    public static Entitas.IMatcher<GameEntity> Dexterity {
        get {
            if (_matcherDexterity == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.Dexterity);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherDexterity = matcher;
            }

            return _matcherDexterity;
        }
    }
}