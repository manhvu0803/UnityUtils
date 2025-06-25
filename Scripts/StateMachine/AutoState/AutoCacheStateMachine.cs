using System.Collections.Generic;

namespace Vun.UnityUtils.GenericFSM
{
    public class AutoCacheStateMachine<TContext, TState, TStateId> :
        AutoStateMachine<TContext, TState, TStateId>
        where TState : IAutoState<TContext, TStateId>
    {
        // This must be a separate class because C# doesn't allow passing "this" to constructors
        private class StateCache : ICreator<TState, TStateId>
        {
            private readonly Dictionary<TStateId, TState> _states = new();

            private readonly ICreator<TState, TStateId> _stateCreator;

            public StateCache(ICreator<TState, TStateId> stateCreator)
            {
                _stateCreator = stateCreator;
            }

            public TState Create(TStateId stateId)
            {
                if (_states.TryGetValue(stateId, out var state))
                {
                    return state;
                }

                state = _stateCreator.Create(stateId);
                _states[stateId] = state;
                return state;
            }
        }

        public AutoCacheStateMachine(TContext context, TStateId initialStateId, ICreator<TState, TStateId> stateCreator) :
            base(context, initialStateId, new StateCache(stateCreator))
        { }
    }

    public class AutoCacheStateMachine<TContext, TStateId> : AutoCacheStateMachine<TContext, IAutoState<TContext, TStateId>, TStateId>
    {
        public AutoCacheStateMachine(TContext context, TStateId initialStateId, ICreator<IAutoState<TContext, TStateId>, TStateId> stateCreator) :
            base(context, initialStateId, stateCreator)
        { }
    }
}