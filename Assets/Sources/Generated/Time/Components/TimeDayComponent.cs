//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class TimeEntity {

    public DayComponent day { get { return (DayComponent)GetComponent(TimeComponentsLookup.Day); } }
    public bool hasDay { get { return HasComponent(TimeComponentsLookup.Day); } }

    public void AddDay(float newValue) {
        var index = TimeComponentsLookup.Day;
        var component = CreateComponent<DayComponent>(index);
        component.value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceDay(float newValue) {
        var index = TimeComponentsLookup.Day;
        var component = CreateComponent<DayComponent>(index);
        component.value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveDay() {
        RemoveComponent(TimeComponentsLookup.Day);
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
public sealed partial class TimeMatcher {

    static Entitas.IMatcher<TimeEntity> _matcherDay;

    public static Entitas.IMatcher<TimeEntity> Day {
        get {
            if (_matcherDay == null) {
                var matcher = (Entitas.Matcher<TimeEntity>)Entitas.Matcher<TimeEntity>.AllOf(TimeComponentsLookup.Day);
                matcher.componentNames = TimeComponentsLookup.componentNames;
                _matcherDay = matcher;
            }

            return _matcherDay;
        }
    }
}