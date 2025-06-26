using System;
using Vun.UnityUtils.GenericFSM;

namespace Sample.Scripts.StateMachine
{
    public abstract class TypeBasedState<T> : AutoState<T, Type>
    {
        protected void TransitionTo<TState>() where TState : IAutoState<T, Type>
        {
            StateMachine.TransitionTo(typeof(TState));
        }
    }
}