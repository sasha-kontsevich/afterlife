using Afterlife.Characters.FSM;
using Afterlife.Core.FSM;
using UnityEngine;

namespace Afterlife.Characters.Core
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Collider2D))]
    public class CharacterMotor : MonoBehaviour
    {
        //Настройки
        [Header("Movement Settings")]
        public float moveSpeed = 5f;
        public float jumpForce = 10f;
        public LayerMask groundLayer;
        public float moveSpeedLerpRate = 3f;
        public float coyoteTime = 0.2f; // Время эффекта койота

        //Информация
        [Header("Info")]
        [SerializeField] private string currentStateName;
        
        //Компоненты
        private Rigidbody2D _rb;
        private Collider2D _coll;
        private CharacterInput _input;

        //Управление
        public Vector2 MoveDirection => _input.MovementInput;
        public float DirectionX { get; private set; }
        public bool IsJumping => CanJump && _input.HasBufferedJump;
        public StateMachine StateMachine { get; private set; }
        public bool IsGrounded { get; private set; }
        public bool CanJump => IsGrounded || _coyoteTimeCounter > 0f;
        public Vector2 Velocity
        {
            get => _rb.linearVelocity;
            set => _rb.linearVelocity = value;
        }

        public Vector2 GroundNormal { get; private set; } = Vector2.up;
        
        private string CurrentStateName => StateMachine.CurrentState?.GetType().Name ?? "No State";
        private float _coyoteTimeCounter;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _coll = GetComponent<Collider2D>();
            _input = GetComponent<CharacterInput>();
            
            StateMachine = new StateMachine();
            
            StateMachine.AddState(new AirborneState(StateMachine, this));
            StateMachine.AddState(new GroundState(StateMachine, this));
            StateMachine.AddState(new MoveState(StateMachine, this));
        }

        private void Start()
        {
            StateMachine.SetState<GroundState>();
        }

        private void Update()
        {
            StateMachine.Update();
        }

        private void FixedUpdate()
        {
            StateMachine.FixedUpdate();
            
            CheckGround();

            UpdateDirection();
            
            DebugThis();
        }

        private void UpdateDirection()
        {
            if (MoveDirection.x > 0)
            {
                DirectionX = 1;
            }

            if (MoveDirection.x < 0)
            {
                DirectionX = -1;
            }
        }

        public void Jump()
        {
            Velocity = new Vector2(Velocity.x, jumpForce);
            // Debug.Log(Velocity);
            _input.ConsumeBufferedJump();
        }
        
        public void SetPhysicsMaterial(PhysicsMaterial2D material)
        {
            _coll.sharedMaterial = material;
        }

        private void DebugThis()
        {
            // Дебаг линии для отображения движения
            currentStateName = CurrentStateName;
            // Debug.Log(currentStateName + ", " + _rb.linearVelocity);
            Debug.DrawLine(transform.position, transform.position + new Vector3(_rb.linearVelocity.x, _rb.linearVelocity.y, 0) * 0.3f, Color.green);
            Debug.DrawLine(Vector3.zero, new Vector3(_rb.linearVelocity.x, 0, 0) * 1f, Color.red);
        }
        
        private void CheckGround()
        {
            IsGrounded = Physics2D.OverlapCircle(transform.position, 0.2f, groundLayer);
            
            if (IsGrounded)
            {
                _coyoteTimeCounter = coyoteTime;
            }
            else
            {
                _coyoteTimeCounter -= Time.fixedDeltaTime;
            }
        }
        
    }
}