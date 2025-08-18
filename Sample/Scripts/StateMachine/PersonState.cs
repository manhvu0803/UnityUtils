using System;
using Vun.UnityUtils.GenericFSM;

namespace Sample.Scripts.StateMachine
{
    public class PersonState : AutoState<Person, Type>
    {
        protected void TransitionTo<TState>() where TState : IState<IStateMachine<Person, Type>>
        {
            StateMachine.TransitionTo(typeof(TState));
        }
    }
}