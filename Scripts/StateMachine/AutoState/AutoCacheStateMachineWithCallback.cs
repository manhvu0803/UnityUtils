namespace Vun.UnityUtils.GenericFSM
{
    public class AutoCacheStateMachineWithCallback<TContext, TKey> : AutoCacheStateMachine<TContext, TKey>
    {
        public delegate IAutoState<TContext, TKey> GetStateCallback(TKey stateId);

        private GetStateCallback _stateCreator;

        public AutoCacheStateMachineWithCallback(TContext context, TKey initialStateId, GetStateCallback stateCreator) :
            base(context, initialStateId, stateCreator(initialStateId)) { }

        protected override IAutoState<TContext, TKey> GetState(TKey stateId)
        {
            return _stateCreator(stateId);
        }
    }
}