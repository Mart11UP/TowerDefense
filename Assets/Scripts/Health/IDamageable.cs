using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tower.Health
{
    public interface IDamageable
    {
        public abstract void RecieveDamage(float amount);
    }
}