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
            // Получаем ввод игрока для движения
            _horizontalInput = Input.GetAxis("Horizontal");
            
            if (Mathf.Abs(_horizontalInput) <= 0.01f)
            {
                motor.ChangeState(new GroundedState());
            }

            if (!motor.IsGrounded)
            {
                motor.ChangeState(new AirborneState());
            }
        }

        public void ExitState(CharacterMotor motor)
        {
            // Логика при выходе из состояния (например, сброс ввода)
        }
    }
}