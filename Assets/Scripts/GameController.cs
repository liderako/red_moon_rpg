using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Entitas;
using RedMoonRPG.Utils;


namespace RedMoonRPG
{
    public class GameController : MonoBehaviour
    {
        public GameObject _playerPrefab;

        public CameraSettings cameraSettings;
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
            GameContext game = Contexts.sharedInstance.game; ;
            _systems.ClearReactiveSystems();
            game.GetEntityWithName(Tags.camera).Destroy();
            game.GetEntityWithName(Tags.playerAvatar).Destroy();
        }

        private void initTest()
        {
            TestInitPlayer();
            TestInitCamera();
            TestInitLevel();
        }

        private void TestInitLevel()
        {
            GameEntity level = Contexts.sharedInstance.game.GetEntityWithName(Tags.level);
            if (level.hasLimitMap)
            {
                level.ReplaceLimitMap(new Vector2(-30, 30), new Vector2(20, 45), new Vector2(-40, 40));
            }
            else
            {
                level.AddLimitMap(new Vector2(-30, 30), new Vector2(20, 45), new Vector2(-40, 40));
            }
        }

        private void TestInitPlayer()
        {
            GameEntity entity = Contexts.sharedInstance.game.CreateEntity();
            GameObject go = Instantiate(_playerPrefab);
            entity.AddNavMeshAgent(go.GetComponent<NavMeshAgent>());
            entity.AddName(Tags.playerAvatar);
            entity.AddMapPosition(new Position(Vector3.zero));
            entity.AddTransform(go.GetComponent<NavMeshAgent>().transform);
            entity.AddAnimator(go.GetComponent<Animator>());
            entity.AddActiveAnimation(AnimationTags.idle);
            entity.AddNextAnimation(AnimationTags.idle);
            entity.AddPersona("Lola");
        }

        private void TestInitCamera()
        {
            GameEntity cameraEntity = Contexts.sharedInstance.game.CreateEntity();
            cameraEntity.AddTransform(Camera.main.gameObject.transform);
            cameraEntity.AddName(Tags.camera);
            cameraEntity.AddSpeed(cameraSettings.speed);
            cameraEntity.AddForceSpeed(cameraSettings.ForceSpeed);
            cameraEntity.AddBorderThickness(cameraSettings.BorderThickness);
            cameraEntity.AddMapPosition(new Position(Vector3.zero));
        }

        private Entitas.Systems CreateSystems(Contexts contexts)
        {
            return new Feature("Game")
            .Add(new Systems.Life.DamageSystem(contexts))
            .Add(new Systems.Life.HealSystem(contexts))
            .Add(new Systems.WorldMap.Camera.CameraFollowerMovementSystem(contexts))
            .Add(new Systems.WorldMap.Camera.MovementSystem(contexts))
            .Add(new Systems.WorldMap.Camera.ReturnMovementSystem(contexts))
            .Add(new Systems.WorldMap.Player.MovementSystem(contexts))
            .Add(new Systems.WorldMap.Player.InputMovementSystem(contexts))
            .Add(new Systems.Animations.BoolAnimationSystem(contexts))
            .Add(new Systems.ModifyStatSystem(contexts))
            .Add(new Systems.UpdateHealthSystem(contexts, gameBalanceSettings));
        }
    }
}