using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tower.Generic
{
    public class SetLayerOnAwake : MonoBehaviour
    {
        [SerializeField] private float delay = 0;
        [SerializeField] private int layer = 0;
        private void Awake()
        {
            Invoke(nameof(SetLayer), delay);
        }

        private void SetLayer()
        {
            gameObject.layer = layer;
        }
    }
}
