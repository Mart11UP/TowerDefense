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
            Died,
            Idle
        }

        void Awake()
        {
            States.Add(EnemyState.WalkToTower, new WalkToTowerState(EnemyState.WalkToTower, gameObject));
            States.Add(EnemyState.Died, new DiedState(EnemyState.Died, gameObject));
            States.Add(EnemyState.Attack, new AttackState(EnemyState.Attack, gameObject));

            CurrentState = States[EnemyState.WalkToTower];
            enemyHealth = GetComponent<HealthController>();
        }

        private void OnEnable()
        {
            enemyHealth.OnDied.AddListener(ChangeToDiedState);
        }

        private void OnDisable()
        {
            enemyHealth.OnDied.RemoveListener(ChangeToDiedState);
        }

        private void ChangeToDiedState()
        {
            enemyHealth.OnDied.RemoveListener(ChangeToDiedState);
            TransitionToState(EnemyState.Died);
        }

        private bool IsAValidState(EnemyState stateKey)
        {
            if (CurrentState.StateKey == EnemyState.Died) return false;
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
