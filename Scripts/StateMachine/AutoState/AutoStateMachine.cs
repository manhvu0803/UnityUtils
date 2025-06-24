using System;

namespace Vun.UnityUtils.GenericFSM
{
    /// <summary>
    /// A simple implementation of <see cref="IAutoStateMachine{TContext,TState}"/> with <see cref="IAutoState{T}"/>,
    /// which need to be manually updated
    /// </summary>
    public class AutoStateMachine<T> : IAutoStateMachine<T, IAutoState<T>>
    {
        public T Context { get; }

        public event Action<IAutoState<T>> OnStateChanged;

        public event Action OnShutdown;

        public IAutoState<T> CurrentState { get; set; }

        public AutoStateMachine(T context, IAutoState<T> initialState)
        {
            Context = context;
            CurrentState = initialState;
            initialState.Enter(this);
        }

        public void TransitionTo(IAutoState<T> state)
        {
            CurrentState.Exit();
            CurrentState = state;
            CurrentState.Enter(this);
            OnStateChanged.TryInvoke(state);
        }

        public void Update(float deltaTime)
        {
            CurrentState.Update(deltaTime);
        }

        public void Shutdown()
        {
            CurrentState.Exit();
            CurrentState = default;
            OnShutdown.TryInvoke();
        }
    }
}