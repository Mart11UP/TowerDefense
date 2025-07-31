using Tower.Health;
using UnityEngine;
using UnityEngine.AI;

namespace Tower.AI.Enemy
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class EnemyStateMachine : StateManager<EnemyStateMachine.EnemyState>
    {
        private HealthController enemyHealth;
        private MeleeAttackTrigger damagableDetector;

        public enum EnemyState
        {
            WalkToTower,
            MeleeAttack,
            Died
        }

        void Start()
        {
            States.Add(EnemyState.WalkToTower, new WalkToTowerState(EnemyState.WalkToTower, gameObject));
            States.Add(EnemyState.Died, new DiedState(EnemyState.Died, gameObject));
            States.Add(EnemyState.MeleeAttack, new MeleeAttackState(EnemyState.MeleeAttack, gameObject));
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

        public override void TransitionToState(EnemyState stateKey)
        {
            if (CurrentState.StateKey == EnemyState.Died)
            {
                Debug.LogWarning("Cannot transition to any state while in the '" + EnemyState.Died + "' state.");
                return;
            }
            if (CurrentState.StateKey == stateKey) return;
            base.TransitionToState(stateKey);
        }
    }
}
