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
    public abstract class StateMachineComponent<T> : MonoBehaviour, IAutoStateMachine<T, IAutoState<T>> where T : Component
    {
        [field: SerializeField]
        public T Context { get; private set; }

        public event Action<IAutoState<T>> OnStateChanged;

        public event Action OnShutdown;

        public UpdateType UpdateMethod;

#if ODIN_INSPECTOR
        [ShowInInspector, ReadOnly]
#endif
        public IAutoState<T> CurrentState => _stateMachine?.CurrentState;

        public bool UseUnscaledTime;

        private AutoStateMachine<T> _stateMachine;

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
            _stateMachine = new AutoStateMachine<T>(Context, InitialState);
            _stateMachine.OnStateChanged += OnSubMachineStateChanged;
            _stateMachine.OnShutdown += OnSubMachineShutdown;
        }

        private void OnSubMachineStateChanged(IAutoState<T> state)
        {
            OnStateChanged?.Invoke(state);
        }

        private void OnSubMachineShutdown()
        {
            enabled = false;
            OnShutdown?.Invoke();
        }

        protected abstract IAutoState<T> InitialState { get; }

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
        public void TransitionTo(IAutoState<T> state)
        {
            _stateMachine.TransitionTo(state);
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