using UnityEngine;

namespace Tower.Health
{
    public class Shooter : MonoBehaviour
    {
        [SerializeField] GameObject bulletPrefab;
        [SerializeField] float fireRate = 1f;
        [SerializeField] Transform shootPoint;
        private float lastShootTime;
        private float shootCooldown;
        private Transform instancesContainer;

        void Start()
        {
            shootCooldown = 1 / fireRate;
            lastShootTime = -shootCooldown;
            instancesContainer = new GameObject("Bullets "+name).transform;
        }

        public GameObject Shoot()
        {
            if (Time.time < lastShootTime + shootCooldown) return null;

            lastShootTime = Time.time;
            GameObject bullet = Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
            bullet.transform.SetParent(instancesContainer);
            return bullet;
        }

        public void Shoot(float damage)
        {
            GameObject bullet = Shoot();
            if (bullet == null) return;

            DamageTrigger damageTrigger = bullet.GetComponent<DamageTrigger>();
            damageTrigger.SetDamageAmount(damage);
        }

        private void OnDestroy()
        {
            if (instancesContainer != null) Destroy(instancesContainer.gameObject);
        }
    }
}
