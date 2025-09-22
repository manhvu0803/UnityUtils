using System;
using UnityEngine;

namespace Vun.UnityUtils
{
    /// <summary>
    /// A wrapper class for an animator parameter with custom drawer support
    /// </summary>
    [Serializable]
    public struct AnimatorValue
    {
#if UNITY_EDITOR
        // Editor-only variables for inspector logic. Don't touch these unless you know what are you doing
        public UnityEditor.Animations.AnimatorController Controller;
#endif

        public AnimatorControllerParameterType Type;

        public int AnimatorId;

        public int IntValue;

        public float FloatValue;

        public bool BoolValue;

        /// <summary>
        /// Automatically set the value of a parameter of <c>animator</c>
        /// base on <see cref="Type"/> and <see cref="AnimatorId"/>
        /// </summary>
        public readonly void ApplyTo(Animator animator)
        {
            switch (Type)
            {
                case AnimatorControllerParameterType.Bool:
                    animator.SetBool(AnimatorId, BoolValue);
                    break;
                case AnimatorControllerParameterType.Int:
                    animator.SetInteger(AnimatorId, IntValue);
                    break;
                case AnimatorControllerParameterType.Float:
                    animator.SetFloat(AnimatorId, FloatValue);
                    break;
                case AnimatorControllerParameterType.Trigger:
                    if (BoolValue)
                    {
                        animator.SetTrigger(AnimatorId);
                    }
                    else
                    {
                        animator.ResetTrigger(AnimatorId);
                    }

                    break;
            }
        }
    }
}