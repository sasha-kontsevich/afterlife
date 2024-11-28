using Afterlife.Core.FSM;

namespace Afterlife.Characters.FSM
{
    public class BaseState : State
    {
        protected BaseState(StateMachine machine) : base(machine) { }
        
        public override void Enter() {}
    }
}