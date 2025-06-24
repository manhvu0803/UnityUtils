#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif
using System;
using UnityEngine;

namespace Vun.UnityUtils.GenericFSM
{
    /// <summary>
    /// A <see cref="MonoBehaviour"/> implementation of <see cref="IAutoStateMachine{TContext,TState}"/>,
    /// with auto-update, auto-enable on transition and auto-disable on exit.
    /// However, unlike <see cref="StateMachineComponent{T}"/>, this automatically cache and reuse state using a key,
    /// which can be anything from an enum to the type of the state itself
    /// </summary>
    /// <remarks>This has the same performance implication as <see cref="StateMachineComponent{T}"/></remarks>
    public abstract class CacheStateMachineComponent<TContext, TKey> : MonoBehaviour, IAutoStateMachine<TContext, TKey> where TContext : Component
    {
        private class StateMachine : AutoCacheStateMachine<TContext, TKey>
        {
            public delegate IAutoState<TContext, TKey> CreateStateCallback(TKey stateId);

            private CreateStateCallback _stateCreator;

            public StateMachine(TContext context, TKey initialStateId, CreateStateCallback stateCreator) :
                base(context, initialStateId, stateCreator(initialStateId)) { }

            protected override IAutoState<TContext, TKey> CreateState(TKey stateId)
            {
                return _stateCreator(stateId);
            }
        }

        [field: SerializeField]
        public TContext Context { get; private set; }

        public event Action<TKey> OnStateChanged;

        public event Action OnShutdown;

        public UpdateType UpdateMethod;

#if ODIN_INSPECTOR
        [ShowInInspector, ReadOnly]
#endif
        public TKey CurrentState => _stateMachine.CurrentState;

        public bool UseUnscaledTime;

        private AutoCacheStateMachine<TContext, TKey> _stateMachine;

#if ODIN_INSPECTOR
        [ShowInInspector, ReadOnly]
#endif
        private string CurrentStateInfo => _stateMachine.CurrentStateObject?.ToString() ?? "";

#if UNITY_EDITOR
        private void OnValidate()
        {
            Context = this.GetIfNull(Context);
        }
#endif

        private void Start()
        {
            _stateMachine = new StateMachine(Context, InitialStateId, CreateState);
        }

        protected abstract TKey InitialStateId { get; }

        protected abstract IAutoState<TContext, TKey> CreateState(TKey stateId);

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
        public void TransitionTo(TKey stateId)
        {
            _stateMachine.TransitionTo(stateId);
            enabled = true;
        }

        protected virtual void OnDestroy()
        {
            Shutdown();
        }

        /// <summary>
        /// Exit the <see cref="CurrentState"/> and disable this <see cref="MonoBehaviour"/>
        /// </summary>
        public void Shutdown()
        {
            enabled = false;
            _stateMachine.Shutdown();
        }
    }
}