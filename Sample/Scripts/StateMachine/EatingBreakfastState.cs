namespace Sample.Scripts.StateMachine
{
    internal class EatingBreakfastState : PersonState
    {
        private float _timeToSay;

        protected override void Enter()
        {
            Context.Say("Eating");
            Context.Wait(5, FinishEating);
        }

        protected override void Update(float deltaTime)
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