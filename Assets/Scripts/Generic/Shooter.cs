using UnityEngine;

namespace Tower.Generic
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

        public void Shoot()
        {
            if (Time.time < lastShootTime + shootCooldown) return;

            lastShootTime = Time.time;
            Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
        }
    }
}
