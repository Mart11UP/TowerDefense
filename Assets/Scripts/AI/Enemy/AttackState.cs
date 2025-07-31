using UnityEngine;

namespace Tower.AI.Enemy
{
    public class AttackState : BaseState<EnemyStateMachine.EnemyState>
    {
        private Attack attack;

        public AttackState(EnemyStateMachine.EnemyState key, GameObject gameObject) : base(key)
        {
            attack = gameObject.GetComponent<Attack>();
        }

        public override void EnterState()
        {
            attack.StartAttack();
        }

        public override void ExitState()
        {
            attack.StopAttack();
        }

        public override EnemyStateMachine.EnemyState GetNextState()
        {
            if (attack.IsAttacking) return EnemyStateMachine.EnemyState.Attack;

            return EnemyStateMachine.EnemyState.WalkToTower;
        }

        public void SetAttack(Attack attack)
        {
            this.attack = attack;
        }
    }
}