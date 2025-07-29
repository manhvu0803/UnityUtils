using Vun.UnityUtils.GenericFSM;

namespace Sample.Scripts.StateMachine
{
    public class PersonState : State<PersonStateMachine>
    {
        protected PersonStateMachine StateMachine => base.Context;

        protected new Person Context => StateMachine.Context;
    }
}