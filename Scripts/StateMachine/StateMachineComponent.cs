#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif
using System;
using UnityEngine;

namespace Vun.UnityUtils.GenericFSM
{
    /// <summary>
    /// A <see cref="MonoBehaviour"/> implementation of <see cref="IAutoStateMachine{TContext,TState}"/>,
    /// with auto-update, auto-enable on transition and auto-disable on exit
    /// </summary>
    /// <remarks>
    /// This would scale badly due to its subscription to multiple update methods.
    /// For a large number of agents, use a pure C# implementation (such as <see cref="AutoStateMachine{T}"/>)
    /// </remarks>
    public abstract class StateMachineComponent<TContext, TState> : MonoBehaviour, IStateMachine<TContext, TState>
        where TContext : Component
    {
        [field: SerializeField]
        public TContext Context { get; private set; }

        public event Action<TState> StateChanged;

        public event Action ShutdownCompleted;

        public UpdateType UpdateMethod;

        public bool UseUnscaledTime;

        private IUpdatableStateMachine<TContext, TState> _stateMachine;

#if UNITY_EDITOR
        private void OnValidate()
        {
            Context = this.GetIfNull(Context);
        }
#endif

        protected virtual void Start()
        {
            _stateMachine = CreateStateMachine();
        }

        protected abstract IUpdatableStateMachine<TContext, TState> CreateStateMachine();

        protected virtual void Update()
        {
            if (UpdateMethod == UpdateType.Update)
            {
                UpdateCurrentState();
            }
        }

        protected virtual void FixedUpdate()
        {
            if (UpdateMethod == UpdateType.FixedUpdate)
            {
                UpdateCurrentState();
            }
        }

        protected virtual void LateUpdate()
        {
            if (UpdateMethod == UpdateType.LateUpdate)
            {
                UpdateCurrentState();
            }
        }

        private void UpdateCurrentState()
        {
            // Unity should automatically use fixed delta time in FixedUpdate, so we don't have to account for it
            var deltaTime = UseUnscaledTime ? Time.unscaledDeltaTime : Time.deltaTime;
            _stateMachine.Update(deltaTime);
        }

        /// <summary>
        /// Exit the <see cref="CurrentState"/>, enter <c>state</c> and enable this <see cref="MonoBehaviour"/>
        /// </summary>
        public void TransitionTo(TState state)
        {
            _stateMachine.TransitionTo(state);
            InvokeStateChanged(state);
        }

        protected void InvokeStateChanged(TState state)
        {
            enabled = true;
            StateChanged?.Invoke(state);
        }

        protected virtual void OnDestroy()
        {
            if (enabled)
            {
                Shutdown();
            }
        }

        /// <summary>
        /// Exit the <see cref="CurrentState"/> and disable this <see cref="MonoBehaviour"/>
        /// </summary>
        public void Shutdown()
        {
            _stateMachine.Shutdown();
            enabled = false;
            ShutdownCompleted?.Invoke();
        }
    }
}