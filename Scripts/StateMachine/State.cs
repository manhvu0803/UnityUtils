namespace Vun.UnityUtils.StateMachine
{
    public class State<T> : IState<T>
    {
        protected IStateMachine<T> StateMachine;

        protected T Owner => StateMachine.Owner;

        public void TransitionTo(State<T> state)
        {
            StateMachine.TransitionTo(state);
        }

        public void OnEnter(IStateMachine<T> stateMachine)
        {
            StateMachine = stateMachine;
        }

        public void Update(float deltaTime) { }

        public void OnExit()
        {
            StateMachine = null;
        }
    }
}