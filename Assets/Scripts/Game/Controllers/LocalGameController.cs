using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TGS;
using Entitas;
using RedMoonRPG.Systems;
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
//            GameEntity gameInitializer = Contexts.sharedInstance.game.GetEntityWithName("GameInit"); // TO Do зачем нужен GameInit

            TestInitPlayer();
            TestInitEnemy();
        }
    
        private void TestInitPlayer()
        {
            TerrainGridSystem tgs = TerrainGridSystem.instance;
            GameEntity entity = Contexts.sharedInstance.game.CreateEntity();
            GameObject go = Instantiate(_playerPrefab);
            go.transform.position = tgs.CellGetPosition(tgs.CellGetAtPosition(_spawnPoint.position, true), true);
            //entity.AddNavMeshAgent(go.GetComponent<NavMeshAgent>()); // удаляем nav mesh потому чтоо не используем его на локальной карте
            entity.AddName(Tags.playerAvatar);
            entity.AddMapPosition(new Position(go.transform.position));
            entity.AddTransform(go.transform);
            entity.AddAnimator(go.GetComponent<Animator>());
            entity.AddActiveAnimation(AnimationTags.idle);
            entity.AddNextAnimation(AnimationTags.idle);
            entity.AddPersona("Antonio");
            entity.AddActiveAvatar(true);
            BuilderMainAttributes(entity, attention: 5, dexterity: 5, endurance: 5, intellect: 5, luck: 5, personality: 5, strength: 5);
            BuilderLifeAttributes(entity);
            BuilderBattleAttributes(entity);
            UpdateAttributes(entity);
            entity.isPlayer = true;


            // создаем аватара для персонажа
            BattleEntity avatar = Contexts.sharedInstance.battle.CreateEntity();
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
            avatar.AddTypeFaction(Factions.Player);
            avatar.AddRadiusAttack(1);
            
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
                BuilderMainAttributes(entity, attention: 5, dexterity: 5, endurance: 5, intellect: 5, luck: 5, personality: 5, strength: 5);
                BuilderLifeAttributes(entity);
                BuilderBattleAttributes(entity);
                UpdateAttributes(entity);
                /*
                * Теги
                */
                entity.isAI = true; // нужно?
                // создаем аватара для врагов
                BattleEntity avatar = Contexts.sharedInstance.battle.CreateEntity();
                avatar.AddActionPoint(0);
                avatar.AddMapPosition(new Position(entity.transform.value.position));
                tgs.CellGetAtPosition(entity.transform.value.position, true).canCross = false; // делаем текущию клетку врага непроходимой для других
                avatar.AddTerrainGrid(TerrainGridSystem.instance);
                avatar.AddName(entity.name.name);
                avatar.AddPath(new List<int>(), 0);
                avatar.AddSpeed(2);
                avatar.AddRotateSpeed(5);
                avatar.AddActiveAvatar(true);
                avatar.AddTypeFaction(Factions.TribesOfHorde);
                avatar.isAI = true;
                avatar.AddRadiusAttack(1);
            }
        }

        private void BuilderMainAttributes(GameEntity entity, int attention, int dexterity, int endurance, int intellect, int luck, int personality, int strength)
        {
            entity.AddAttention(attention);
            entity.AddDexterity(dexterity);
            entity.AddEndurance(endurance);
            entity.AddIntellect(intellect);
            entity.AddLuck(luck);
            entity.AddPersonality(personality);
            entity.AddStrength(strength);
        }

        private void BuilderLifeAttributes(GameEntity entity)
        {
            entity.AddHealth(0, 0);
            entity.AddMana(0, 0);
        }

        private void BuilderBattleAttributes(GameEntity entity)
        {
            entity.AddIndexMagicDamage(0,0);
            entity.AddIndexMeleeDamage(0, 0);
            entity.AddIndexRangedDamage(0, 0);
        }

        private void UpdateAttributes(GameEntity entity)
        {
            entity.isManaUpdate = true;
            entity.isHealthUpdate = true;
            entity.isUpdateMagicDamage = true;
            entity.isUpdateMeleeDamage = true;
            entity.isUpdateRangedDamage = true;
        }

        private Entitas.Systems CreateSystems(Contexts contexts)
        {
            return new Feature("Game")
                .Add(new Systems.InitializeSystems.CameraInitEntitySystem())
                .Add(new Systems.InitializeSystems.LevelInitEntitySystem())
                .Add(new Systems.WorldMap.Camera.CameraFollowerMovementSystem(contexts))
                .Add(new Systems.WorldMap.Camera.MovementSystem(contexts))
                .Add(new Systems.WorldMap.Camera.ReturnMovementSystem(contexts))
                .Add(new Systems.WorldMap.Camera.TeleportSystem(contexts))
                .Add(new Systems.Animations.BoolAnimationSystem(contexts))
                .Add(new Systems.LocalMap.Player.MovementSystem(contexts))
                .Add(new Systems.LocalMap.Player.InputMovementSystem(contexts))
                .Add(new Systems.UpdateHealthSystem(contexts, gameBalanceSettings))
                .Add(new Systems.UpdateManaSystem(contexts, gameBalanceSettings))
                .Add(new Systems.UpdateMagicDamageSystem(contexts, gameBalanceSettings))
                .Add(new Systems.UpdateMeleeDamageSystem(contexts, gameBalanceSettings))
                .Add(new Systems.UpdateRangedDamageSystem(contexts, gameBalanceSettings));
        }
    }
}