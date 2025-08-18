using System;
using Vun.UnityUtils.GenericFSM;

namespace Sample.Scripts.StateMachine
{
    public class PersonStateMachine : StateMachineComponent<Person, Type>
    {
        private static readonly Type InitialStateType = typeof(WakingUpState);

        protected override IUpdatableStateMachine<Person, Type> CreateStateMachine()
        {
            var stateMachine = new CacheAutoStateMachine<Person, Type, PersonState>(Context, InitialStateType, CreateState);
            stateMachine.ShutdownCompleted += Shutdown;
            stateMachine.StateChanged += InvokeStateChanged;
            return stateMachine;
        }

        public void TransitionTo<T>() where T : IState<PersonStateMachine>
        {
            TransitionTo(typeof(T));
        }

        private static PersonState CreateState(Type type)
        {
            if (type == InitialStateType)
            {
                return new WakingUpState(2);
            }

            if (type == typeof(GettingReadyState))
            {
                return new GettingReadyState();
            }

            throw new ArgumentException($"Unknown type: {type}");
        }
    }
}