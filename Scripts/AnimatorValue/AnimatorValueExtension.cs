using UnityEngine;

namespace Vun.UnityUtils
{
    public static class AnimatorValueExtensions
    {
        /// <summary>
        /// Wrapper for <see cref="AnimatorValue.ApplyTo"/> for convenience.
        /// This will <b>automatically</b> send the correct value type to <c>animator</c>
        /// </summary>
        public static void SetValue(this Animator animator, in AnimatorValue animatorValue)
        {
            animatorValue.ApplyTo(animator);
        }

        /// <summary>
        /// Wrapper for <see cref="AnimatorValue.ApplyTo"/> for convenience.
        /// This will <b>automatically</b> send the correct value type to <c>animator</c>
        /// </summary>
        public static void SetInt(this Animator animator, in AnimatorValue animatorValue)
        {
            animatorValue.ApplyTo(animator);
        }

        /// <summary>
        /// Wrapper for <see cref="AnimatorValue.ApplyTo"/> for convenience.
        /// This will <b>automatically</b> send the correct value type to <c>animator</c>
        /// </summary>
        public static void SetFloat(this Animator animator, in AnimatorValue animatorValue)
        {
            animatorValue.ApplyTo(animator);
        }

        /// <summary>
        /// Wrapper for <see cref="AnimatorValue.ApplyTo"/> for convenience.
        /// This will <b>automatically</b> send the correct value type to <c>animator</c>
        /// </summary>
        public static void SetTrigger(this Animator animator, in AnimatorValue animatorValue)
        {
            animatorValue.ApplyTo(animator);
        }

        /// <summary>
        /// Wrapper for <see cref="AnimatorValue.ApplyTo"/> for convenience.
        /// This will <b>automatically</b> send the correct value type to <c>animator</c>
        /// </summary>
        public static void ResetTrigger(this Animator animator, in AnimatorValue animatorValue)
        {
            animatorValue.ApplyTo(animator);
        }

        /// <summary>
        /// Wrapper for <see cref="AnimatorValue.ApplyTo"/> for convenience.
        /// This will <b>automatically</b> send the correct value type to <c>animator</c>
        /// </summary>
        public static void SetBool(this Animator animator, in AnimatorValue animatorValue)
        {
            animatorValue.ApplyTo(animator);
        }
    }
}