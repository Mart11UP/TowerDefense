using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tower.Health
{
    public class ReduceHealthByTime : MonoBehaviour
    {
        [SerializeField] private float lifeTime = 10;
        private HealthController healthController;
        private float healthReductionBySecond;

        // Start is called before the first frame update
        void Start()
        {
            healthController = GetComponent<HealthController>();
            healthReductionBySecond = healthController.MaxHealth / lifeTime;
        }

        // Update is called once per frame
        void Update()
        {
            healthController.ReceiveDamage(healthReductionBySecond * Time.deltaTime);
        }
    }
}
