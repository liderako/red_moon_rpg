using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;
using RedMoonRPG.Tags;

public class LoaderController : MonoBehaviour
{
    private Systems _systems;

    private void Start()
    {
		Contexts contexts = Contexts.sharedInstance;

		_systems = CreateLoadingSystem(contexts);
        _systems.Initialize();

        InitTest();
        DontDestroyOnLoad(gameObject);
    }

    private void InitTest()
    {
        GameEntity level = Contexts.sharedInstance.game.CreateEntity();
        level.AddName(Tags.level);
        level.AddLimitMap(new Vector2(-30, 30), new Vector2(20, 45), new Vector2(-40, 40));
    }

    private void Update()
    {
        _systems.Execute();
    }

    private Systems CreateLoadingSystem(Contexts contexts)
	{
		return new Feature("GameLoading")
        .Add(new LoadLevelSystem(contexts));
    }
}
