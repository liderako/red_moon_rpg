﻿using System.Collections;
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
            entity.AddPersona("Lola");
            entity.AddActiveAvatar(true);

            GridEntity avatar = Contexts.sharedInstance.grid.CreateEntity();
            avatar.AddActionPoint(5);
            avatar.AddMapPosition(new Position(entity.transform.value.position));
            avatar.AddTerrainGrid(TerrainGridSystem.instance);
            avatar.AddName(entity.name.name);
            avatar.AddPath(new List<int>(), 0);
            avatar.AddSpeed(2);
            avatar.AddRotateSpeed(5);
            avatar.AddActiveAvatar(true);

            //GameEntity camera = Contexts.sharedInstance.game.GetEntityWithName(Tags.camera);
            //camera.isWorldMap = false;
            // to do нужно чтобы не было никаких конфликтов между управлением камер в локал и глоб картах
        }

        public void FixedUpdate()
        {
            for (int i = 0; i < _testEnemy.Count; i++)
            {
                //Debug.Log(TerrainGridSystem.instance.CellGetAtPosition(Vector3.zero));
            }
        }

        //void SnapToCellCenter()
        //{
        //    Vector3 pos = tgs.SnapToCell(character.transform.position);
        //    character.transform.position = pos + Vector3.up;
        //}

        private void TestInitEnemy()
        {
            TerrainGridSystem tgs = TerrainGridSystem.instance;
            for (int i = 0; i < _testEnemy.Count; i++)
            {
                GameEntity entity = Contexts.sharedInstance.game.CreateEntity();
                _testEnemy[i].transform.position = tgs.CellGetPosition(tgs.CellGetAtPosition(_testEnemy[i].transform.position, true), true);
                entity.AddAnimator(_testEnemy[i].GetComponent<Animator>());
                entity.AddActiveAnimation(AnimationTags.idle);
                entity.AddNextAnimation(AnimationTags.idle);
                entity.AddPersona(_testEnemy[i].name);
                entity.AddActiveAvatar(false);
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
            .Add(new Systems.LocalMap.Player.InputMovementSystem())
            .Add(new Systems.LocalMap.Player.MovementSystem(contexts))
            .Add(new Systems.Battle.Grid.AwakeDisplayGridSystem(contexts))
            .Add(new Systems.Battle.Grid.DisplayAvailableGridSystem(contexts));
        }
    }
}