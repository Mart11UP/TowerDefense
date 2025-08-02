using UnityEngine;

namespace Tower.Weapon
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
