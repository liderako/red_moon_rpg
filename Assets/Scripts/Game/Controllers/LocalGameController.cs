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
            go.GetComponent<AnimatorListener>().SetUnit(entity);
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
            
            CharacterEntity characterEntity = Contexts.sharedInstance.character.CreateEntity();
            characterEntity.AddName(entity.name.name);
            characterEntity.AddPersona(entity.persona.value);
            BuilderMainAttributes(characterEntity, attention: 5, dexterity: 5, endurance: 5, intellect: 5, luck: 5, personality: 5, strength: 5);
            BuilderLifeAttributes(characterEntity);
            BuilderBattleAttributes(characterEntity);
            UpdateAttributes(characterEntity);
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
            List<ItemScriptableObjects> array = new List<ItemScriptableObjects>();
            array.Add(Resources.Load<ItemScriptableObjects>("SO/Items/BigAxe"));
            array.Add(Resources.Load<ItemScriptableObjects>("SO/Items/WoodenClub"));
            for (int i = 0; i < array.Count; i++)
            {
                Debug.Log(array[i].name);
            }
            // создаем гейм сущности для врагов
            for (int i = 0; i < _testEnemy.Count; i++)
            {
                GameEntity entity = Contexts.sharedInstance.game.CreateEntity();
                _testEnemy[i].transform.position = tgs.CellGetPosition(tgs.CellGetAtPosition(_testEnemy[i].transform.position, true), true);
                entity.AddAnimator(_testEnemy[i].GetComponent<Animator>());
                _testEnemy[i].GetComponent<AnimatorListener>().SetUnit(entity);
                entity.AddTransform(_testEnemy[i].transform);
                entity.AddActiveAnimation(AnimationTags.idle);
                entity.AddNextAnimation(AnimationTags.idle);
                entity.AddPersona(_testEnemy[i].name);
                entity.AddName(_testEnemy[i].name + i.ToString());
                entity.AddActiveAvatar(true);
                CharacterEntity characterEntity = Contexts.sharedInstance.character.CreateEntity();
                characterEntity.AddName(_testEnemy[i].name + i.ToString());
                characterEntity.AddPersona(_testEnemy[i].name);
                
                BuilderMainAttributes(characterEntity, attention: 5, dexterity: 5, endurance: 5, intellect: 5, luck: 5, personality: 5, strength: 5);
                BuilderLifeAttributes(characterEntity);
                BuilderBattleAttributes(characterEntity);
                BuilderWeapon(characterEntity, array[i]);
                UpdateAttributes(characterEntity);
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

        private void BuilderMainAttributes(CharacterEntity entity, int attention, int dexterity, int endurance, int intellect, int luck, int personality, int strength)
        {
            entity.AddAttention(attention);
            entity.AddDexterity(dexterity);
            entity.AddEndurance(endurance);
            entity.AddIntellect(intellect);
            entity.AddLuck(luck);
            entity.AddPersonality(personality);
            entity.AddStrength(strength);
        }

        private void BuilderWeapon(CharacterEntity unit, ItemScriptableObjects data)
        {
            CharacterEntity entityWeapon = Contexts.sharedInstance.character.CreateEntity();
            entityWeapon.AddPersona(unit.persona.value);
            if (data.magicDamage[1] != 0)
            {
                entityWeapon.AddIndexMagicDamage(data.magicDamage[0], data.magicDamage[1]); 
            }
            
            if (data.meeleDamage[1] != 0)
            {
                entityWeapon.AddIndexMeleeDamage(data.meeleDamage[0], data.meeleDamage[1]);
            }
            
            if (data.rangedDamage[1] != 0)
            {
                entityWeapon.AddIndexRangedDamage(data.rangedDamage[0], data.rangedDamage[1]);
            }
            if (data.attention != 0)
            {
                entityWeapon.AddAttention(data.attention);
            }
            if (data.dexterity != 0)
            {
                entityWeapon.AddAttention(data.dexterity);
            }
            if (data.endurance != 0)
            {
                entityWeapon.AddAttention(data.endurance);
            }
            if (data.intellect != 0)
            {
                entityWeapon.AddAttention(data.intellect);
            }
            if (data.luck != 0)
            {
                entityWeapon.AddAttention(data.luck);
            }
            if (data.personality != 0)
            {
                entityWeapon.AddAttention(data.personality);
            }
            if (data.strength != 0)
            {
                entityWeapon.AddAttention(data.strength);
            }
            entityWeapon.AddName(data.name);
            entityWeapon.AddWeigth(data.weigth);
            entityWeapon.AddDefaultPrice(data.defaultPrice);
        }

        private void BuilderLifeAttributes(CharacterEntity entity)
        {
            entity.AddHealth(0, 0);
            entity.AddMana(0, 0);
        }

        private void BuilderBattleAttributes(CharacterEntity entity)
        {
            entity.AddIndexMagicDamage(0,0);
            entity.AddIndexMeleeDamage(0, 0);
            entity.AddIndexRangedDamage(0, 0);
        }

        private void UpdateAttributes(CharacterEntity entity)
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