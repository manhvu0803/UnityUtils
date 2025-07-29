using UnityEngine;

namespace Vun.UnityUtils.GenericFSM
{
    /// <summary>
    /// A bare minimum class implementation of <see cref="IState{T}"/>
    /// </summary>
    public class State<T> : IState<T>
    {
        private T Context;

        protected T Context
        {
            get
            {
#if DEBUG
                if (_context == null)
                {
                    Debug.LogError($"Context of {this} is null. Check if Enter() or Exit() has been called for this state");
                }
#endif

                return _context;
            }

            private set => _context = value;
        }

        public virtual void Enter(T context)
        {
            Context = context;
        }

        public virtual void Update(float deltaTime) { }

        public virtual void Exit()
        {
            Context = default;
        }
    }
}