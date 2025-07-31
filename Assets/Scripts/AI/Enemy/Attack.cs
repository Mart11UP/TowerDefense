using System.Collections;
using UnityEngine;

namespace Tower.AI.Enemy
{
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
