using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tower.Player
{
    public class InputMovement : MonoBehaviour
    {
        [SerializeField] private float speed = 10;
        private CharacterController characterController;
        private Vector3 input;

        // Start is called before the first frame update
        void Start()
        {
            characterController = GetComponent<CharacterController>();
        }

        // Update is called once per frame
        void Update()
        {
            input = new(Input.GetAxis("Horizontal"), 0, 0);
            characterController.SimpleMove(input * speed);
        }
    }
}
