using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Entitas;


public class GameController : MonoBehaviour
{
    public GameObject _playerPrefab;
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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _systems = CreateWorldMapSystems(Contexts.sharedInstance);
            return ;
        }
        _systems.Execute();
    }

    private void FixedUpdate()
    {
        _systems.Execute();
    }

    private void initTest()
    {
        TestInitPlayer();
        TestInitCamera();
        TestLevelEntity();
    }

    private void TestLevelEntity()
    {
        GameEntity level = Contexts.sharedInstance.game.CreateEntity();
        level.AddName("Level");
        level.AddLimitMap(new Vector2(-30, 30), new Vector2(20, 45), new Vector2(-40, 40));
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
        cameraEntity.AddTransform(Camera.main.gameObject.transform);
        cameraEntity.AddName("Camera");
        cameraEntity.AddSpeed(6);
        cameraEntity.AddForceSpeed(12);
        cameraEntity.AddBorderThickness(10);
    }

    private Systems CreateWorldMapSystems(Contexts contexts)
	{
		return new Feature("Game")
        .Add(new WorldMap.MovementSystem(contexts))
        .Add(new WorldMap.InputMovementSystem(contexts));
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
