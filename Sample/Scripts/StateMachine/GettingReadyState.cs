using System;
using Vun.UnityUtils.GenericFSM;

namespace Sample.Scripts.StateMachine
{
    public class GettingReadyState : PersonState
    {
        private IUpdatableStateMachine<Person, Type> _subStateMachine;

        protected override void Enter()
        {
            var subStateMachine = new CacheAutoStateMachine<Person, Type, PersonState>(Context, typeof(BrushingTeethState), CreateState);
            subStateMachine.ShutdownCompleted += OnSubMachineShutdown;
            _subStateMachine = subStateMachine;
        }

        protected override void Update(float deltaTime)
        {
            _subStateMachine.Update(deltaTime);
        }

        private void OnSubMachineShutdown()
        {
            Context.Say("I'm done with everything");
            StateMachine.Shutdown();
        }

        public static PersonState CreateState(Type type)
        {
            if (type == typeof(BrushingTeethState))
            {
                return new BrushingTeethState();
            }

            if (type == typeof(EatingBreakfastState))
            {
                return new EatingBreakfastState();
            }

            if (type == typeof(MovingOutState))
            {
                return new MovingOutState();
            }

            throw new ArgumentException($"Unknown state: {type}");
        }
    }
}