using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;
using RedMoonRPG.Systems;
using RedMoonRPG.Utils;

namespace RedMoonRPG
{
    public class LoaderController : Singleton<LoaderController>
    {
        private Entitas.Systems _systems;

        protected override void Awake()
        {
            base.Awake();
            Contexts contexts = Contexts.sharedInstance;

            _systems = CreateLoadingSystem(contexts);
            _systems.Initialize();
            CreateLevelEntity();

            DontDestroyOnLoad(gameObject);
        }

        private void Update()
        {
            _systems.Execute();
        }

        private void CreateLevelEntity()
        {
            GameEntity game = Contexts.sharedInstance.game.CreateEntity();
            game.AddName("GameInit");
            GameEntity level = Contexts.sharedInstance.game.CreateEntity();
            level.AddName(Tags.level);
        }

        private Entitas.Systems CreateLoadingSystem(Contexts contexts)
        {
            return new Feature("GameLoading")
                .Add(new Systems.InitializeSystems.CreateFactionSystem())
                .Add(new LoadLevelSystem(contexts));
        }
    }
}