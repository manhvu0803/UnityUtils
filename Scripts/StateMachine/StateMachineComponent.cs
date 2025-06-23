#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif
using UnityEngine;

namespace Vun.UnityUtils.GenericFSM
{
    /// <summary>
    /// A <see cref="MonoBehaviour"/> implementation of <see cref="IStateMachine{TContext,TState}"/>,
    /// with auto-update, auto-enable on transition and auto-disable on exit
    /// </summary>
    /// <remarks>
    /// This would scale badly due to its subscription to multiple update methods.
    /// For a large number of agents, use a pure C# implementation (such as <see cref="StateMachineWithContext{T}"/>)
    /// </remarks>
    public abstract class StateMachineComponent<T> : MonoBehaviour, IStateMachine<T, IAutoState<T>> where T : Component
    {
        [field: SerializeField]
        public T Context { get; private set; }

        private IAutoState<T> _currentState;

        protected abstract IAutoState<T> InitialState { get; }

        public UpdateType UpdateMethod;

        public bool UseUnscaledTime;

#if ODIN_INSPECTOR
        [ShowInInspector, ReadOnly]
#endif
        private string CurrentStateName => _currentState?.ToString() ?? "";

#if UNITY_EDITOR
        private void OnValidate()
        {
            Context = this.GetIfNull(Context);
        }
#endif

        private void Start()
        {
            _currentState = InitialState;
            _currentState.OnEnter(this);
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
            _currentState.Update(deltaTime);
        }

        /// <summary>
        /// Exit the <see cref="_currentState"/>, enter <c>state</c> and enable this <see cref="MonoBehaviour"/>
        /// </summary>
        public void TransitionTo(IAutoState<T> state)
        {
            _currentState.OnExit();
            _currentState = state;
            _currentState.OnEnter(this);
            enabled = true;
        }

        /// <summary>
        /// Exit the <see cref="_currentState"/> and disable this <see cref="MonoBehaviour"/>
        /// </summary>
        public void Exit()
        {
            _currentState.OnExit();
            _currentState = default;
            enabled = false;
        }
    }
}