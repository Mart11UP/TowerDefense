using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tower.Generic
{
    public class ObjectDestroyer : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            Destroy(other.gameObject);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = new Color(1, 0, 0, 0.5f);

            Gizmos.DrawCube(transform.position, transform.localScale);
        }
    }
}
