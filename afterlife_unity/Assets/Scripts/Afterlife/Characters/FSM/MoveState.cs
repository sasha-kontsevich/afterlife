using Afterlife.Characters.Core;
using Afterlife.Core.FSM;
using UnityEngine;

namespace Afterlife.Characters.FSM
{
    public class MoveState : AliveState
    {
        public MoveState(StateMachine machine, CharacterMotor motor) : base(machine, motor)
        {
            PhysicsMaterial = new PhysicsMaterial2D { friction = 0.2f };
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
            
            if (Motor.IsJumping && IsGrounded)
            {
                Motor.Jump();
            }
            if (Mathf.Abs(Motor.MoveDirection.x) < 0.01f)
            {
                Machine.SetState<GroundState>();
            }

            Move();
        }

        private void Move()
        {
            // Направление движения вдоль поверхности
            // var movementDirection = Vector2.Perpendicular(-Motor.GroundNormal).normalized * Motor.MoveDirection.x;
            var movementDirection = Motor.MoveDirection;
            // Целевая горизонтальная скорость
            var targetVelocity = movementDirection * Motor.moveSpeed;

            // Смягчение только горизонтальной компоненты скорости
            // motor.Velocity = targetVelocity;
            Motor.Velocity = Vector2.Lerp(Motor.Velocity, targetVelocity, Time.fixedDeltaTime * Motor.moveSpeedLerpRate);
        }
    }
}