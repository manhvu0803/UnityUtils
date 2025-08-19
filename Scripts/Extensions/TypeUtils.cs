using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Vun.UnityUtils
{
    public static class TypeUtils
    {
        public static bool IsCommonType(this Type type)
        {
#if UNITY_5_3_OR_NEWER
            return type == typeof(object)
                || type == typeof(Component)
                || type == typeof(MonoBehaviour)
                || type == typeof(Transform)
                || type == typeof(RectTransform)
                || type == typeof(GameObject)
                || type == typeof(ScriptableObject)
                || type == typeof(UnityEngine.Object);
#else
            return type == typeof(object);
#endif
        }

        public static bool IsCommonInterface(this Type type)
        {
            return type == typeof(IDisposable)
                || type == typeof(ICloneable)
                || type == typeof(IList<>)
                || type == typeof(IEnumerable<>)
                || type == typeof(ICollection<>)
                || type == typeof(IDictionary<,>)
                || type == typeof(IList)
                || type == typeof(IEnumerable)
                || type == typeof(ICollection)
                || type == typeof(IDictionary)
                || type == typeof(IReadOnlyCollection<>)
                || type == typeof(IReadOnlyDictionary<,>)
                || type == typeof(IReadOnlyList<>);
        }

        public static bool IsEcsType(this Type type)
        {
            // TODO: Add custom authoring type and collider type check
            return type == typeof(Transform)
                || type == typeof(Rigidbody)
                || type == typeof(MeshRenderer);
        }
    }
}