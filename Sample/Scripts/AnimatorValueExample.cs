using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Vun.UnityUtils.Sample
{
    public class AnimatorValueExample : MonoBehaviour
    {
        [Serializable]
        public class Scope
        {
            [AnimatorValueTarget]
            public Animator Animator;

            public AnimatorValue Param1;

            public AnimatorValue Param3;
        }

        [DrawWithUnity]
        public Scope InnerScope;

        [Space]
        public AnimatorValue Param;

        private void Start()
        {
            InnerScope.Animator.SetValue(Param);
        }
    }
}