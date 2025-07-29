using System;
using Vun.UnityUtils.GenericFSM;

namespace Sample.Scripts.StateMachine
{
    public class WakingUpState : PersonState
    {
        private readonly float _wakeUpTime;

        public WakingUpState(float wakeUpTime)
        {
            _wakeUpTime = wakeUpTime;
        }

        public override void Enter(PersonStateMachine stateMachine)
        {
            base.Enter(stateMachine);
            Context.Wait(_wakeUpTime, WakeUp);
            Context.Say("Mm");
        }

        private void WakeUp()
        {
            Context.Say("Holy shit i'm late");
            StateMachine.TransitionTo<GettingReadyState>();
        }
    }
}