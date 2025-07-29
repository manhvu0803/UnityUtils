using System;
using Vun.UnityUtils.GenericFSM;

namespace Sample.Scripts.StateMachine
{
    public class GettingReadyState : PersonState, IStateMachine<>
    {
        private IUpdatableStateMachine<Type> _subStateMachine;

        public override void Enter(PersonStateMachine stateMachine)
        {
            base.Enter(stateMachine);
            _subStateMachine = new CacheStateMachine<PersonStateMachine, Type, IState<PersonStateMachine>>(stateMachine, typeof(BrushingTeethState), CreateState);
        }

        public override void Update(float deltaTime)
        {
            _subStateMachine.Update(deltaTime);
        }

        private void OnSubMachineShutdown()
        {
            Context.Say("I'm done with everything");
            StateMachine.Shutdown();
        }

        public static IState<PersonStateMachine> CreateState(Type type)
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