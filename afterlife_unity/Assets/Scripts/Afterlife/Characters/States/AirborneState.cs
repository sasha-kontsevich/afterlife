using Afterlife.Characters.Core;

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
            if (motor.IsGrounded)
            {
                motor.ChangeState(new GroundedState());
            }
        }

        public void ExitState(CharacterMotor motor)
        {
            // Опционально: действия при выходе из состояния
        }
    }
}