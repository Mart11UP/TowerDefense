using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Tower.AI.Enemy
{
    public class WalkToTowerState : BaseState<EnemyStateMachine.EnemyState>
    {
        private GameObject gameObject;
        private NavMeshAgent agent;

        public WalkToTowerState(EnemyStateMachine.EnemyState key, GameObject gameObject) : base(key)
        {
            this.gameObject = gameObject;
            agent = gameObject.GetComponent<NavMeshAgent>();
        }

        public override EnemyStateMachine.EnemyState GetNextState()
        {
            return EnemyStateMachine.EnemyState.WalkToTower;
        }

        public override void EnterState()
        {
            base.EnterState();
        }
        public override void UpdateState(BaseState<EnemyStateMachine.EnemyState> state)
        {
            base.UpdateState(state);
            agent.SetDestination(gameObject.transform.position + Vector3.back * 10);
        }
    }
}
