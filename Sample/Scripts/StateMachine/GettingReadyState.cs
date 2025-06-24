using Vun.UnityUtils.GenericFSM;

namespace Sample.Scripts.StateMachine
{
    public class GettingReadyState : AutoCompositeState<Person>
    {
        protected override IAutoState<Person> InitialState => new MovingState(Context.GetTarget(), new BrushingTeethState());

        public override void Shutdown()
        {
            Context.Say("I'm done with everything");
            StateMachine.Shutdown();
            base.Exit();
        }
    }
}