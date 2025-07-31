using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tower.Player
{
    public class Shooter : MonoBehaviour
    {
        [SerializeField] GameObject bulletPrefab;
        [SerializeField] float fireRate = 1f;
        private float lastShootTime;
        private float shootCooldown;

        // Start is called before the first frame update
        void Start()
        {
            shootCooldown = 1 / fireRate;
            lastShootTime = -shootCooldown;
        }

        // Update is called once per frame
        void Update()
        {
            Shoot();
        }

        private void Shoot()
        {
            if (!Input.GetMouseButtonDown(0)) return;
            if (Time.time < lastShootTime + shootCooldown) return;

            lastShootTime = Time.time;
            Instantiate(bulletPrefab, transform.position, transform.rotation);
        }
    }
}
