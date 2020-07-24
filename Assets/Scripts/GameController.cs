using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Entitas;
using RedMoonRPG.Tags;


public class GameController : MonoBehaviour
{
    public GameObject _playerPrefab;

    public CameraSettings ct;
    public GameBalanceSettings gbt;

    private Systems _systems;

    private void Start()
    {
		Contexts contexts = Contexts.sharedInstance;

		_systems = CreateSystems(contexts);
        _systems.Initialize();
        initTest();
    }

    private void Update()
    {
        if (_systems != null)
        {
            _systems.Execute();
        }
    }

    private void OnDestroy()
    {
        GameContext game = Contexts.sharedInstance.game;;
        _systems.ClearReactiveSystems();
        game.GetEntityWithName(Tags.camera).Destroy();
        game.GetEntityWithName(Tags.playerAvatar).Destroy();
    }

    private void initTest()
    {
        TestInitPlayer();
        TestInitCamera();
    }

    private void TestInitPlayer()
    {
        GameEntity entity = Contexts.sharedInstance.game.CreateEntity();
        GameObject go = Instantiate(_playerPrefab);
        entity.AddNavMeshAgent(go.GetComponent<NavMeshAgent>());
        entity.AddName(Tags.playerAvatar);
        entity.AddMapPosition(new Position(Vector3.zero));
        entity.AddTransform(go.GetComponent<NavMeshAgent>().transform);
        entity.AddPersona("Lola");
    }

    private void TestInitCamera()
    {
        GameEntity cameraEntity = Contexts.sharedInstance.game.CreateEntity();
        cameraEntity.AddTransform(Camera.main.gameObject.transform);
        cameraEntity.AddName(Tags.camera);
        cameraEntity.AddSpeed(ct.speed);
        cameraEntity.AddForceSpeed(ct.ForceSpeed);
        cameraEntity.AddBorderThickness(ct.BorderThickness);
        cameraEntity.AddMapPosition(new Position(Vector3.zero));
    }

    private Systems CreateSystems(Contexts contexts)
	{
		return new Feature("Game")
        .Add(new Life.DamageSystem(contexts))
        .Add(new Life.HealSystem(contexts))
        .Add(new ModifyStatSystem(contexts))
        .Add(new WorldMap.Camera.CameraFollowerMovementSystem(contexts))
        .Add(new WorldMap.Player.MovementSystem(contexts))
        .Add(new WorldMap.Player.InputMovementSystem(contexts))
        .Add(new WorldMap.Camera.CameraMovementSystem(contexts))
        .Add(new WorldMap.Camera.ReturnMovementSystem(contexts))
        .Add(new UpdateHealthSystem(contexts, gbt));
    }
}
