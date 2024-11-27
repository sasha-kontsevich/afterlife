using Afterlife.Characters.Core;
using UnityEngine;

namespace Afterlife.Characters.States
{
    public class JumpingState : ICharacterState
    {
        public void EnterState(CharacterMotor motor)
        {
            motor.SetFriction(0);
            motor.Velocity += Vector2.up * motor.jumpForce;
        }

        public void UpdateState(CharacterMotor motor)
        {
            if (!motor.IsGrounded)
            {
                motor.ChangeState(new AirborneState());
            }
            
            // Целевая горизонтальная скорость
            var targetVelocity = motor.MoveDirection * motor.moveSpeed;
            
            var vector2 = motor.Velocity;
            vector2.x = Mathf.Lerp(motor.Velocity.x, targetVelocity.x, Time.fixedDeltaTime * motor.moveSpeedLerpRate);
            motor.Velocity = vector2;
        }

        public void ExitState(CharacterMotor motor)
        {
            // motor.IsJumping = false;
        }
    }
}