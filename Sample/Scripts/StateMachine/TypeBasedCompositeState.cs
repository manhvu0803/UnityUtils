using System;
using Vun.UnityUtils.GenericFSM;

namespace Sample.Scripts.StateMachine
{
    public abstract class TypeBasedCompositeState<TContext, TInnerState> : AutoCompositeState<TContext, Type, TInnerState>
    {
        protected void TransitionTo<TState>() where TState : IAutoState<TContext, Type>
        {
            StateMachine.TransitionTo(typeof(TState));
        }
    }
}