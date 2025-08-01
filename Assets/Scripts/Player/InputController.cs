using UnityEngine;
using Tower.Health;

namespace Tower.Player
{
    [RequireComponent(typeof(CharacterMovement))]
    [RequireComponent (typeof(Shooter))]
    public class InputController : MonoBehaviour
    {
        public Vector3 MovementInput { get; private set; }
        private Shooter shooter;
        private CharacterMovement characterMovement;

        void Start()
        {
            shooter = GetComponent<Shooter>();
            characterMovement = GetComponent<CharacterMovement>();
        }

        void Update()
        {
            MovementInput = new(Input.GetAxis("Horizontal"), 0, 0);
            characterMovement.MoveCharacter(MovementInput);

            if (Input.GetMouseButtonDown(0)) shooter.Shoot();
        }
    }
}
