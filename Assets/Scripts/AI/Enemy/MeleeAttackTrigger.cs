using System;
using Tower.AI.Enemy;
using UnityEngine;

namespace Tower.AI
{
    public class MeleeAttackTrigger : MonoBehaviour
    {
        [SerializeField] private Vector3 center;
        [SerializeField] private float maxDistance = 2;
        [SerializeField] private LayerMask layerMask;
        public event Action OnTriggered;
        public event Action OnUnTriggered;
        private bool triggered = false;
        private EnemyStateMachine enemyStateMachine;
        public RaycastHit LastHitInfo { get; private set; }
        public bool Triggered
        {
            get { return triggered; }
            private set 
            {
                if (triggered != value)
                {
                    if (value) OnTriggered?.Invoke();
                    else OnUnTriggered?.Invoke();
                }
                triggered = value; 
            }
        }

        private void OnEnable()
        {
            enemyStateMachine = GetComponent<EnemyStateMachine>();
            OnTriggered += ChangeToAttackState;
        }

        private void OnDisable()
        {
            OnTriggered -= ChangeToAttackState;
        }

        private void ChangeToAttackState()
        {
            enemyStateMachine.TransitionToState(EnemyStateMachine.EnemyState.MeleeAttack);
        }

        private void Update()
        {
            bool hit = Physics.Raycast(transform.position + center, transform.forward, out var hitInfo, maxDistance, layerMask);
            if (hit) LastHitInfo = hitInfo;
            Triggered = hit;
        }

        private void OnDrawGizmos()
        {
            if (Triggered) Gizmos.color = Color.red;
            else Gizmos.color = Color.green;

            Gizmos.DrawLine(transform.position + center, transform.position + center + transform.forward * maxDistance);
        }
    }
}
