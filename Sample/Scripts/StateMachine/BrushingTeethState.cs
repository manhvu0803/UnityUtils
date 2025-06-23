using UnityEngine;
using Vun.UnityUtils.GenericFSM;

namespace Sample.Scripts.StateMachine
{
    public class BrushingTeethState : AutoState<Person>
    {
        private Vector3 _originalPosition;

        public override void OnEnter(IStateMachine<Person, IAutoState<Person>> stateMachine)
        {
            base.OnEnter(stateMachine);
            _originalPosition = Context.transform.position;
            Context.Say("Brushing");
            Context.Wait(5, FinishBrushing);
        }

        public override void Update(float deltaTime)
        {
            var height = Mathf.Sin(Time.time * 10);
            Context.transform.position = _originalPosition + new Vector3(0, height, 0);
        }

        private void FinishBrushing()
        {
            Context.Say("Done brushing");
            TransitionTo(new MovingState(Context.GetTarget(), new EatingBreakfastState()));
        }
    }
}