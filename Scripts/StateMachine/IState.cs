namespace Vun.UnityUtils.GenericFSM
{
    public interface IState<in T>
    {
        /// <param name="context">The target (host) of this state</param>
        public void OnEnter(T context);

        public void Update(float deltaTime);

        /// <summary>
        /// Called when state exit, either when the state machine exit or a new state is transited to.
        /// <b>Do not</b> trigger state transition in here, it might cause an infinite loop
        /// </summary>
        public void OnExit();
    }
}