using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Entitas;


public class GameController : MonoBehaviour
{
    public GameObject _playerPrefab;
    private Systems _systems;

    [SerializeField] private Camera _mainCamera;

    private void Start()
    {
		Contexts contexts = Contexts.sharedInstance;

		_systems = CreateSystems(contexts);
        _systems.Initialize();

        initTest();
        Debug.Log("Start");
        // DontDestroyOnLoad(gameObject);
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
        Debug.Log("Destroy");
        GameContext game = Contexts.sharedInstance.game;;
        // game.DestroyAllEntities();
        _systems.ClearReactiveSystems();
        game.GetEntityWithName("Camera").Destroy();
        game.GetEntityWithName("PlayerModel").Destroy();
    }

    // private void FixedUpdate()
    // {
    //     _systems.Execute();
    // }

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
        entity.AddName("PlayerModel");
        entity.AddMapPosition(new Position(Vector3.zero));
        entity.AddTransform(go.GetComponent<NavMeshAgent>().transform);
        entity.AddPersona("Lola");
    }

    private void TestInitCamera()
    {
        GameEntity cameraEntity = Contexts.sharedInstance.game.CreateEntity();
        cameraEntity.AddTransform(_mainCamera.gameObject.transform);
        cameraEntity.AddName("Camera");
        cameraEntity.AddSpeed(6);
        cameraEntity.AddForceSpeed(12);
        cameraEntity.AddBorderThickness(10);
    }

    private Systems CreateSystems(Contexts contexts)
	{
		return new Feature("Game")
        .Add(new DamageSystem(contexts))
        .Add(new HealSystem(contexts))
        .Add(new ModifyStatSystem(contexts))
        .Add(new WorldMap.CameraFollowerMovementSystem(contexts))
        .Add(new WorldMap.MovementSystem(contexts))
        .Add(new WorldMap.InputMovementSystem(contexts))
        .Add(new WorldMap.CameraMovementSystem(contexts))
        .Add(new UpdateHealthSystem(contexts));
    }
}
