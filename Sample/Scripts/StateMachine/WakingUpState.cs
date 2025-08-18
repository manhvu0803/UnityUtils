namespace Sample.Scripts.StateMachine
{
    public class WakingUpState : PersonState
    {
        private readonly float _wakeUpTime;

        public WakingUpState(float wakeUpTime)
        {
            _wakeUpTime = wakeUpTime;
        }

        protected override void Enter()
        {
            Context.Wait(_wakeUpTime, WakeUp);
            Context.Say("Mm");
        }

        private void WakeUp()
        {
            Context.Say("Holy shit i'm late");
            TransitionTo<GettingReadyState>();
        }
    }
}