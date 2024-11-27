using Afterlife.Characters.Core;
using UnityEngine;

namespace Afterlife.Characters.States
{
    public class MovingState : ICharacterState
    {
        private float _horizontalInput;

        public void EnterState(CharacterMotor motor)
        {
            motor.SetFriction(1f);
        }

        public void UpdateState(CharacterMotor motor)
        {
            if (Mathf.Abs(motor.MoveDirection.x) <= 0.01f)
            {
                motor.ChangeState(new GroundedState());
            }
            else if (!motor.IsGrounded)
            {
                motor.ChangeState(new AirborneState()); 
            }
            else if (motor.IsJumping)
            {
                motor.ChangeState(new JumpingState());
                return;
            }
            
            // Направление движения вдоль поверхности
            var movementDirection = Vector2.Perpendicular(-motor.GroundNormal).normalized * motor.MoveDirection.x;

            // Целевая горизонтальная скорость
            var targetVelocity = movementDirection * motor.moveSpeed;

            // Смягчение только горизонтальной компоненты скорости
            // motor.Velocity = targetVelocity;
            motor.Velocity = Vector2.Lerp(motor.Velocity, targetVelocity, Time.fixedDeltaTime * motor.moveSpeedLerpRate);

        }

        public void ExitState(CharacterMotor motor)
        {
            // Логика при выходе из состояния (например, сброс ввода)
        }
    }
}