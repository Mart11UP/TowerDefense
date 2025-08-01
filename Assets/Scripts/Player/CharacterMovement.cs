using UnityEngine;

namespace Tower.Player
{
    public class CharacterMovement : MonoBehaviour
    {
        [SerializeField] private float speed = 10;
        private CharacterController characterController;

        void Start()
        {
            characterController = GetComponent<CharacterController>();
        }

        public void MoveCharacter(Vector3 movement)
        {
            characterController.SimpleMove(movement * speed);
        }
    }
}
