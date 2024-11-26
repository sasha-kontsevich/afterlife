using Afterlife.Characters.Core;
using UnityEngine;

namespace Afterlife.Characters.States
{
    public class GroundedState : ICharacterState
    {
        public void EnterState(CharacterMotor motor)
        {
            motor.SetFriction(20f);
        }

        public void UpdateState(CharacterMotor motor)
        {
            if (!motor.IsGrounded)
            {
                motor.ChangeState(new AirborneState());
            }
            else if (Mathf.Abs(Input.GetAxis("Horizontal")) > 0.01f)
            {
                motor.ChangeState(new MovingState());
            }
        }

        public void ExitState(CharacterMotor motor)
        {
            // Опционально: действия при выходе из состояния
        }
    }
}