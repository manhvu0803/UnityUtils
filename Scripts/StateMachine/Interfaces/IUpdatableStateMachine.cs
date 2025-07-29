namespace Vun.UnityUtils.GenericFSM
{
    public interface IUpdatableStateMachine<out TContext, in TState> : IStateMachine<TContext, TState>
    {
        public void Update(float deltaTime);
    }
}