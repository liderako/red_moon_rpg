using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Entitas;
using RedMoonRPG.Utils;
using UnityEngine.SceneManagement;


namespace RedMoonRPG
{
    public class GameController : MonoBehaviour
    {
        public GameObject _playerPrefab;

        public GameBalanceSettings gameBalanceSettings;

        private Entitas.Systems _systems;

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
            GameContext game = Contexts.sharedInstance.game;
            _systems.ClearReactiveSystems();
            game.GetEntityWithName(Tags.camera).Destroy();
            game.GetEntityWithName(Tags.playerAvatar).Destroy();
        }

        private void initTest()
        {
            GameEntity gameInitializer = Contexts.sharedInstance.game.GetEntityWithName("GameInit");
            TestInitPlayer();
            gameInitializer.isCameraCreate = true;
            gameInitializer.isLevelCreate = true;
        }

        private void TestInitPlayer()
        {
            GameEntity entity = Contexts.sharedInstance.game.CreateEntity();

            GameEntity entity1 = Contexts.sharedInstance.game.CreateEntity();
            entity1.AddActiveAvatar(false);
            GameEntity entity2 = Contexts.sharedInstance.game.CreateEntity();
            entity2.AddActiveAvatar(false);
            GameObject go = Instantiate(_playerPrefab);
            entity.AddNavMeshAgent(go.GetComponent<NavMeshAgent>());
            entity.AddName(Tags.playerAvatar);
            entity.AddMapPosition(new Position(Vector3.zero));
            entity.AddTransform(go.GetComponent<NavMeshAgent>().transform);
            entity.AddAnimator(go.GetComponent<Animator>());
            entity.AddActiveAnimation(AnimationTags.idle);
            entity.AddNextAnimation(AnimationTags.idle);
            entity.AddActiveAvatar(true);
            entity.AddPersona("Lola");
        }

        //private Entitas.Systems CreateSystems(Contexts contexts)
        //{
        //    return new Feature("Game")
        //    .Add(new Systems.Life.DamageSystem(contexts))
        //    .Add(new Systems.Life.HealSystem(contexts))
        //    .Add(new Systems.WorldMap.Camera.CameraFollowerMovementSystem(contexts))
        //    .Add(new Systems.WorldMap.Camera.MovementSystem(contexts))
        //    .Add(new Systems.WorldMap.Camera.ReturnMovementSystem(contexts))
        //    .Add(new Systems.WorldMap.Camera.TeleportSystem(contexts))
        //    .Add(new Systems.WorldMap.Player.MovementSystem(contexts))
        //    .Add(new Systems.WorldMap.Player.InputMovementSystem(contexts))
        //    .Add(new Systems.Animations.BoolAnimationSystem(contexts))
        //    .Add(new Systems.ModifyStatSystem(contexts))
        //    .Add(new Systems.UpdateHealthSystem(contexts, gameBalanceSettings));
        //}
        private Entitas.Systems CreateSystems(Contexts contexts)
        {
            return new Feature("Game")
            .Add(new Systems.WorldMap.Camera.CameraFollowerMovementSystem(contexts))
            .Add(new Systems.WorldMap.Camera.MovementSystem(contexts))
            .Add(new Systems.WorldMap.Camera.ReturnMovementSystem(contexts))
            .Add(new Systems.WorldMap.Camera.TeleportSystem(contexts))
            .Add(new Systems.WorldMap.Player.MovementSystem(contexts))
            .Add(new Systems.WorldMap.Player.InputMovementSystem(contexts))
            .Add(new Systems.Animations.BoolAnimationSystem(contexts));
        }
    }
}