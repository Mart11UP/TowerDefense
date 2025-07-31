using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Tower.AI.Enemy
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class EnemyStateMachine : StateManager<EnemyStateMachine.EnemyState>
    {
        private HealthController enemyHealth; 
        public enum EnemyState
        {
            WalkToTower,
            Attack,
            Died
        }

        void Start()
        {
            States.Add(EnemyState.WalkToTower, new WalkToTowerState(EnemyState.WalkToTower, gameObject));
            States.Add(EnemyState.Died, new DiedState(EnemyState.Died, gameObject));
            CurrentState = States[EnemyState.WalkToTower];
            enemyHealth = GetComponent<HealthController>();
            enemyHealth.OnDied += ChangeToDiedState;
        }

        private void OnDisable()
        {
            enemyHealth.OnDied -= ChangeToDiedState;
        }

        private void ChangeToDiedState()
        {
            enemyHealth.OnDied -= ChangeToDiedState;
            TransitionToState(EnemyState.Died);
        }
    }
}
