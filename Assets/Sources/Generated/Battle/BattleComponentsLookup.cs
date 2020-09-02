//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentsLookupGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public static class BattleComponentsLookup {

    public const int AwakeBattle = 0;
    public const int BattleList = 1;
    public const int Name = 2;
    public const int Round = 3;
    public const int SkipTurn = 4;
    public const int UpdateActiveAvatar = 5;
    public const int UpdateBattleRound = 6;

    public const int TotalComponents = 7;

    public static readonly string[] componentNames = {
        "AwakeBattle",
        "BattleList",
        "Name",
        "Round",
        "SkipTurn",
        "UpdateActiveAvatar",
        "UpdateBattleRound"
    };

    public static readonly System.Type[] componentTypes = {
        typeof(AwakeBattleComponent),
        typeof(BattleListComponent),
        typeof(RedMoonRPG.NameComponent),
        typeof(RoundComponent),
        typeof(SkipTurnComponent),
        typeof(UpdateActiveAvatarComponent),
        typeof(UpdateBattleRound)
    };
}
