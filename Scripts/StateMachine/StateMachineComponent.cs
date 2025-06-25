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
    public abstract class StateMachineComponent<TContext, TState> :
        MonoBehaviour,
        IAutoStateMachine<TContext, TState>
        where TContext : Component
    {
        [field: SerializeField]
        public TContext Context { get; private set; }

        public event Action<TState> OnStateChanged;

        public event Action OnShutdown;

        public UpdateType UpdateMethod;

#if ODIN_INSPECTOR
        [ShowInInspector, ReadOnly]
#endif
        public TState CurrentState => _stateMachine == null ? default : _stateMachine.CurrentState;

        public bool UseUnscaledTime;

        private IUpdatableAutoStateMachine<TContext, TState> _stateMachine;

#if ODIN_INSPECTOR
        [ShowInInspector, ReadOnly]
#endif
        private string CurrentStateInfo => CurrentState?.ToString() ?? "";

#if UNITY_EDITOR
        private void OnValidate()
        {
            Context = this.GetIfNull(Context);
        }
#endif

        private void Start()
        {
            _stateMachine = CreateStateMachine();
            _stateMachine.OnStateChanged += OnSubMachineStateChanged;
            _stateMachine.OnShutdown += OnSubMachineShutdown;
        }

        protected abstract IUpdatableAutoStateMachine<TContext, TState> CreateStateMachine();

        private void OnSubMachineStateChanged(TState stateId)
        {
            OnStateChanged?.Invoke(stateId);
        }

        private void OnSubMachineShutdown()
        {
            enabled = false;
            OnShutdown?.Invoke();
        }

        private void Update()
        {
            if (UpdateMethod == UpdateType.Update)
            {
                UpdateCurrentState();
            }
        }

        private void FixedUpdate()
        {
            if (UpdateMethod == UpdateType.FixedUpdate)
            {
                UpdateCurrentState();
            }
        }

        private void LateUpdate()
        {
            if (UpdateMethod == UpdateType.LateUpdate)
            {
                UpdateCurrentState();
            }
        }

        private void UpdateCurrentState()
        {
            // Unity should automatically use fixed delta time in FixedUpdate, so we don't have to change it
            var deltaTime = UseUnscaledTime ? Time.unscaledTime : Time.deltaTime;
            _stateMachine.Update(deltaTime);
        }

        /// <summary>
        /// Exit the <see cref="CurrentState"/>, enter <c>state</c> and enable this <see cref="MonoBehaviour"/>
        /// </summary>
        public void TransitionTo(TState stateId)
        {
            _stateMachine.TransitionTo(stateId);
            enabled = true;
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
        }
    }
}