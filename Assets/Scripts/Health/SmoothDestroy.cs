using System.Collections;
using UnityEngine;

namespace Tower.Health
{
    public class SmoothDestroy : MonoBehaviour
    {
        [SerializeField] private ParticleSystem destroyEffectPrefab;

        public void Destroy()
        {
            foreach (var component in GetComponents<MeshRenderer>()) Destroy(component);
            foreach (var component in GetComponentsInChildren<MeshRenderer>()) Destroy(component);
            foreach (var component in GetComponents<Collider>()) Destroy(component);

            if (destroyEffectPrefab != null) StartCoroutine(DestroyEffect());

            StartCoroutine(DestroyDelay(2));
        }

        private IEnumerator DestroyEffect()
        {
            ParticleSystem effect = Instantiate(destroyEffectPrefab, transform.position, transform.rotation);
            effect.Play();

            yield return new WaitForSeconds(3);

            Destroy(effect.gameObject);
        }

        private IEnumerator DestroyDelay(float delay)
        {
            yield return new WaitForSeconds(delay);

            Destroy(gameObject);
        }
    }
}
