using System.Collections;
using UnityEngine;
using DG.Tweening;
using UnityEngine.AI;
using Tower.Health;

namespace Tower.AI.Enemy
{
    public class MeleeAttackState : BaseState<EnemyStateMachine.EnemyState>
    {
        private GameObject gameObject;
        private EnemyStateMachine enemyStateMachine;
        private MeleeAttackTrigger meleeAttackTrigger;
        private NavMeshAgent agent;
        private bool attacking;
        private Vector3 attackPoint;
        private Vector3 backPoint;
        private IEnumerator attackRoutine;

        public MeleeAttackState(EnemyStateMachine.EnemyState key, GameObject gameObject) : base(key)
        {
            this.gameObject = gameObject;
            meleeAttackTrigger = gameObject.GetComponent<MeleeAttackTrigger>();
            enemyStateMachine = gameObject.GetComponent<EnemyStateMachine>();
            agent = gameObject.GetComponent<NavMeshAgent>();
        }

        public override void EnterState()
        {
            base.EnterState();
            agent.ResetPath();
            agent.enabled = false;
            Vector3 normal = meleeAttackTrigger.LastHitInfo.normal;
            attackPoint = meleeAttackTrigger.LastHitInfo.point + normal * agent.radius;
            backPoint = gameObject.transform.position + normal * agent.radius + normal * 0.5f;
            attackRoutine = AttackRoutine();
            enemyStateMachine.StartCoroutine(attackRoutine);
        }

        public override void ExitState()
        {
            base.ExitState();
            enemyStateMachine.StopCoroutine(attackRoutine);
            agent.enabled = true;
        }

        private void TryToDamage()
        {
            if (!meleeAttackTrigger.Triggered) return;

            IDamageable damagable = meleeAttackTrigger.LastHitInfo.collider.GetComponent<IDamageable>();
            damagable?.ReceiveDamage(1);
        }

        private IEnumerator AttackRoutine()
        {
            while (true)
            {
                attacking = true;
                yield return gameObject.transform.DOMove(attackPoint, 0.5f).SetEase(Ease.InQuad).WaitForCompletion();

                attacking = false;

                TryToDamage();

                yield return new WaitForSeconds(0.5f);

                attacking = true;
                yield return gameObject.transform.DOMove(backPoint, 0.5f).SetEase(Ease.OutQuad).WaitForCompletion();

                yield return new WaitForSeconds(0.5f);
            }
        }

        public override EnemyStateMachine.EnemyState GetNextState()
        {
            if (attacking) return EnemyStateMachine.EnemyState.MeleeAttack;
            if (meleeAttackTrigger.Triggered) return EnemyStateMachine.EnemyState.MeleeAttack;
            return EnemyStateMachine.EnemyState.WalkToTower;
        }
    }
}
