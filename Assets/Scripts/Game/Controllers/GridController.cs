using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RedMoonRPG
{
    public class GridController : MonoBehaviour
    {
        private Entitas.Systems _systems;

        private void Start()
        {
            Contexts contexts = Contexts.sharedInstance;
            _systems = CreateSystems(contexts);
            _systems.Initialize();
        }

        private void Update()
        {
            if (_systems != null)
            {
                _systems.Execute();
            }
        }

        private Entitas.Systems CreateSystems(Contexts contexts)
        {
            return new Feature("Grid")
            .Add(new Systems.Battle.Grid.AwakeDisplayGridSystem(contexts))
            .Add(new Systems.Battle.Grid.DisplayAvailableGridSystem(contexts));
        }
    }

}
