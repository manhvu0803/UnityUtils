using System;

namespace Vun.UnityUtils.GenericFSM
{
    public abstract class AutoCacheCompositeState<TContext, TState, TInnerKey> : AutoState<TContext, TState>, IAutoStateMachine<TContext, TInnerKey>
    {
        public event Action<TInnerKey> OnStateChanged;

        public event Action OnShutdown;

        protected abstract TInnerKey InitialStateId { get; }

        protected abstract TState NextState { get; }

        public TInnerKey CurrentState => _subStateMachine.CurrentState;

        public IAutoState<TContext, TInnerKey> CurrentStateObject => _subStateMachine.CurrentStateObject;

        private AutoCacheStateMachine<TContext, TInnerKey> _subStateMachine;

        TContext IAutoStateMachine<TContext, TInnerKey>.Context => base.Context;

        public override void Enter(IAutoStateMachine<TContext, TState> stateMachine)
        {
            base.Enter(stateMachine);
            _subStateMachine = new AutoCacheStateMachineWithCallback<TContext, TInnerKey>(Context, InitialStateId, GetState);
            _subStateMachine.OnStateChanged += OnSubMachineStateChange;
            _subStateMachine.OnShutdown += OnSubMachineShutdown;
        }

        private void OnSubMachineStateChange(TInnerKey stateId)
        {
            OnStateChanged.TryInvoke(stateId);
        }

        private void OnSubMachineShutdown()
        {
            OnShutdown.TryInvoke();
            TransitionTo(NextState);
        }

        protected abstract IAutoState<TContext, TInnerKey> GetState(TInnerKey stateId);

        void IStateMachine<TInnerKey>.TransitionTo(TInnerKey stateId)
        {
            _subStateMachine.TransitionTo(stateId);
        }

        public void Shutdown()
        {
            _subStateMachine.Shutdown();
        }
    }
}