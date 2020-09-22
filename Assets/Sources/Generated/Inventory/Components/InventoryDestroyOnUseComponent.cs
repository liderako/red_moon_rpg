//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class InventoryEntity {

    public DestroyOnUseComponent destroyOnUse { get { return (DestroyOnUseComponent)GetComponent(InventoryComponentsLookup.DestroyOnUse); } }
    public bool hasDestroyOnUse { get { return HasComponent(InventoryComponentsLookup.DestroyOnUse); } }

    public void AddDestroyOnUse(DestroyOnUseComponent newValue) {
        var index = InventoryComponentsLookup.DestroyOnUse;
        var component = CreateComponent<DestroyOnUseComponent>(index);
        component.value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceDestroyOnUse(DestroyOnUseComponent newValue) {
        var index = InventoryComponentsLookup.DestroyOnUse;
        var component = CreateComponent<DestroyOnUseComponent>(index);
        component.value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveDestroyOnUse() {
        RemoveComponent(InventoryComponentsLookup.DestroyOnUse);
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
public sealed partial class InventoryMatcher {

    static Entitas.IMatcher<InventoryEntity> _matcherDestroyOnUse;

    public static Entitas.IMatcher<InventoryEntity> DestroyOnUse {
        get {
            if (_matcherDestroyOnUse == null) {
                var matcher = (Entitas.Matcher<InventoryEntity>)Entitas.Matcher<InventoryEntity>.AllOf(InventoryComponentsLookup.DestroyOnUse);
                matcher.componentNames = InventoryComponentsLookup.componentNames;
                _matcherDestroyOnUse = matcher;
            }

            return _matcherDestroyOnUse;
        }
    }
}
