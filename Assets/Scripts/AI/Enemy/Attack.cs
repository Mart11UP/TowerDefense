using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace Tower.AI.Enemy
{
    [RequireComponent(typeof(EnemyStateMachine))]
    public abstract class Attack : MonoBehaviour
    {
        protected IEnumerator attackRoutine;
        public bool IsAttacking { get; protected set; }
        public virtual void StartAttack()
        {
            IsAttacking = true;
            attackRoutine = AttackRoutine();
            StartCoroutine(attackRoutine);
        }

        public virtual void StopAttack()
        {
            StopCoroutine(attackRoutine);
            IsAttacking = false;
        }

        protected abstract void ChangeToAttackState();

        protected abstract IEnumerator AttackRoutine();
    }
}
