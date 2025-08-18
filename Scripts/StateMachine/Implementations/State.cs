namespace Vun.UnityUtils.GenericFSM
{
    /// <summary>
    /// A bare minimum stateful implementation of <see cref="IState{T}"/>
    /// </summary>
    public class State<TContext> : IState<TContext>
    {
        protected TContext Context { get; private set; }

        public void Enter(TContext context)
        {
            Context = context;
        }

        protected virtual void Enter() { }

        public void Update(TContext context, float deltaTime)
        {
            Update(deltaTime);
        }

        protected virtual void Update(float deltaTime) { }

        public void Exit(TContext context)
        {
            Exit();
            Context = default;
        }

        protected virtual void Exit()
        {
        }
    }
}