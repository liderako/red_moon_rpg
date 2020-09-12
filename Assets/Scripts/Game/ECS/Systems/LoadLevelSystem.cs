using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Entitas;

namespace RedMoonRPG.Systems
{
    /*
    ** Система необходимая для того чтобы камера следовала за фигуркой игрока на глобальной карте
    */
    public class LoadLevelSystem : ReactiveSystem<GameEntity>
    {
        private Contexts _contexts;
        private List<AsyncOperation> _loadOperations = new List<AsyncOperation>();

        private string _prevLevelName;

        private string _name;

        public LoadLevelSystem(Contexts contexts) : base(contexts.game)
        {
            _contexts = contexts;
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.NextLevelName);
        }

        protected override bool Filter(GameEntity entity)
        {
            return entity.hasNextLevelName;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            _prevLevelName = SceneManager.GetActiveScene().name;
            _name = Scenes.loading;
            LoadLevel(_name);
        }

        private void OnLoadOperationComplete(AsyncOperation ao)
        {
            if (_loadOperations.Contains(ao))
            {
                _loadOperations.Remove(ao);
                if (_loadOperations.Count == 0)
                {
                    SceneManager.SetActiveScene(SceneManager.GetSceneByName(_name));
                    UnloadLevel(_prevLevelName);
                }
            }
        }

        private void OnUnloadOperationComplete(AsyncOperation ao)
        {
            if (SceneManager.GetActiveScene().name.Equals(Scenes.loading))
            {
                GameEntity entity = _contexts.game.GetEntityWithName(Tags.level);
                _prevLevelName = Scenes.loading;
                _name = entity.nextLevelName.value;
                LoadLevel(entity.nextLevelName.value);
                entity.RemoveNextLevelName();
            }
        }

        private void LoadLevel(string levelName)
        {
            AsyncOperation ao = SceneManager.LoadSceneAsync(levelName, LoadSceneMode.Additive);
            if (ao == null)
            {
                Debug.LogError("[GameManager] unable load scene " + levelName);
                return;
            }
            ao.completed += OnLoadOperationComplete;
            _loadOperations.Add(ao);
        }
        private void UnloadLevel(string levelName)
        {
            AsyncOperation ao = SceneManager.UnloadSceneAsync(levelName);
            if (ao == null)
            {
                Debug.LogError("[GameManager] unable unload scene " + levelName);
                return;
            }
            ao.completed += OnUnloadOperationComplete;
        }
    }
}