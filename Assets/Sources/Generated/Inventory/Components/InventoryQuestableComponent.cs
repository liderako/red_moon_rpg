//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class InventoryEntity {

    static readonly QuestableComponent questableComponent = new QuestableComponent();

    public bool isQuestable {
        get { return HasComponent(InventoryComponentsLookup.Questable); }
        set {
            if (value != isQuestable) {
                var index = InventoryComponentsLookup.Questable;
                if (value) {
                    var componentPool = GetComponentPool(index);
                    var component = componentPool.Count > 0
                            ? componentPool.Pop()
                            : questableComponent;

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
public sealed partial class InventoryMatcher {

    static Entitas.IMatcher<InventoryEntity> _matcherQuestable;

    public static Entitas.IMatcher<InventoryEntity> Questable {
        get {
            if (_matcherQuestable == null) {
                var matcher = (Entitas.Matcher<InventoryEntity>)Entitas.Matcher<InventoryEntity>.AllOf(InventoryComponentsLookup.Questable);
                matcher.componentNames = InventoryComponentsLookup.componentNames;
                _matcherQuestable = matcher;
            }

            return _matcherQuestable;
        }
    }
}
