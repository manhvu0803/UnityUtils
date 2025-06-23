using Vun.UnityUtils.GenericFSM;

namespace Sample.Scripts.StateMachine
{
    internal class EatingBreakfastState : AutoState<Person>
    {
        private float _timeToSay;

        public override void OnEnter(IStateMachine<Person, IAutoState<Person>> stateMachine)
        {
            base.OnEnter(stateMachine);
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
            TransitionTo(new MovingState(Context.GetTarget(), StateMachine.Exit));
        }
    }
}