using Afterlife.Characters.Core;
using Afterlife.Core.FSM;
using UnityEngine;

namespace Afterlife.Characters.FSM
{
    public class AliveState : BaseState
    {
        protected readonly CharacterMotor Motor;
        protected PhysicsMaterial2D PhysicsMaterial;

        protected AliveState(StateMachine machine, CharacterMotor motor) : base(machine)
        {
            Motor = motor;
            PhysicsMaterial =  new PhysicsMaterial2D { friction = 0.2f };
        }

        public override void Enter()
        {
            base.Enter();
            
            Motor.SetPhysicsMaterial(PhysicsMaterial);
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
            
            if (!Motor.IsGrounded && !Motor.CanJump)
            {
                Machine.SetState<AirborneState>();
            }
        }
    }
}