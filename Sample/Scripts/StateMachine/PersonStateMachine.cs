using System;
using Vun.UnityUtils.GenericFSM;

namespace Sample.Scripts.StateMachine
{
    public class PersonStateMachine : StateMachineComponent<Person, Type>
    {
        private static readonly Type InitialStateType = typeof(WakingUpState);

        protected override IUpdatableStateMachine<Type> CreateStateMachine()
        {
            return new CacheStateMachine<PersonStateMachine, Type, IState<PersonStateMachine>>(this, InitialStateType, CreateState);
        }

        public void TransitionTo<T>() where T : IState<PersonStateMachine>
        {
            TransitionTo(typeof(T));
        }

        private static IState<PersonStateMachine> CreateState(Type type)
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