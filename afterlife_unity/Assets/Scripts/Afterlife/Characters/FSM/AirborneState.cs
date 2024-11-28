using Afterlife.Characters.Core;
using Afterlife.Core.FSM;
using UnityEngine;

namespace Afterlife.Characters.FSM
{
    public class AirborneState : AliveState
    {
        public AirborneState(StateMachine machine, CharacterMotor motor) : base(machine, motor)
        {
            PhysicsMaterial = new PhysicsMaterial2D { friction = 0f };
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();

            if (IsGrounded && Mathf.Abs(Motor.MoveDirection.x) < 0.01f)
            {
                Machine.SetState<GroundState>();
            }
            if (IsGrounded && Mathf.Abs(Motor.MoveDirection.x) > 0.01f)
            {
                Machine.SetState<MoveState>();
            }
            Move();
        }

        private void Move()
        {
            var targetVelocity = Motor.MoveDirection * Motor.moveSpeed;
        
            var vector2 = Motor.Velocity;
            vector2.x = Mathf.Lerp(Motor.Velocity.x, targetVelocity.x, Time.fixedDeltaTime * Motor.moveSpeedLerpRate);
            Motor.Velocity = vector2;
        }
        
    }
}