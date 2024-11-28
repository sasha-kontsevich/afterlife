using Afterlife.Characters.Core;
using Afterlife.Core.FSM;
using UnityEngine;

namespace Afterlife.Characters.FSM
{
    public class AliveState : BaseState
    {
        protected readonly CharacterMotor Motor;
        protected PhysicsMaterial2D PhysicsMaterial;
        protected bool IsGrounded { get; private set; }

        protected AliveState(StateMachine machine, CharacterMotor motor) : base(machine)
        {
            Motor = motor;
            PhysicsMaterial =  new PhysicsMaterial2D { friction = 0.2f };
        }

        public override void Enter()
        {
            base.Enter();
            
            Motor.SetPhysicsMaterial(PhysicsMaterial);
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();

            CheckGround();
            
            if (!IsGrounded)
            {
                Machine.SetState<AirborneState>();
            }
        }
        
        private void CheckGround()
        {
            var hit = Physics2D.Raycast(Motor.transform.position, Vector2.down, 0.25f);
            Debug.DrawLine(Motor.transform.position, Motor.transform.position + Vector3.down * 0.25f, Color.cyan);
            if (hit.collider)
            {
                IsGrounded = true;
            }
            else
            {
                IsGrounded = false;
            }
        }
    }
}