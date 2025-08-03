using System.Collections;
using TMPro;
using UnityEngine;


namespace Tower.Weapon
{
    public class BulletMultiplier : MonoBehaviour
    {
        [SerializeField] private string bulletTargetTag = "Enemy";
        [SerializeField] private int bulletMultiplier = 2;
        [SerializeField] private TextMeshPro multiplierText;

        private void Start()
        {
            multiplierText.text = "X" + bulletMultiplier.ToString();
        }

        private void OnTriggerEnter(Collider other)
        {
            string targetTag = other.GetComponent<DamageTrigger>().TargetTag;
            if (targetTag != bulletTargetTag) return;

            MultiplicateBullets(other);
        }

        private void MultiplicateBullets(Collider other)
        {
            other.gameObject.layer = 0;
            other.enabled = false;

            float sizeStep = transform.lossyScale.x / (bulletMultiplier);
            float start = (1 - (1.0f / bulletMultiplier)) * (transform.lossyScale.x / 2.0f);

            for (float i = 0; i < bulletMultiplier; i++)
            {
                Transform bullet = Instantiate(other.gameObject).transform;
                bullet.GetComponent<Collider>().enabled = true; 
                float randomOffset = Random.Range(-0.25f, 0.25f);
                Vector3 position = other.transform.position + (start - i * sizeStep + randomOffset) * transform.right;

                bullet.SetPositionAndRotation(position, other.transform.rotation);
                bullet.SetParent(bullet.transform.parent);
            }
            Destroy(other.gameObject);
        }
    }
}
