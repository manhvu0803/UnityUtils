namespace Vun.UnityUtils.GenericFSM
{
    /// <summary>
    /// A bare minimum class implementation of <see cref="IState{T}"/>
    /// </summary>
    public class State<T> : IState<T>
    {
        protected T Context { get; private set; }

        public virtual void Enter(T context)
        {
            Context = context;
        }

        public virtual void Update(float deltaTime) { }

        public virtual void Exit()
        {
            Context = default;
        }
    }
}