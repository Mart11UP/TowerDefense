using System;
using System.Collections;
using System.Collections.Generic;
using Tower.Health;
using UnityEngine;

namespace Tower.AI.Enemy
{
    public class EnemyHealth : MonoBehaviour, IDamageable
    {
        [SerializeField] private float health = 1;
        public event Action<float> OnEnemyDamaged;

        public float Health 
        { 
            get { return health; }
            private set 
            { 
                health = Mathf.Min(0, value); 
            }
        }

        public void RecieveDamage(float amount)
        {
            Health -= amount;
            OnEnemyDamaged?.Invoke(amount);
            print("EnemyDamaged :(");
        }


    }
}
