namespace Vun.UnityUtils.GenericFSM
{
    /// <summary>
    /// A state interface that can be applied to anything from sharable stateless <see cref="UnityEngine.ScriptableObject"/>
    /// to stateful state <c>struct</c> that is unique for each context
    /// </summary>
    /// <typeparam name="TContext">The type of target (host) of this state</typeparam>
    public interface IState<in TContext>
    {
        /// <param name="context">The target (host) of this state</param>
        public void Enter(TContext context);

        public void Update(TContext context, float deltaTime);

        /// <summary>
        /// Exit the state, either when the state machine exit or a new state is transited to.<br/>
        /// <b>Do not</b> trigger state transition in here, it might cause an infinite loop
        /// </summary>
        public void Exit(TContext context);
    }
}