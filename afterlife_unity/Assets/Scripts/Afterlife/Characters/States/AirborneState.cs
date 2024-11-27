using Afterlife.Characters.Core;
using UnityEngine;

namespace Afterlife.Characters.States
{
    public class AirborneState : ICharacterState
    {
        public void EnterState(CharacterMotor motor)
        {
            motor.SetFriction(0f);
        }

        public void UpdateState(CharacterMotor motor)
        {
            if (motor.IsGrounded && Mathf.Abs(motor.MoveDirection.x) >= 0.01f)
            {
                motor.ChangeState(new MovingState());
                return;
            }
            else if (motor.IsGrounded)
            {
                motor.ChangeState(new GroundedState());
                return;
            }

            // Целевая горизонтальная скорость
            var targetVelocity = motor.MoveDirection * motor.moveSpeed;
            
            var vector2 = motor.Velocity;
            vector2.x = Mathf.Lerp(motor.Velocity.x, targetVelocity.x, Time.fixedDeltaTime * motor.moveSpeedLerpRate);
            motor.Velocity = vector2;
        }

        public void ExitState(CharacterMotor motor)
        {
            // Опционально: действия при выходе из состояния
        }
    }
}