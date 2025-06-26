using System;
using Vun.UnityUtils;
using Vun.UnityUtils.GenericFSM;

namespace Sample.Scripts.StateMachine
{
    public class PersonStateMachine : StateMachineComponent<Person, Type>, ICreator<IAutoState<Person, Type>, Type>
    {
        private static readonly Type InitialStateType = typeof(WakingUpState);

        protected override IUpdatableAutoStateMachine<Person, Type> CreateStateMachine()
        {
            return new AutoCacheStateMachine<Person, Type>(Context, InitialStateType, this);
        }

        public IAutoState<Person, Type> Create(Type type)
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