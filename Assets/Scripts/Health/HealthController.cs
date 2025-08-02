using System;
using UnityEngine;
using UnityEngine.Events;

namespace Tower.Health
{
    public class HealthController : MonoBehaviour, IDamageable
    {
        [SerializeField] private float maxHealth = 1;
        public float MaxHealth { get { return maxHealth; } }
        private float currentHealth;
        public event Action<float> OnDamageReceived;
        public UnityEvent OnDied;
        private bool IsDied = false;

        public float CurrentHealth 
        { 
            get { return currentHealth; }
            private set 
            { 
                currentHealth = Mathf.Clamp(value, 0, maxHealth);
            }
        }

        private void Awake()
        {
            CurrentHealth = maxHealth;
        }

        public void ReceiveDamage(float amount)
        {
            if (IsDied) return;

            CurrentHealth -= amount;
            OnDamageReceived?.Invoke(amount);
            if (CurrentHealth != 0) return; 
            IsDied = true;
            OnDied?.Invoke();
        }

        public float GetNormalizedHealth()
        {
            return CurrentHealth / MaxHealth;
        }
    }
}
