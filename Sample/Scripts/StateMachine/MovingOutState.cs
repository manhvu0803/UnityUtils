using System;
using Vun.UnityUtils.GenericFSM;

namespace Sample.Scripts.StateMachine
{
    public class MovingOutState : TypeBasedState<Person>
    {
        private IState<Person> _innerState;

        public override void Enter(IAutoStateMachine<Person, Type> stateMachine)
        {
            base.Enter(stateMachine);
            _innerState = new MovingState(Context.GetTarget(), OnDoneMoving);
            _innerState.Enter(Context);
        }

        private void OnDoneMoving()
        {
            StateMachine.Shutdown();
        }

        public override void Update(float deltaTime)
        {
            _innerState.Update(deltaTime);
        }

        public override void Exit()
        {
            base.Exit();
            _innerState.Exit();
        }
    }
}