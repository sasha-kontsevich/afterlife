namespace Afterlife.Core.FSM
{
    public abstract class State
    {
        protected readonly StateMachine Machine;

        protected State(StateMachine machine)
        {
            Machine = machine;
        }
        
        public virtual void Enter() {}
        
        public virtual void Update() {}
        
        public virtual void FixedUpdate() {}
        
        public virtual void Exit() {}
    }
}