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

        [Header("Info")]
        [SerializeField] private string currentStateName;
        
        
        private Rigidbody2D _rb;
        private Collider2D _coll;
        private ICharacterState _currentState;

        private PhysicsMaterial2D _physicsMaterial2D0;
        private PhysicsMaterial2D _physicsMaterial2D1;
        private PhysicsMaterial2D _physicsMaterial2D10;

        public Vector2 GroundNormal { get; private set; } = Vector2.up;
        public Vector2 MoveDirection { get; private set; } = Vector2.zero;
        public bool IsGrounded { get; private set; }
        public bool IsJumping { get; private set; }
        private string CurrentStateName => _currentState?.GetType().Name ?? "No State";

        public Vector2 Velocity
        {
            get => _rb.linearVelocity;
            set => _rb.linearVelocity = value;
        }

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _coll = GetComponent<Collider2D>();
            
            // Инициализация материалов
            _physicsMaterial2D0 = new PhysicsMaterial2D { friction = 0f };
            _physicsMaterial2D1 = new PhysicsMaterial2D { friction = 0.2f };
            _physicsMaterial2D10 = new PhysicsMaterial2D { friction = 10f };
            
            ChangeState(new GroundedState()); // Начальное состояние
        }

        private void FixedUpdate()
        {
            CheckGround();
            _currentState.UpdateState(this);
            // Дебаг линии для отображения движения
            currentStateName = CurrentStateName;
            // Debug.Log(currentStateName + ", " + _rb.linearVelocity);
            Debug.DrawLine(transform.position, transform.position + new Vector3(_rb.linearVelocity.x, _rb.linearVelocity.y, 0) * 0.3f, Color.green);
            Debug.DrawLine(Vector3.zero, new Vector3(_rb.linearVelocity.x, 0, 0) * 1f, Color.red);
        }

        public void Move(float horizontal)
        {
            MoveDirection = new Vector2(horizontal, 0);
        }

        public void Jump()
        {
            if (IsGrounded)
            {
                IsJumping = true;
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
            // Обновляем нормаль поверхности, если персонаж на земле
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.25f);
            if (hit.collider)
            {
                IsGrounded = true;
                GroundNormal = hit.normal;
            }
            else
            {
                IsGrounded = false;
                GroundNormal = Vector2.up; // Сброс нормали на вертикаль
                IsJumping = false;
            }

        }
    }
}