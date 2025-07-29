namespace Vun.UnityUtils.GenericFSM
{
    public interface IState<in TContext>
    {
        /// <param name="context">The target (host) of this state</param>
        public void Enter(TContext context);

        public void Update(float deltaTime);

        /// <summary>
        /// Called when state exit, either when the state machine exit or a new state is transited to.
        /// <b>Do not</b> trigger state transition in here, it might cause an infinite loop
        /// </summary>
        public void Exit();
    }
}