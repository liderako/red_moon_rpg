//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class CharacterEntity {

    public IntellectComponent intellect { get { return (IntellectComponent)GetComponent(CharacterComponentsLookup.Intellect); } }
    public bool hasIntellect { get { return HasComponent(CharacterComponentsLookup.Intellect); } }

    public void AddIntellect(int newValue) {
        var index = CharacterComponentsLookup.Intellect;
        var component = CreateComponent<IntellectComponent>(index);
        component.value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceIntellect(int newValue) {
        var index = CharacterComponentsLookup.Intellect;
        var component = CreateComponent<IntellectComponent>(index);
        component.value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveIntellect() {
        RemoveComponent(CharacterComponentsLookup.Intellect);
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

    static Entitas.IMatcher<CharacterEntity> _matcherIntellect;

    public static Entitas.IMatcher<CharacterEntity> Intellect {
        get {
            if (_matcherIntellect == null) {
                var matcher = (Entitas.Matcher<CharacterEntity>)Entitas.Matcher<CharacterEntity>.AllOf(CharacterComponentsLookup.Intellect);
                matcher.componentNames = CharacterComponentsLookup.componentNames;
                _matcherIntellect = matcher;
            }

            return _matcherIntellect;
        }
    }
}
