//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GridEntity {

    public RedMoonRPG.NameComponent name { get { return (RedMoonRPG.NameComponent)GetComponent(GridComponentsLookup.Name); } }
    public bool hasName { get { return HasComponent(GridComponentsLookup.Name); } }

    public void AddName(string newName) {
        var index = GridComponentsLookup.Name;
        var component = CreateComponent<RedMoonRPG.NameComponent>(index);
        component.name = newName;
        AddComponent(index, component);
    }

    public void ReplaceName(string newName) {
        var index = GridComponentsLookup.Name;
        var component = CreateComponent<RedMoonRPG.NameComponent>(index);
        component.name = newName;
        ReplaceComponent(index, component);
    }

    public void RemoveName() {
        RemoveComponent(GridComponentsLookup.Name);
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityInterfaceGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GridEntity : IName { }

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentMatcherGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed partial class GridMatcher {

    static Entitas.IMatcher<GridEntity> _matcherName;

    public static Entitas.IMatcher<GridEntity> Name {
        get {
            if (_matcherName == null) {
                var matcher = (Entitas.Matcher<GridEntity>)Entitas.Matcher<GridEntity>.AllOf(GridComponentsLookup.Name);
                matcher.componentNames = GridComponentsLookup.componentNames;
                _matcherName = matcher;
            }

            return _matcherName;
        }
    }
}