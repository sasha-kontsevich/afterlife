using UnityEngine;

namespace Afterlife.Characters.Core
{
    public class CharacterInput : MonoBehaviour
    {
        public Vector2 MovementInput { get; private set; }
        public bool JumpInput { get; private set; }

        private void Update()
        {
            MovementInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

            JumpInput = Input.GetButtonDown("Jump");
        }
    }
}