using System;
using Vun.UnityUtils.GenericFSM;

namespace Sample.Scripts.StateMachine
{
    internal class EatingBreakfastState : TypeBasedState<Person>
    {
        private float _timeToSay;

        public override void Enter(IAutoStateMachine<Person, Type> stateMachine)
        {
            base.Enter(stateMachine);
            Context.Say("Eating");
            Context.Wait(5, FinishEating);
        }

        public override void Update(float deltaTime)
        {
            if (_timeToSay <= 0)
            {
                _timeToSay = 1;
                Context.Say("Nom");
            }

            _timeToSay -= deltaTime;
        }

        private void FinishEating()
        {
            Context.Say("Done eating");
            TransitionTo<MovingOutState>();
        }
    }
}