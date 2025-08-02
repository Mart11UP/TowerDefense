using TMPro;
using UnityEngine;


namespace Tower.Weapon
{
    public class BulletMultiplier : MonoBehaviour
    {
        [SerializeField] private string bulletTargetTag = "Enemy";
        [SerializeField] private int bulletMultiplier = 2;
        [SerializeField] private TextMeshPro multiplierText;
        private new Collider collider;

        private void Start()
        {
            collider = GetComponent<Collider>();
            multiplierText.text = "X" + bulletMultiplier.ToString();
        }

        private void OnTriggerEnter(Collider other)
        {
            string targetTag = other.GetComponent<DamageTrigger>().TargetTag;
            if (targetTag != bulletTargetTag) return;
            
            MultiplicateBullets(other);
            Destroy(other.gameObject);
        }

        private void MultiplicateBullets(Collider other)
        {
            float sizeStep = transform.lossyScale.x / (bulletMultiplier);
            float start = (1 - (1.0f / bulletMultiplier)) * (transform.lossyScale.x / 2.0f);

            for (float i = 0; i < bulletMultiplier; i++)
            {
                Transform bullet = Instantiate(other.gameObject).transform;
                bullet.gameObject.layer = 0;
                float randomOffset = Random.Range(-0.25f, 0.25f);
                Vector3 position = other.transform.position + (start - i * sizeStep + randomOffset) * transform.right;

                bullet.SetPositionAndRotation(position, other.transform.rotation);
                bullet.SetParent(bullet.transform.parent);
            }
        }

        private void EnableCollider()
        {
            collider.enabled = true;
        }
    }
}
