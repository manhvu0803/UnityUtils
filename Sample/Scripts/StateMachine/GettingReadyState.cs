using System;
using Vun.UnityUtils;
using Vun.UnityUtils.GenericFSM;

namespace Sample.Scripts.StateMachine
{
    public class GettingReadyState : TypeBasedCompositeState<Person, Type>, ICreator<IAutoState<Person, Type>, Type>
    {
        private IUpdatableAutoStateMachine<Person,Type> _subStateMachine;

        protected override IUpdatableAutoStateMachine<Person, Type> SubStateMachine => _subStateMachine;

        public override void Enter(IAutoStateMachine<Person, Type> stateMachine)
        {
            base.Enter(stateMachine);
            _subStateMachine = new AutoCacheStateMachine<Person, Type>(Context, typeof(BrushingTeethState), this);
            SubStateMachine.OnShutdown += OnSubMachineShutdown;
        }

        public override void Update(float deltaTime)
        {
            SubStateMachine.Update(deltaTime);
        }

        private void OnSubMachineShutdown()
        {
            Context.Say("I'm done with everything");
            StateMachine.Shutdown();
        }

        public IAutoState<Person, Type> Create(Type type)
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