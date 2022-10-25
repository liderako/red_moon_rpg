//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class CharacterEntity {

    public IndexMeleeDamageComponent indexMeleeDamage { get { return (IndexMeleeDamageComponent)GetComponent(CharacterComponentsLookup.IndexMeleeDamage); } }
    public bool hasIndexMeleeDamage { get { return HasComponent(CharacterComponentsLookup.IndexMeleeDamage); } }

    public void AddIndexMeleeDamage(int newMinValue, int newMaxValue) {
        var index = CharacterComponentsLookup.IndexMeleeDamage;
        var component = CreateComponent<IndexMeleeDamageComponent>(index);
        component.minValue = newMinValue;
        component.maxValue = newMaxValue;
        AddComponent(index, component);
    }

    public void ReplaceIndexMeleeDamage(int newMinValue, int newMaxValue) {
        var index = CharacterComponentsLookup.IndexMeleeDamage;
        var component = CreateComponent<IndexMeleeDamageComponent>(index);
        component.minValue = newMinValue;
        component.maxValue = newMaxValue;
        ReplaceComponent(index, component);
    }

    public void RemoveIndexMeleeDamage() {
        RemoveComponent(CharacterComponentsLookup.IndexMeleeDamage);
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

    static Entitas.IMatcher<CharacterEntity> _matcherIndexMeleeDamage;

    public static Entitas.IMatcher<CharacterEntity> IndexMeleeDamage {
        get {
            if (_matcherIndexMeleeDamage == null) {
                var matcher = (Entitas.Matcher<CharacterEntity>)Entitas.Matcher<CharacterEntity>.AllOf(CharacterComponentsLookup.IndexMeleeDamage);
                matcher.componentNames = CharacterComponentsLookup.componentNames;
                _matcherIndexMeleeDamage = matcher;
            }

            return _matcherIndexMeleeDamage;
        }
    }
}
