using System.Collections;
using System.Collections.Generic;
using Tower.Health;
using UnityEngine;
using UnityEngine.AI;
using static Codice.Client.Common.WebApi.WebApiEndpoints;

namespace Tower.AI.Enemy
{
    [RequireComponent(typeof(SmoothDestroy))]
    public class EnemyDie : MonoBehaviour
    {
        private NavMeshAgent agent;
        private SmoothDestroy smoothDestroy;

        private void Start()
        {
            agent = GetComponent<NavMeshAgent>();
            smoothDestroy = GetComponent<SmoothDestroy>();
        }

        public void StartDeath()
        {
            StartCoroutine(DeathRoutine());
        }

        private IEnumerator DeathRoutine()
        {
            agent.ResetPath();
            yield return new WaitForSeconds(0.25f);
            smoothDestroy.Destroy();
        }
    }
}
