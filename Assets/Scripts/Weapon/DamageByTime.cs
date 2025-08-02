using System.Collections;
using System.Collections.Generic;
using Tower.Health;
using UnityEngine;
using UnityEngine.Events;

namespace Tower.Weapon
{
    public class DamageByTime : MonoBehaviour
    {
        [SerializeField] protected string targetTag = "Enemy";
        [SerializeField] private float damageBySecond = 0.1f;

        private void OnTriggerStay(Collider other)
        {
            if (!other.CompareTag(targetTag)) return;
            if (!other.TryGetComponent<IDamageable>(out var damageable))
            {
                Debug.LogWarning(other.name + " doesn't have the IDamageable interface.");
                return;
            }
            damageable.ReceiveDamage(damageBySecond * Time.deltaTime);
        }
    }
}
