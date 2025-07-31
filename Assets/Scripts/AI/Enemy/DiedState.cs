using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Tower.AI.Enemy
{
    public class DiedState : BaseState<EnemyStateMachine.EnemyState>
    {
        private NavMeshAgent agent;
        private GameObject gameObject;

        public DiedState(EnemyStateMachine.EnemyState key, GameObject gameObject) : base(key)
        {
            this.gameObject = gameObject;
            agent = gameObject.GetComponent<NavMeshAgent>();
        }

        public override void EnterState()
        {
            base.EnterState();
            gameObject.GetComponent<EnemyStateMachine>().StartCoroutine(DieRoutine());
        }

        private IEnumerator DieRoutine()
        {
            agent.ResetPath();
            yield return new WaitForSeconds(1);
            MonoBehaviour.Destroy(gameObject);
        }

        public override EnemyStateMachine.EnemyState GetNextState()
        {
            return EnemyStateMachine.EnemyState.Died;
        }
    }
}
