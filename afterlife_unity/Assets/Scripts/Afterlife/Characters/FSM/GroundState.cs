using Afterlife.Characters.Core;
using Afterlife.Core.FSM;
using UnityEngine;

namespace Afterlife.Characters.FSM
{
    public class GroundState : AliveState
    {
        public GroundState(StateMachine machine, CharacterMotor motor) : base(machine, motor)
        {
            PhysicsMaterial = new PhysicsMaterial2D { friction = 10f };
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
            
            if (Motor.IsJumping)
            {
                Motor.Jump();
            }
            if (Mathf.Abs(Motor.MoveDirection.x) > 0.01f)
            {
                Machine.SetState<MoveState>();
            }
        } 
    }
}