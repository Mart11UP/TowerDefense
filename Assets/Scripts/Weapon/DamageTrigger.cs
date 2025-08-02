using UnityEngine;
using UnityEngine.Events;
using Tower.Health;

namespace Tower.Weapon
{
    public class DamageTrigger : MonoBehaviour
    {
        [SerializeField] protected string targetTag = "Enemy";
        public string TargetTag { get { return targetTag; } private set { targetTag = value; } }
        [SerializeField] private float damageAmount;
        public UnityEvent OnTrigger;

        public void SetDamageAmount(float damage)
        {
            damageAmount = damage;
        }

        protected void OnTriggerEnter(Collider other)
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
