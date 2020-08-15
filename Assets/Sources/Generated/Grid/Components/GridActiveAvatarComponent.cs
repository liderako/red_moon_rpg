//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GridEntity {

    public ActiveAvatarComponent activeAvatar { get { return (ActiveAvatarComponent)GetComponent(GridComponentsLookup.ActiveAvatar); } }
    public bool hasActiveAvatar { get { return HasComponent(GridComponentsLookup.ActiveAvatar); } }

    public void AddActiveAvatar(bool newValue) {
        var index = GridComponentsLookup.ActiveAvatar;
        var component = CreateComponent<ActiveAvatarComponent>(index);
        component.value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceActiveAvatar(bool newValue) {
        var index = GridComponentsLookup.ActiveAvatar;
        var component = CreateComponent<ActiveAvatarComponent>(index);
        component.value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveActiveAvatar() {
        RemoveComponent(GridComponentsLookup.ActiveAvatar);
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
public partial class GridEntity : IActiveAvatar { }

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentMatcherGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed partial class GridMatcher {

    static Entitas.IMatcher<GridEntity> _matcherActiveAvatar;

    public static Entitas.IMatcher<GridEntity> ActiveAvatar {
        get {
            if (_matcherActiveAvatar == null) {
                var matcher = (Entitas.Matcher<GridEntity>)Entitas.Matcher<GridEntity>.AllOf(GridComponentsLookup.ActiveAvatar);
                matcher.componentNames = GridComponentsLookup.componentNames;
                _matcherActiveAvatar = matcher;
            }

            return _matcherActiveAvatar;
        }
    }
}
