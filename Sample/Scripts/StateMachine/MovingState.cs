using System;
using UnityEngine;
using Vun.UnityUtils.GenericFSM;

namespace Sample.Scripts.StateMachine
{
    public class MovingState : AutoState<Person>
    {
        private readonly Vector3 _target;

        private readonly Action _doneMovingCallback;

        public MovingState(in Vector3 target, IAutoState<Person> nextState)
        {
            _target = target;
            _doneMovingCallback = () => TransitionTo(nextState);
        }

        public MovingState(in Vector3 target, Action doneMovingCallback)
        {
            _target = target;
            _doneMovingCallback = doneMovingCallback;
        }

        public override void Update(float deltaTime)
        {
            var transform = Context.transform;
            var t = Context.MoveSpeed * deltaTime;
            transform.position = Vector3.Lerp(transform.position, _target, t);

            if (Vector3.SqrMagnitude(_target - transform.position) <= 0.1f)
            {
                _doneMovingCallback?.Invoke();
            }
        }
    }
}