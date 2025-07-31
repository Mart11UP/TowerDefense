using System.Collections;
using UnityEngine;

namespace Tower.Health
{
    public class DestroyOnDied : MonoBehaviour
    {
        [SerializeField] private ParticleSystem destroyEffectPrefab;
        private HealthController healthController;

        // Start is called before the first frame update
        void Start()
        {
            healthController = GetComponent<HealthController>();
            healthController.OnDied += Destroy;
        }

        private void OnDisable()
        {
            healthController.OnDied -= Destroy;
        }

        private void Destroy()
        {
            foreach (var component in GetComponents<Component>())
                if (component != this && component != transform) Destroy(component);
            if (destroyEffectPrefab != null) StartCoroutine(DestroyEffect());
        }

        private IEnumerator DestroyEffect()
        {
            ParticleSystem effect = Instantiate(destroyEffectPrefab, transform.position, transform.rotation);
            effect.Play();

            yield return new WaitForSeconds(3);

            Destroy(effect.gameObject);
        }
    }
}
