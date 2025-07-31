using Tower.Health;
using UnityEngine;
using UnityEngine.AI;

namespace Tower.AI.Enemy
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class EnemyStateMachine : StateManager<EnemyStateMachine.EnemyState>
    {
        private HealthController enemyHealth;
        private ForwardRaycast damagableDetector;

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
            States.Add(EnemyState.Attack, new AttackState(EnemyState.Attack, gameObject));

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

        private bool IsAValidState(EnemyState stateKey)
        {
            if (CurrentState.StateKey == EnemyState.Died)
            {
                Debug.LogWarning("Cannot transition to any state while in the '" + EnemyState.Died + "' state.");
                return false;
            }
            if (CurrentState.StateKey == stateKey) return false;

            return true;
        }

        public override void TransitionToState(EnemyState stateKey)
        {
            if (!IsAValidState(stateKey)) return;
            base.TransitionToState(stateKey);
        }

        public void ChangeToAttackState(Attack attack = null)
        {
            if (!IsAValidState(EnemyState.Attack)) return;

            AttackState attackState = (AttackState)States[EnemyState.Attack];
            if (attack != null) attackState.SetAttack(attack);
            TransitionToState(EnemyState.Attack);
        }
    }
}
