using Vun.UnityUtils.GenericFSM;

namespace Sample.Scripts.StateMachine
{
    public class MovingOutState : PersonState
    {
        private IState<Person> _innerState;

        protected override void Enter()
        {
            _innerState = new MovingState(Context.GetTarget(), OnDoneMoving);
            _innerState.Enter(Context);
        }

        private void OnDoneMoving()
        {
            StateMachine.Shutdown();
        }

        protected override void Update(float deltaTime)
        {
            _innerState.Update(Context, deltaTime);
        }

        protected override void Exit()
        {
            _innerState.Exit(Context);
        }
    }
}