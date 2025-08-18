using System;
using System.Collections.Generic;

namespace Vun.UnityUtils.GenericFSM
{
    /// <summary>
    /// An implementation of <see cref="IStateMachine{TState}"/> that automatically cache states upon new state transition
    /// </summary>
    public class CacheStateMachine<TContext, TStateId, TState> : IUpdatableStateMachine<TContext, TStateId>
        where TState : IState<TContext>
    {
        private readonly Dictionary<TStateId, TState> _states = new();

        private readonly StateMachine<TContext, TState> _stateMachine;

        private readonly Func<TStateId, TState> _stateCreator;

        public TContext Context { get; }

        public CacheStateMachine(TContext context, TStateId initialStateId, Func<TStateId, TState> stateCreator)
        {
            _stateCreator = stateCreator;
            var initialState = stateCreator.Invoke(initialStateId);
            _states[initialStateId] = initialState;
            Context = context;
            _stateMachine = new StateMachine<TContext, TState>(context, initialState);
        }

        public void TransitionTo(TStateId stateId)
        {
            if (!_states.TryGetValue(stateId, out var state))
            {
                state = _stateCreator.Invoke(stateId);
                _states[stateId] = state;
            }

            _stateMachine.TransitionTo(state);
        }

        public void Update(float deltaTime)
        {
            _stateMachine.Update(deltaTime);
        }

        public void Shutdown()
        {
            _stateMachine.Shutdown();
        }
    }
}