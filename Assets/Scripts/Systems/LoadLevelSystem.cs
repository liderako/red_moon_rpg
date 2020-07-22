using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Entitas;

/*
** Система необходимая для того чтобы камера следовала за фигуркой игрока на глобальной карте
*/
public class LoadLevelSystem : ReactiveSystem<GameEntity>
{
    private Contexts _contexts;
    private List<AsyncOperation> _loadOperations = new List<AsyncOperation>();

    private string _prevLevelName;

    private string name;

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
        GameEntity entity = _contexts.game.GetEntityWithName("Level");
        _prevLevelName = SceneManager.GetActiveScene().name;
        name = "Loading";
        LoadLevel(name);
    }


    private void OnLoadOperationComplete(AsyncOperation ao)
    {
        if (_loadOperations.Contains(ao))
        {
            _loadOperations.Remove(ao);
            if (_loadOperations.Count == 0)
            {
                SceneManager.SetActiveScene(SceneManager.GetSceneByName(name));
                UnloadLevel(_prevLevelName);
            }
        }
    }

    private void OnUnloadOperationComplete(AsyncOperation ao)
    {
        if (SceneManager.GetActiveScene().name.Equals("Loading"))
        {
            GameEntity entity = _contexts.game.GetEntityWithName("Level");
            _prevLevelName = "Loading";
            name = entity.nextLevelName.value;
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
            return ;
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
            return ;
        }
        ao.completed += OnUnloadOperationComplete;
    }
}
