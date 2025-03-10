namespace Vun.UnityUtils.StateMachine
{
    public interface IState<T>
    {
        public void OnEnter(IStateMachine<T> stateMachine);

        public void Update(float deltaTime);

        public void OnExit();
    }
}