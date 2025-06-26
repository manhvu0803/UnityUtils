using UnityEngine;

namespace Vun.UnityUtils.GenericFSM
{
    /// <summary>
    /// An abstract implementation of <see cref="IAutoState"/>
    /// </summary>
    public abstract class AutoState<TContext, TState> : IAutoState<TContext, TState>
    {
        private IAutoStateMachine<TContext, TState> _stateMachine;

        /// <summary>The owner state machine of this state</summary>
        protected IAutoStateMachine<TContext, TState> StateMachine
        {
            get
            {
#if DEBUG
                if (_stateMachine == null)
                {
                    Debug.LogError($"Context of {this} is null. Check if Enter() or Exit() has been called for this state");
                }
#endif

                return _stateMachine;
            }
            private set => _stateMachine = value;
        }

        /// <summary>The target (host) of this state</summary>
        protected TContext Context => StateMachine.Context;

        public virtual void Enter(IAutoStateMachine<TContext, TState> stateMachine)
        {
            StateMachine = stateMachine;
        }

        public virtual void Update(float deltaTime) { }

        protected void TransitionTo(TState state)
        {
            StateMachine.TransitionTo(state);
        }

        public virtual void Exit()
        {
            StateMachine = default;
        }
    }

    public class AutoState<TContext> : AutoState<TContext, IAutoState<TContext>>, IAutoState<TContext> { }
}