using Vun.UnityUtils.GenericFSM;

namespace Sample.Scripts.StateMachine
{
    public class WakingUpState : AutoState<Person>
    {
        private readonly float _wakeUpTime;

        public WakingUpState(float wakeUpTime)
        {
            _wakeUpTime = wakeUpTime;
        }

        public override void OnEnter(IStateMachine<Person, IAutoState<Person>> stateMachine)
        {
            base.OnEnter(stateMachine);
            Context.Wait(_wakeUpTime, WakeUp);
            Context.Say("Mm");
        }

        private void WakeUp()
        {
            Context.Say("Holy shit i'm late");
            TransitionTo(new GettingReadyState());
        }
    }
}