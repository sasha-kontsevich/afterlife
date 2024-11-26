using Afterlife.Characters.Core;

namespace Afterlife.Characters.States
{
    public interface ICharacterState
    {
        void EnterState(CharacterMotor motor);
        void UpdateState(CharacterMotor motor);
        void ExitState(CharacterMotor motor);
    }
}