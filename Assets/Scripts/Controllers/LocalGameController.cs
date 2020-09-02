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
        public List<GameObject> _testEnemy;


        [SerializeField] private Transform _spawnPoint;


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
            TestInitEnemy();
        }

        private void TestInitPlayer()
        {
            TerrainGridSystem tgs = TerrainGridSystem.instance;
            GameEntity entity = Contexts.sharedInstance.game.CreateEntity();
            GameObject go = Instantiate(_playerPrefab);
            go.transform.position = tgs.CellGetPosition(tgs.CellGetAtPosition(_spawnPoint.position, true), true);
            //entity.AddNavMeshAgent(go.GetComponent<NavMeshAgent>());
            entity.AddName(Tags.playerAvatar);
            entity.AddMapPosition(new Position(go.transform.position));
            entity.AddTransform(go.transform);
            entity.AddAnimator(go.GetComponent<Animator>());
            entity.AddActiveAnimation(AnimationTags.idle);
            entity.AddNextAnimation(AnimationTags.idle);
            entity.AddPersona("Antonio");
            entity.AddDexterity(5);
            entity.AddActiveAvatar(true);
            entity.isPlayer = true;

            // создаем клетку для игрока
            GridEntity avatar = Contexts.sharedInstance.grid.CreateEntity();
            avatar.AddActionPoint(0);
            avatar.AddMapPosition(new Position(entity.transform.value.position));
            tgs.CellGetAtPosition(_spawnPoint.position, true).canCross = false; // делаем текущию клетку героя непроходимой для других
            avatar.AddTerrainGrid(TerrainGridSystem.instance);
            avatar.AddName(entity.name.name);
            avatar.AddPath(new List<int>(), 0);
            avatar.AddSpeed(2);
            avatar.AddRotateSpeed(5);
            avatar.AddActiveAvatar(true);
            avatar.isPlayer = true;

            //GameEntity camera = Contexts.sharedInstance.game.GetEntityWithName(Tags.camera);
            //camera.isWorldMap = false;
            // to do нужно чтобы не было никаких конфликтов между управлением камер в локал и глоб картах
        }

        private void TestInitEnemy()
        {
            TerrainGridSystem tgs = TerrainGridSystem.instance;
            // создаем гейм сущности для врагов
            for (int i = 0; i < _testEnemy.Count; i++)
            {
                GameEntity entity = Contexts.sharedInstance.game.CreateEntity();
                _testEnemy[i].transform.position = tgs.CellGetPosition(tgs.CellGetAtPosition(_testEnemy[i].transform.position, true), true);
                entity.AddAnimator(_testEnemy[i].GetComponent<Animator>());
                entity.AddTransform(_testEnemy[i].transform);
                entity.AddActiveAnimation(AnimationTags.idle);
                entity.AddNextAnimation(AnimationTags.idle);
                entity.AddPersona(_testEnemy[i].name);
                entity.AddName(_testEnemy[i].name + i.ToString());
                entity.AddActiveAvatar(true);
                entity.AddDexterity(4);

                // создаем клетки для врагов
                GridEntity avatar = Contexts.sharedInstance.grid.CreateEntity();
                avatar.AddActionPoint(0);
                avatar.AddMapPosition(new Position(entity.transform.value.position));
                tgs.CellGetAtPosition(entity.transform.value.position, true).canCross = false; // делаем текущию клетку врага непроходимой для других
                avatar.AddTerrainGrid(TerrainGridSystem.instance);
                avatar.AddName(entity.name.name);
                avatar.AddPath(new List<int>(), 0);
                avatar.AddSpeed(2);
                avatar.AddRotateSpeed(5);
                avatar.AddActiveAvatar(true);
            }
        }

        private Entitas.Systems CreateSystems(Contexts contexts)
        {
            return new Feature("Game")
            .Add(new Systems.WorldMap.Camera.CameraFollowerMovementSystem(contexts))
            .Add(new Systems.WorldMap.Camera.MovementSystem(contexts))
            .Add(new Systems.WorldMap.Camera.ReturnMovementSystem(contexts))
            .Add(new Systems.WorldMap.Camera.TeleportSystem(contexts))
            .Add(new Systems.Animations.BoolAnimationSystem(contexts))
            .Add(new Systems.LocalMap.Player.Battle.InputMovementSystem(contexts))
            .Add(new Systems.LocalMap.Player.InputMovementSystem(contexts))
            .Add(new Systems.LocalMap.Player.MovementSystem(contexts))
            .Add(new Systems.LocalMap.Player.Battle.BattleMovementSystem(contexts));
        }
    }
}