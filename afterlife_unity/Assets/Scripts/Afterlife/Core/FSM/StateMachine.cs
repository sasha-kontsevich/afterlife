using System;
using System.Collections.Generic;

namespace Afterlife.Core.FSM
{
    public class StateMachine
    {
        public State CurrentState { get; private set; }
        
        private readonly Dictionary<Type, State> _states = new();

        public void AddState(State state)
        {
            _states.Add(state.GetType(), state);
        }

        public void SetState<T>() where T : State
        {
            var type = typeof(T);

            if (CurrentState?.GetType() == type) return;
            
            if (!_states.TryGetValue(type, out var state)) return;
            
            CurrentState?.Exit();
            CurrentState = state;
            CurrentState?.Enter();
        }

        public virtual void Update()
        {
            CurrentState?.Update();
        }

        public virtual void FixedUpdate()
        {
            CurrentState?.FixedUpdate();
        }
    }
}