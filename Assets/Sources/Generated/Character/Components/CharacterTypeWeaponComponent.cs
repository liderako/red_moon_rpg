//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class CharacterEntity {

    public TypeWeaponComponent typeWeapon { get { return (TypeWeaponComponent)GetComponent(CharacterComponentsLookup.TypeWeapon); } }
    public bool hasTypeWeapon { get { return HasComponent(CharacterComponentsLookup.TypeWeapon); } }

    public void AddTypeWeapon(WeaponType newValue) {
        var index = CharacterComponentsLookup.TypeWeapon;
        var component = CreateComponent<TypeWeaponComponent>(index);
        component.value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceTypeWeapon(WeaponType newValue) {
        var index = CharacterComponentsLookup.TypeWeapon;
        var component = CreateComponent<TypeWeaponComponent>(index);
        component.value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveTypeWeapon() {
        RemoveComponent(CharacterComponentsLookup.TypeWeapon);
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

    static Entitas.IMatcher<CharacterEntity> _matcherTypeWeapon;

    public static Entitas.IMatcher<CharacterEntity> TypeWeapon {
        get {
            if (_matcherTypeWeapon == null) {
                var matcher = (Entitas.Matcher<CharacterEntity>)Entitas.Matcher<CharacterEntity>.AllOf(CharacterComponentsLookup.TypeWeapon);
                matcher.componentNames = CharacterComponentsLookup.componentNames;
                _matcherTypeWeapon = matcher;
            }

            return _matcherTypeWeapon;
        }
    }
}
