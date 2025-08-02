using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tower.Data
{
    [CreateAssetMenu(fileName = "DefenderData", menuName = "Tower/DefenderData")]
    public class DefenderData : ScriptableObject
    {
        public string Name;
        public GameObject prefab;
        public int Cost = 1;
        public Sprite Icon;

        public override bool Equals(object other)
        {
            return other is DefenderData data && Name == data.Name;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
