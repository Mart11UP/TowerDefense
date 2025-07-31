using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using Tower.Generic;

namespace Tower.AI.Enemy
{
    public class ProjectileAttack : Attack
    {
        [Header("Wait Time Range")]
        [SerializeField] private float minWaitTime = 2;
        [SerializeField] private float maxWaitTime = 3;
        private NavMeshAgent agent;
        private EnemyStateMachine enemyStateMachine;
        private Shooter shooter;
        private IEnumerator randomShootRoutine;

        private void Awake()
        {
            agent = gameObject.GetComponent<NavMeshAgent>();
            enemyStateMachine = GetComponent<EnemyStateMachine>();
            shooter = GetComponent<Shooter>();
            randomShootRoutine = RandomShoot();
            StartCoroutine(randomShootRoutine);
        }

        private IEnumerator RandomShoot()
        {
            while (true)
            {
                float waitTime = Random.Range(minWaitTime, maxWaitTime);
                yield return new WaitForSeconds(waitTime);

                ChangeToAttackState();
            }
        }

        override protected IEnumerator AttackRoutine()
        {
            IsAttacking = true;
            yield return new WaitForSeconds(0.5f);

            shooter.Shoot();

            yield return new WaitForSeconds(1);
            IsAttacking = false;
        }

        public override void StartAttack()
        {
            StopCoroutine(randomShootRoutine);
            agent.ResetPath();
            base.StartAttack();
        }

        public override void StopAttack()
        {
            base.StopAttack();
            StartCoroutine(randomShootRoutine);
        }

        protected override void ChangeToAttackState()
        {
            enemyStateMachine.ChangeToAttackState(this);
        }
    }
}
