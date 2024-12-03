using UnityEngine;

namespace Afterlife.Characters.Core
{
    public class CharacterInput : MonoBehaviour
    {
        public Vector2 MovementInput { get; private set; }
        public float jumpBufferTime = 0.2f;
        
        private float _lastJumpTime = -1f;

        public bool HasBufferedJump => Time.time - _lastJumpTime <= jumpBufferTime;

        private void Update()
        {
            MovementInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            
            if (Input.GetButtonDown("Jump"))
            {
                _lastJumpTime = Time.time;
            }
        }
        
        public bool ConsumeBufferedJump()
        {
            if (!HasBufferedJump) return false;
            _lastJumpTime = -1f;
            return true;
        }
    }
}