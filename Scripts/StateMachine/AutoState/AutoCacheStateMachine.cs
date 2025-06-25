using System;
using System.Collections.Generic;

namespace Vun.UnityUtils.GenericFSM
{
    public abstract class AutoCacheStateMachine<TContext, TKey> : IAutoStateMachine<TContext, TKey>
    {
        public event Action OnShutdown;

        public event Action<TKey> OnStateChanged;

        private readonly Dictionary<TKey, IAutoState<TContext, TKey>> _states = new();

        public TContext Context { get; }

        public TKey CurrentState { get; private set; }

        public IAutoState<TContext, TKey> CurrentStateObject { get; private set; }

        protected AutoCacheStateMachine(TContext context, TKey initialStateId, IAutoState<TContext, TKey> initialState)
        {
            _states[initialStateId] = initialState;
            CurrentState = initialStateId;
            CurrentStateObject = initialState;
            Context = context;
        }

        public void TransitionTo(TKey stateID)
        {
            if (!_states.TryGetValue(stateID, out var state))
            {
                state = GetState(stateID);
                _states[stateID] = state;
            }

            CurrentStateObject.Exit();
            CurrentStateObject = state;
            CurrentStateObject.Enter(this);
            SetStateId(stateID);
        }

        protected void SetStateId(TKey stateId)
        {
            var oldStateId = CurrentState;
            CurrentState = stateId;
            OnStateChanged.TryInvoke(oldStateId);
        }

        protected abstract IAutoState<TContext, TKey> GetState(TKey stateId);

        public virtual void Update(float deltaTime)
        {
            CurrentStateObject.Update(deltaTime);
        }

        public virtual void Shutdown()
        {
            CurrentStateObject.Exit();
            OnShutdown.TryInvoke();
        }
    }
}