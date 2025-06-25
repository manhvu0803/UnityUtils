using System;

namespace Vun.UnityUtils.GenericFSM
{
    public abstract class AutoStateMachine<TContext, TState, TStateId> :
        IUpdatableAutoStateMachine<TContext, TStateId>
        where TState : IAutoState<TContext, TStateId>
    {
        public TContext Context { get; }

        public TStateId CurrentState { get; private set; }

        public event Action<TStateId> OnStateChanged;

        public event Action OnShutdown;

        public TState CurrentStateObject { get; private set; }

        private readonly ICreator<TState, TStateId> _stateCreator;

        protected AutoStateMachine(TContext context, TStateId initialStateId, ICreator<TState, TStateId> stateCreator)
        {
            _stateCreator = stateCreator;
            CurrentState = initialStateId;
            Context = context;
            CurrentStateObject = stateCreator.Create(initialStateId);
            CurrentStateObject.Enter(this);
        }

        public virtual void TransitionTo(TStateId stateId)
        {
            CurrentStateObject.Exit();
            CurrentStateObject = _stateCreator.Create(stateId);
            CurrentStateObject.Enter(this);
            SetStateId(stateId);
        }

        protected void SetStateId(TStateId stateId)
        {
            var oldStateId = CurrentState;
            CurrentState = stateId;
            OnStateChanged.TryInvoke(oldStateId);
        }

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

    public abstract class AutoStateMachine<TContext, TStateId> : AutoStateMachine<TContext, IAutoState<TContext, TStateId>, TStateId>
    {
        protected AutoStateMachine(TContext context, TStateId initialStateId, ICreator<IAutoState<TContext, TStateId>, TStateId> stateCreator) :
            base(context, initialStateId, stateCreator)
        { }
    }

    public class AutoStateMachine<TContext> : AutoStateMachine<TContext, IAutoState<TContext>, IAutoState<TContext>>
    {
        private class AutoStateCreator : ICreator<IAutoState<TContext>, IAutoState<TContext>>
        {
            public IAutoState<TContext> Create(IAutoState<TContext> state)
            {
                return state;
            }
        }

        public AutoStateMachine(TContext context, IAutoState<TContext> initialState)
            : base(context, initialState, new AutoStateCreator()) { }
    }
}