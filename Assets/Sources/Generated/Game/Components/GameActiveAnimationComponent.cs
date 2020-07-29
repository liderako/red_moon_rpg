//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public ActiveAnimationComponent activeAnimation { get { return (ActiveAnimationComponent)GetComponent(GameComponentsLookup.ActiveAnimation); } }
    public bool hasActiveAnimation { get { return HasComponent(GameComponentsLookup.ActiveAnimation); } }

    public void AddActiveAnimation(string newName) {
        var index = GameComponentsLookup.ActiveAnimation;
        var component = CreateComponent<ActiveAnimationComponent>(index);
        component.name = newName;
        AddComponent(index, component);
    }

    public void ReplaceActiveAnimation(string newName) {
        var index = GameComponentsLookup.ActiveAnimation;
        var component = CreateComponent<ActiveAnimationComponent>(index);
        component.name = newName;
        ReplaceComponent(index, component);
    }

    public void RemoveActiveAnimation() {
        RemoveComponent(GameComponentsLookup.ActiveAnimation);
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
public sealed partial class GameMatcher {

    static Entitas.IMatcher<GameEntity> _matcherActiveAnimation;

    public static Entitas.IMatcher<GameEntity> ActiveAnimation {
        get {
            if (_matcherActiveAnimation == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.ActiveAnimation);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherActiveAnimation = matcher;
            }

            return _matcherActiveAnimation;
        }
    }
}