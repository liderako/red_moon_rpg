using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TGS;

public class TestBattleController : MonoBehaviour
{
    private Entitas.Systems _systems;
    [SerializeField] private GameObject Player;

    void Start()
    {
        Contexts contexts = Contexts.sharedInstance;
        _systems = CreateSystems(contexts);
        _systems.Initialize();
        InitTest();
    }

    private void Update()
    {
        if (_systems != null)
        {
            _systems.Execute();
        }
    }

    private void InitTest()
    {
        GridEntity avatar = Contexts.sharedInstance.grid.CreateEntity();
        avatar.AddActiveAvatar(true);
        avatar.AddActionPoint(5);
        avatar.AddMapPosition(new Position(Player.transform.position));
        avatar.AddTerrainGrid(TerrainGridSystem.instance);
    }

    private Entitas.Systems CreateSystems(Contexts contexts)
    {
        return new Feature("Grid")
        .Add(new RedMoonRPG.Systems.Battle.Grid.DisplayAvailableGridSystem(contexts));
    }
}
