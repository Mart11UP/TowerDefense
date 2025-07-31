using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tower.Health
{
    public class DamageTrigger : MonoBehaviour
    {
        [SerializeField] private string targetTag = "Enemy";
        [SerializeField] private float damageAmount;
        public event Action OnTrigger;

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag(targetTag)) return;
            if (!other.TryGetComponent<IDamageable>(out var damageable))
            {
                Debug.LogWarning(other.name + " doesn't have the IDamageable interface.");
                return;
            }
            damageable.ReceiveDamage(damageAmount);
            OnTrigger?.Invoke();
        }
    }
}
