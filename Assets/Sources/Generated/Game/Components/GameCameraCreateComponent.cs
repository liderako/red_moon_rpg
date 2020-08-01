//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    static readonly CameraCreateComponent cameraCreateComponent = new CameraCreateComponent();

    public bool isCameraCreate {
        get { return HasComponent(GameComponentsLookup.CameraCreate); }
        set {
            if (value != isCameraCreate) {
                var index = GameComponentsLookup.CameraCreate;
                if (value) {
                    var componentPool = GetComponentPool(index);
                    var component = componentPool.Count > 0
                            ? componentPool.Pop()
                            : cameraCreateComponent;

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
public sealed partial class GameMatcher {

    static Entitas.IMatcher<GameEntity> _matcherCameraCreate;

    public static Entitas.IMatcher<GameEntity> CameraCreate {
        get {
            if (_matcherCameraCreate == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.CameraCreate);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherCameraCreate = matcher;
            }

            return _matcherCameraCreate;
        }
    }
}
