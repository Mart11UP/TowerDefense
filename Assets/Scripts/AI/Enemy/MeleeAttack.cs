using DG.Tweening;
using System.Collections;
using Tower.Health;
using UnityEngine;
using UnityEngine.AI;

namespace Tower.AI.Enemy
{
    [RequireComponent(typeof(ForwardRaycast))]
    public class MeleeAttack : Attack
    {
        [SerializeField] private float damage = 1;
        private ForwardRaycast meleeAttackTrigger;
        private NavMeshAgent agent;
        private Vector3 attackPoint;
        private Vector3 backPoint;
        private EnemyStateMachine enemyStateMachine;

        private void OnEnable()
        {
            meleeAttackTrigger = GetComponent<ForwardRaycast>();
            agent = GetComponent<NavMeshAgent>();
            enemyStateMachine = GetComponent<EnemyStateMachine>();
            meleeAttackTrigger.OnTriggered += ChangeToAttackState;
        }

        private void OnDisable()
        {
            meleeAttackTrigger.OnTriggered -= ChangeToAttackState;
        }

        protected override void ChangeToAttackState()
        {
            enemyStateMachine.ChangeToAttackState(this);
        }

        private void TryToDamage()
        {
            if (!meleeAttackTrigger.Triggered) return;

            IDamageable damagable = meleeAttackTrigger.LastHitInfo.collider.GetComponent<IDamageable>();
            damagable?.ReceiveDamage(damage);
        }

        protected override IEnumerator AttackRoutine()
        {
            yield return new WaitForSeconds(0.5f);
            agent.enabled = false;
            yield return gameObject.transform.DOMove(backPoint, 0.5f).SetEase(Ease.OutQuad).WaitForCompletion();
            while (true)
            {
                IsAttacking = true;
                yield return new WaitForSeconds(0.25f);

                yield return gameObject.transform.DOMove(attackPoint, 0.5f).SetEase(Ease.InQuint).WaitForCompletion();
                IsAttacking = meleeAttackTrigger.Triggered;

                TryToDamage();

                yield return new WaitForSeconds(0.5f);

                IsAttacking = meleeAttackTrigger.Triggered;
                if (!meleeAttackTrigger.Triggered) break;
                yield return gameObject.transform.DOMove(backPoint, 0.5f).SetEase(Ease.OutQuad).WaitForCompletion();
            }
        }

        public override void StartAttack()
        {
            agent.ResetPath();
            Vector3 normal = meleeAttackTrigger.LastHitInfo.normal;
            attackPoint = meleeAttackTrigger.LastHitInfo.point + normal * agent.radius;
            backPoint = gameObject.transform.position + normal * agent.radius;

            base.StartAttack();
        }

        public override void StopAttack()
        {
            base.StopAttack();
            agent.enabled = true;
        }
    }
}
