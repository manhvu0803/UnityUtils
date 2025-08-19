using UnityEngine;
using Object = UnityEngine.Object;

namespace Vun.UnityUtils
{
    public static class ComponentUtils
    {
        #region SetActive and SetEnable

        public static void TrySetActive(this GameObject gameObject, bool value)
        {
            if (gameObject != null)
            {
                gameObject.SetActive(value);
            }
        }

        public static void TrySetActive(this Component component, bool value)
        {
            if (component != null)
            {
                component.gameObject.SetActive(value);
            }
        }

        public static void TrySetEnable(this MonoBehaviour behaviour, bool value)
        {
            if (behaviour != null)
            {
                behaviour.enabled = value;
            }
        }

        public static void TrySetEnable(this Collider collider, bool value)
        {
            if (collider != null)
            {
                collider.enabled = value;
            }
        }

        #endregion

        /// <summary>
        /// Destroy <c>unityObject</c>, whether in build, play mode or edit mode
        /// </summary>
        public static void DestroyUnconditionally(this Object unityObject)
        {
            if (unityObject == null)
            {
                return;
            }

#if UNITY_EDITOR
            if (!Application.isPlaying)
            {
                Object.DestroyImmediate(unityObject);
                return;
            }
#endif

            Object.Destroy(unityObject);
        }
    }
}