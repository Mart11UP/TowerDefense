using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tower.Health
{
    [RequireComponent(typeof(Shooter))]
    public class AutoShoot : MonoBehaviour
    {
        [SerializeField] private float startDelay = 0;
        private Shooter shooter;

        private void Start()
        {
            shooter = GetComponent<Shooter>();
            InvokeRepeating(nameof(Shoot), startDelay, 0.1f);
        }

        private void Shoot()
        {
            shooter.Shoot();
        }
    }
}
