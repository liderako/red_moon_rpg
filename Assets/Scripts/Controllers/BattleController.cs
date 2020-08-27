using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RedMoonRPG
{
    public class BattleController : MonoBehaviour
    {
        private Entitas.Systems _systems;

        private void Start()
        {
            Contexts contexts = Contexts.sharedInstance;
            _systems = CreateSystems(contexts);
            _systems.Initialize();
        }

        private void FixedUpdate()
        {
            if (_systems != null)
            {
                _systems.Execute();
            }
        }

        private Entitas.Systems CreateSystems(Contexts contexts)
        {
            return new Feature("Battle")
            .Add(new Systems.Battle.QueueBattleSystems(contexts))
            .Add(new Systems.Battle.AwakeBattleSystem(contexts));
        }
    }

}
