//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class LifeEntity {

    public CaloryComponent calory { get { return (CaloryComponent)GetComponent(LifeComponentsLookup.Calory); } }
    public bool hasCalory { get { return HasComponent(LifeComponentsLookup.Calory); } }

    public void AddCalory(int newValue) {
        var index = LifeComponentsLookup.Calory;
        var component = CreateComponent<CaloryComponent>(index);
        component.value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceCalory(int newValue) {
        var index = LifeComponentsLookup.Calory;
        var component = CreateComponent<CaloryComponent>(index);
        component.value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveCalory() {
        RemoveComponent(LifeComponentsLookup.Calory);
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
public sealed partial class LifeMatcher {

    static Entitas.IMatcher<LifeEntity> _matcherCalory;

    public static Entitas.IMatcher<LifeEntity> Calory {
        get {
            if (_matcherCalory == null) {
                var matcher = (Entitas.Matcher<LifeEntity>)Entitas.Matcher<LifeEntity>.AllOf(LifeComponentsLookup.Calory);
                matcher.componentNames = LifeComponentsLookup.componentNames;
                _matcherCalory = matcher;
            }

            return _matcherCalory;
        }
    }
}