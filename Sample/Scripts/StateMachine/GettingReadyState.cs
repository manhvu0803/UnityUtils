using Vun.UnityUtils.GenericFSM;

namespace Sample.Scripts.StateMachine
{
    public class GettingReadyState : CompositeAutoState<Person>
    {
        protected override IAutoState<Person> InitialState => new MovingState(Context.GetTarget(), new BrushingTeethState());

        public override void Exit()
        {
            Context.Say("I'm done with everything");
            StateMachine.Exit();
            base.Exit();
        }
    }
}