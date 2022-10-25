//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class CharacterEntity {

    public LuckComponent luck { get { return (LuckComponent)GetComponent(CharacterComponentsLookup.Luck); } }
    public bool hasLuck { get { return HasComponent(CharacterComponentsLookup.Luck); } }

    public void AddLuck(int newValue) {
        var index = CharacterComponentsLookup.Luck;
        var component = CreateComponent<LuckComponent>(index);
        component.value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceLuck(int newValue) {
        var index = CharacterComponentsLookup.Luck;
        var component = CreateComponent<LuckComponent>(index);
        component.value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveLuck() {
        RemoveComponent(CharacterComponentsLookup.Luck);
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

    static Entitas.IMatcher<CharacterEntity> _matcherLuck;

    public static Entitas.IMatcher<CharacterEntity> Luck {
        get {
            if (_matcherLuck == null) {
                var matcher = (Entitas.Matcher<CharacterEntity>)Entitas.Matcher<CharacterEntity>.AllOf(CharacterComponentsLookup.Luck);
                matcher.componentNames = CharacterComponentsLookup.componentNames;
                _matcherLuck = matcher;
            }

            return _matcherLuck;
        }
    }
}
