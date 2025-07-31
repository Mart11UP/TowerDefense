using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Tower.AI.Enemy
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class EnemyStateMachine : StateManager<EnemyStateMachine.EnemyState>
    {
        public enum EnemyState
        {
            WalkToTower,
            Idle
        }

        // Start is called before the first frame update
        void Start()
        {
            States.Add(EnemyState.WalkToTower, new WalkToTowerState(EnemyState.WalkToTower, gameObject));
            CurrentState = States[EnemyState.WalkToTower];
        }
    }
}
