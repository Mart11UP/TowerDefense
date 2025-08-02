using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tower.Health
{
    [RequireComponent(typeof(Shooter))]
    public class AutoShoot : MonoBehaviour
    {
        private Shooter shooter;

        private void Start()
        {
            shooter = GetComponent<Shooter>();
        }

        void Update()
        {
            shooter.Shoot();
        }
    }
}
