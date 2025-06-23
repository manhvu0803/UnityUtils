using Vun.UnityUtils.GenericFSM;

namespace Sample.Scripts.StateMachine
{
    public class PersonStateMachine : StateMachineComponent<Person>
    {
        protected override IAutoState<Person> InitialState => new WakingUpState(2);
    }
}