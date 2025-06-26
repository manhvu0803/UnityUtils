using System;
using UnityEngine;
using Vun.UnityUtils.GenericFSM;

namespace Sample.Scripts.StateMachine
{
    public class BrushingTeethState : TypeBasedState<Person>
    {
        private Vector3 _brushPosition;

        private IState<Person> _innerState;

        private bool _isBrushing;

        public override void Enter(IAutoStateMachine<Person, Type> stateMachine)
        {
            base.Enter(stateMachine);
            _innerState = new MovingState(Context.GetTarget(), OnDoneMoving);
            _innerState.Enter(Context);
        }

        private void OnDoneMoving()
        {
            _isBrushing = true;
            Context.Say("Brushing");
            Context.Wait(5, FinishBrushing);
            _brushPosition = Context.transform.position;
        }

        public override void Update(float deltaTime)
        {
            if (_isBrushing)
            {
                var height = Mathf.Sin(Time.time * 20) * 0.5f;
                Context.transform.position = _brushPosition + new Vector3(0, height, 0);
                return;
            }

            _innerState.Update(deltaTime);
        }

        private void FinishBrushing()
        {
            Context.Say("Done brushing");
            TransitionTo<EatingBreakfastState>();
        }
    }
}