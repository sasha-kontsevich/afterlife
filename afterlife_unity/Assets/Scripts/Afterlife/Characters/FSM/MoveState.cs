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
            
            Move();
            
            if (Motor.IsJumping)
            {
                Motor.Jump();
            }
            if (Mathf.Abs(Motor.MoveDirection.x) < 0.01f)
            {
                Machine.SetState<GroundState>();
            }
        }

        private void Move()
        {
            // Рассчитываем целевую скорость по горизонтали
            var targetVelocityX = Motor.MoveDirection.x * Motor.moveSpeed;

            // Если персонаж на наклонной поверхности (определяется нормалью)
            if (Vector2.Angle(Vector2.up, Motor.GroundNormal) > 10f && Vector2.Angle(Vector2.up, Motor.GroundNormal) <= 45f)
            {
                // Направление движения с учётом наклона поверхности
                var slopeDirection = Vector2.Perpendicular(Motor.GroundNormal).normalized;
                if (slopeDirection.y > 0) slopeDirection = -slopeDirection; // Гарантия движения вдоль наклона вниз

                var adjustedVelocity = slopeDirection * targetVelocityX;
                var smoothedVelocity = Vector2.Lerp(Motor.Velocity, adjustedVelocity, Time.fixedDeltaTime * Motor.moveSpeedLerpRate);

                // Устанавливаем новую скорость
                Motor.Velocity = new Vector2(smoothedVelocity.x, Motor.Velocity.y);
            }
            else
            {
                // Для горизонтальных поверхностей или больших углов применяем обычное движение
                var smoothedVelocityX = Mathf.Lerp(Motor.Velocity.x, targetVelocityX, Time.fixedDeltaTime * Motor.moveSpeedLerpRate);
                Motor.Velocity = new Vector2(smoothedVelocityX, Motor.Velocity.y);
            }
        }
    }
}