using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tower.Health
{
    public class HealthBarController : MonoBehaviour
    {
        [SerializeField] private HealthController healthController;
        private Transform healthContainer;

        private void Start()
        {
            healthContainer = transform.GetChild(0);
            EnableBar(false);
        }

        private void OnEnable()
        {
            healthController.OnDamageReceived += UpdateHealthBar;
            healthController.OnDied.AddListener(DisableHealthBar);
        }
        private void OnDisable()
        {
            healthController.OnDamageReceived -= UpdateHealthBar;
            healthController.OnDied.RemoveListener(DisableHealthBar);
        }

        private void EnableBar(bool enabled)
        {
            GetComponent<SpriteRenderer>().enabled = enabled;
            foreach (SpriteRenderer sprite in GetComponentsInChildren<SpriteRenderer>())
                sprite.enabled = enabled;
        }

        private void UpdateHealthBar(float damage)
        {
            EnableBar(true);
            Vector3 scale = healthContainer.localScale;
            float normalizedHealth = healthController.GetNormalizedHealth();
            healthContainer.localScale = new(normalizedHealth, scale.y, scale.z);
        }

        private void DisableHealthBar()
        {
            EnableBar(false);
        }
    }
}
