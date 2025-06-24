namespace Vun.UnityUtils.GenericFSM
{
    public class AutoReflectState<TContext> : AutoState<TContext, System.Type>
    {
        protected void TransitionTo<TState>()
        {
            TransitionTo(typeof(TState));
        }
    }
}