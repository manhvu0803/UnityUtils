using System;
using System.Collections.Generic;

namespace Vun.UnityUtils.GenericFSM
{
    /// <summary>
    /// An implementation of <see cref="IStateMachine{TState}"/> that automatically cache states upon new state transition
    /// </summary>
    public class CacheAutoStateMachine<TContext, TStateId, TState> : IUpdatableStateMachine<TContext, TStateId>
        where TState : IState<IUpdatableStateMachine<TContext, TStateId>>
    {
        public event Action<TStateId> StateChanged;

        public event Action ShutdownCompleted;

        private readonly Dictionary<TStateId, TState> _states = new();

        private readonly StateMachine<IUpdatableStateMachine<TContext, TStateId>, TState> _stateMachine;

        private readonly Func<TStateId, TState> _stateCreator;

        public TContext Context { get; }

        public CacheAutoStateMachine(TContext context, TStateId initialStateId, Func<TStateId, TState> stateCreator)
        {
            _stateCreator = stateCreator;
            var initialState = stateCreator.Invoke(initialStateId);
            _states[initialStateId] = initialState;
            Context = context;
            _stateMachine = new StateMachine<IUpdatableStateMachine<TContext, TStateId>, TState>(this, initialState);
        }

        public void TransitionTo(TStateId stateId)
        {
            if (!_states.TryGetValue(stateId, out var state))
            {
                state = _stateCreator.Invoke(stateId);
                _states[stateId] = state;
            }

            _stateMachine.TransitionTo(state);
            StateChanged?.Invoke(stateId);
        }

        public void Update(float deltaTime)
        {
            _stateMachine.Update(deltaTime);
        }

        public void Shutdown()
        {
            _stateMachine.Shutdown();
            ShutdownCompleted?.Invoke();
        }
    }
}