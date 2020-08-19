using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TGS;
using Entitas;
using RedMoonRPG.Utils;
using UnityEngine.SceneManagement;


namespace RedMoonRPG
{
    public class LocalGameController : MonoBehaviour
    {
        public GameObject _playerPrefab;

        public GameBalanceSettings gameBalanceSettings;

        private Entitas.Systems _systems;

        private void Start()
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

        private void OnDestroy()
        {
            GameContext game = Contexts.sharedInstance.game;
            _systems.ClearReactiveSystems();
            game.GetEntityWithName(Tags.camera).Destroy();
            game.GetEntityWithName(Tags.playerAvatar).Destroy(); // TO DO не нужно удалять это измениться с загрузкой персонажей
        }

        private void InitTest()
        {
            GameEntity gameInitializer = Contexts.sharedInstance.game.GetEntityWithName("GameInit");
            gameInitializer.isCameraCreate = true;
            gameInitializer.isLevelCreate = true;

            TestInitPlayer();
        }

        //private void initTest()
        //{
        //    GameEntity gameInitializer = Contexts.sharedInstance.game.GetEntityWithName("GameInit");
        //    TestInitPlayer();
        //    gameInitializer.isCameraCreate = true;
        //    gameInitializer.isLevelCreate = true;
        //}

        [SerializeField] private Transform _spawnPoint;

        private void TestInitPlayer()
        {
            GameEntity entity = Contexts.sharedInstance.game.CreateEntity();
            GameObject go = Instantiate(_playerPrefab);
            go.transform.position = _spawnPoint.position;
            entity.AddNavMeshAgent(go.GetComponent<NavMeshAgent>());
            entity.AddName(Tags.playerAvatar);
            entity.AddMapPosition(new Position(go.transform.position));
            entity.AddTransform(go.GetComponent<NavMeshAgent>().transform);
            entity.AddAnimator(go.GetComponent<Animator>());
            entity.AddActiveAnimation(AnimationTags.idle);
            entity.AddNextAnimation(AnimationTags.idle);
            entity.AddActiveAvatar(true);
            entity.AddPersona("Lola");

            GridEntity avatar = Contexts.sharedInstance.grid.CreateEntity();
            avatar.AddActiveAvatar(true);
            avatar.AddActionPoint(5);
            avatar.AddMapPosition(new Position(entity.transform.value.position));
            avatar.AddTerrainGrid(TerrainGridSystem.instance);
            avatar.AddName(entity.name.name);
            avatar.AddPath(new List<int>(), 0);
            avatar.AddSpeed(2);
            avatar.AddRotateSpeed(5);


            //GameEntity camera = Contexts.sharedInstance.game.GetEntityWithName(Tags.camera);
            //camera.isWorldMap = false;
            // to do нужно чтобы не было никаких конфликтов между управлением камер в локал и глоб картах
        }

        private Entitas.Systems CreateSystems(Contexts contexts)
        {
            return new Feature("Game")
            .Add(new Systems.WorldMap.Camera.CameraFollowerMovementSystem(contexts))
            .Add(new Systems.WorldMap.Camera.MovementSystem(contexts))
            .Add(new Systems.WorldMap.Camera.ReturnMovementSystem(contexts))
            .Add(new Systems.WorldMap.Camera.TeleportSystem(contexts))
            .Add(new Systems.Animations.BoolAnimationSystem(contexts))
            .Add(new Systems.LocalMap.Player.InputMovementSystem())
            .Add(new Systems.LocalMap.Player.MovementSystem(contexts));
        }
    }
}