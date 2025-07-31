using System;
using Tower.Health;
using UnityEngine;

namespace Tower.Health
{
    public class HealthController : MonoBehaviour, IDamageable
    {
        [SerializeField] private float maxHealth = 1;
        private float currentHealth;
        public event Action<float> OnDamageReceived;
        public event Action OnDied;

        public float CurrentHealth 
        { 
            get { return currentHealth; }
            private set 
            { 
                currentHealth = Mathf.Clamp(value, 0, maxHealth);
            }
        }

        private void Start()
        {
            CurrentHealth = maxHealth;
        }

        public void ReceiveDamage(float amount)
        {
            CurrentHealth -= amount;
            OnDamageReceived?.Invoke(amount);
            if (CurrentHealth == 0) OnDied?.Invoke();
            enabled = false;
        }
    }
}
