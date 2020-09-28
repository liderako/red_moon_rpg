//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class CharacterEntity {

    public DefaultPriceComponent defaultPrice { get { return (DefaultPriceComponent)GetComponent(CharacterComponentsLookup.DefaultPrice); } }
    public bool hasDefaultPrice { get { return HasComponent(CharacterComponentsLookup.DefaultPrice); } }

    public void AddDefaultPrice(int newValue) {
        var index = CharacterComponentsLookup.DefaultPrice;
        var component = CreateComponent<DefaultPriceComponent>(index);
        component.value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceDefaultPrice(int newValue) {
        var index = CharacterComponentsLookup.DefaultPrice;
        var component = CreateComponent<DefaultPriceComponent>(index);
        component.value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveDefaultPrice() {
        RemoveComponent(CharacterComponentsLookup.DefaultPrice);
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

    static Entitas.IMatcher<CharacterEntity> _matcherDefaultPrice;

    public static Entitas.IMatcher<CharacterEntity> DefaultPrice {
        get {
            if (_matcherDefaultPrice == null) {
                var matcher = (Entitas.Matcher<CharacterEntity>)Entitas.Matcher<CharacterEntity>.AllOf(CharacterComponentsLookup.DefaultPrice);
                matcher.componentNames = CharacterComponentsLookup.componentNames;
                _matcherDefaultPrice = matcher;
            }

            return _matcherDefaultPrice;
        }
    }
}