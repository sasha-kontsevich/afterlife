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

        //Информация
        [Header("Info")]
        [SerializeField] private string currentStateName;
        
        //Компоненты
        private Rigidbody2D _rb;
        private Collider2D _coll;
        private CharacterInput _input;
        private StateMachine _stateMachine;
        
        //Управление
        public Vector2 MoveDirection => _input.MovementInput;
        public bool IsJumping => _input.JumpInput;
        
        public Vector2 Velocity
        {
            get => _rb.linearVelocity;
            set => _rb.linearVelocity = value;
        }

        public Vector2 GroundNormal { get; private set; } = Vector2.up;
        
        private string CurrentStateName => _stateMachine.CurrentState?.GetType().Name ?? "No State";

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _coll = GetComponent<Collider2D>();
            _input = GetComponent<CharacterInput>();
            
            _stateMachine = new StateMachine();
            
            _stateMachine.AddState(new AirborneState(_stateMachine, this));
            _stateMachine.AddState(new GroundState(_stateMachine, this));
            _stateMachine.AddState(new MoveState(_stateMachine, this));
        }

        private void Start()
        {
            _stateMachine.SetState<GroundState>();
        }

        private void Update()
        {
            _stateMachine.Update();
        }

        private void FixedUpdate()
        {
            _stateMachine.FixedUpdate();
            
            DebugThis();
        }

        public void Jump()
        {
            Velocity += Vector2.up * jumpForce;
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
    }
}