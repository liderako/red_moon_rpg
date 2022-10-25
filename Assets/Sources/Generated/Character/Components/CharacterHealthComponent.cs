//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class CharacterEntity {

    public HealthComponent health { get { return (HealthComponent)GetComponent(CharacterComponentsLookup.Health); } }
    public bool hasHealth { get { return HasComponent(CharacterComponentsLookup.Health); } }

    public void AddHealth(int newValue, int newMaxValue) {
        var index = CharacterComponentsLookup.Health;
        var component = CreateComponent<HealthComponent>(index);
        component.value = newValue;
        component.maxValue = newMaxValue;
        AddComponent(index, component);
    }

    public void ReplaceHealth(int newValue, int newMaxValue) {
        var index = CharacterComponentsLookup.Health;
        var component = CreateComponent<HealthComponent>(index);
        component.value = newValue;
        component.maxValue = newMaxValue;
        ReplaceComponent(index, component);
    }

    public void RemoveHealth() {
        RemoveComponent(CharacterComponentsLookup.Health);
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

    static Entitas.IMatcher<CharacterEntity> _matcherHealth;

    public static Entitas.IMatcher<CharacterEntity> Health {
        get {
            if (_matcherHealth == null) {
                var matcher = (Entitas.Matcher<CharacterEntity>)Entitas.Matcher<CharacterEntity>.AllOf(CharacterComponentsLookup.Health);
                matcher.componentNames = CharacterComponentsLookup.componentNames;
                _matcherHealth = matcher;
            }

            return _matcherHealth;
        }
    }
}
