namespace Vun.UnityUtils.StateMachine
{
    public interface IStateMachine<T>
    {
        public T Owner { get; }

        public void TransitionTo(IState<T> state);

        public void Exit();
    }
}