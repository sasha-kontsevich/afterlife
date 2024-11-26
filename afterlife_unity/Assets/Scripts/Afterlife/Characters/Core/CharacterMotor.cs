using Afterlife.Characters.States;
using UnityEngine;

namespace Afterlife.Characters.Core
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Collider2D))]
    public class CharacterMotor : MonoBehaviour
    {
        [Header("Movement Settings")]
        public float moveSpeed = 5f;
        public float jumpForce = 10f;
        public LayerMask groundLayer;
        
        public float moveSpeedLerpRate = 3f;

        private Rigidbody2D _rb;
        private Collider2D _coll;
        private ICharacterState _currentState;

        private PhysicsMaterial2D _physicsMaterial2D0;
        private PhysicsMaterial2D _physicsMaterial2D1;
        private PhysicsMaterial2D _physicsMaterial2D10;

        public bool IsGrounded { get; private set; }
        
        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _coll = GetComponent<Collider2D>();
            
            // Инициализация материалов
            _physicsMaterial2D0 = new PhysicsMaterial2D { friction = 0f };
            _physicsMaterial2D1 = new PhysicsMaterial2D { friction = 1f };
            _physicsMaterial2D10 = new PhysicsMaterial2D { friction = 10f };
            
            ChangeState(new GroundedState()); // Начальное состояние
        }

        private void FixedUpdate()
        {
            CheckGround();
            _currentState.UpdateState(this);
        }

        public void Move(float horizontal)
        {
            var targetVelocityX = horizontal * moveSpeed;
            var smoothedVelocityX = Mathf.Lerp(_rb.linearVelocity.x, targetVelocityX, Time.fixedDeltaTime * moveSpeedLerpRate);
            _rb.linearVelocity = new Vector2(smoothedVelocityX, _rb.linearVelocity.y);
        }

        public void Jump()
        {
            if (IsGrounded)
            {
                _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, jumpForce);
            }
        }

        public void ChangeState(ICharacterState newState)
        {
            _currentState?.ExitState(this);
            _currentState = newState;
            _currentState.EnterState(this);
        }

        public void SetFriction(float value)
        {
            _coll.sharedMaterial = value switch
            {
                < 0.5f => _physicsMaterial2D0,
                < 5f => _physicsMaterial2D1,
                _ => _physicsMaterial2D10
            };
        }

        private void CheckGround()
        {
            IsGrounded = Physics2D.OverlapCircle(transform.position, 0.2f, groundLayer);
        }
    }
}