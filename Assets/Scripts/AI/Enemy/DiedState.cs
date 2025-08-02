using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Tower.AI.Enemy
{
    public class DiedState : BaseState<EnemyStateMachine.EnemyState>
    {
        private readonly EnemyDie enemyDie;

        public DiedState(EnemyStateMachine.EnemyState key, GameObject gameObject) : base(key)
        {
            enemyDie = gameObject.GetComponent<EnemyDie>();
        }

        public override void EnterState()
        {
            base.EnterState();
            enemyDie.StartDeath();
        }

        public override EnemyStateMachine.EnemyState GetNextState()
        {
            return EnemyStateMachine.EnemyState.Died;
        }
    }
}
