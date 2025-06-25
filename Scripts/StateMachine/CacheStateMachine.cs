using System.Collections.Generic;

namespace Vun.UnityUtils.GenericFSM
{
    public abstract class CacheStateMachine<TContext, TKey> : IStateMachine<TKey>
    {
        private readonly Dictionary<TKey, IState<TContext>> _states = new();

        private readonly StateMachine<TContext> _stateMachine;

        protected CacheStateMachine(TContext context, TKey initialStateId, IState<TContext> initialState)
        {
            _states[initialStateId] = initialState;
            _stateMachine = new StateMachine<TContext>(context, initialState);
        }

        public void TransitionTo(TKey stateId)
        {
            if (!_states.TryGetValue(stateId, out var state))
            {
                state = GetState(stateId);
                _states[stateId] = state;
            }

            _stateMachine.TransitionTo(state);
        }

        protected abstract IState<TContext> GetState(TKey stateId);

        public void Shutdown()
        {
            _stateMachine.Shutdown();
        }
    }
}