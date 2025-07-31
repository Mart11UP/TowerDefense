using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tower.Generic
{
    [RequireComponent(typeof(Rigidbody))]

    public class ForwardMovement : MonoBehaviour
    {
        [SerializeField] private float speed = 10;
        private Rigidbody rb;

        void Start()
        {
            rb = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            rb.MovePosition(transform.position + transform.forward * speed * Time.fixedDeltaTime);
        }
    }
}
