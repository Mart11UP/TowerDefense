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

        void Start()
        {
            shootCooldown = 1 / fireRate;
            lastShootTime = -shootCooldown;
        }

        public GameObject Shoot()
        {
            if (Time.time < lastShootTime + shootCooldown) return null;

            lastShootTime = Time.time;
            return Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
        }

        public void Shoot(float damage)
        {
            GameObject bullet = Shoot();
            if (bullet == null) return;

            DamageTrigger damageTrigger = bullet.GetComponent<DamageTrigger>();
            damageTrigger.SetDamageAmount(damage);
        }
    }
}
