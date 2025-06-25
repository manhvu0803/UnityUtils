namespace Vun.UnityUtils.GenericFSM
{
    public interface IUpdatableStateMachine<in T> : IStateMachine<T>, IUpdatable { }
}