using System.Collections;
using UnityEngine;

namespace Tower.Health
{
    public class DestroyOnTrigger : MonoBehaviour
    {
        [SerializeField] private ParticleSystem destroyEffectPrefab;
        private DamageTrigger damageTrigger;

        // Start is called before the first frame update
        void Start()
        {
            damageTrigger = GetComponent<DamageTrigger>();
            damageTrigger.OnTrigger += Destroy;
        }

        private void OnDisable()
        {
            damageTrigger.OnTrigger -= Destroy;
        }

        private void Destroy()
        {
            foreach(var component in GetComponents<Component>())
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
