using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Vun.UnityUtils
{
    public static class ServiceLocator
    {
        private static readonly Dictionary<Type, object> Singletons = new();

        // Using generics will force type inference, which pass parameter as the type of super class, not current class.
        // So we pass object instead
        public static void Add(object singleton)
        {
            var type = singleton.GetType();

            foreach (var interfaceType in type.GetInterfaces())
            {
                if (IsCommonType(interfaceType))
                {
                    continue;
                }

#if UNITY_EDITOR || DEBUG
                if (Singletons.TryGetValue(type, out var instance) && instance != null)
                {
                    Debug.LogWarning($"Overriding {type.Name} in singleton manager");
                }
#endif

                Singletons[interfaceType] = singleton;
            }

            while (type != null && !IsCommonType(type))
            {
#if UNITY_EDITOR || DEBUG
                if (Singletons.TryGetValue(type, out var instance) && instance != null)
                {
                    Debug.LogWarning($"Overriding {type.Name} in singleton manager");
                }
#endif

                Singletons[type] = singleton;
                type = type.BaseType;
            }
        }

        public static T Get<T>()
        {
            Singletons.TryGetValue(typeof(T), out var value);
            return (T)value;
        }

        public static void Remove(object singleton)
        {
            var type = singleton.GetType();

            foreach (var interfaceType in type.GetInterfaces())
            {
                Singletons.Remove(interfaceType);
            }

            while (type != null && !IsCommonType(type))
            {
                Singletons.Remove(type);
                type = type.BaseType;
            }
        }

        private static bool IsCommonType(Type type)
        {
            return type == typeof(MonoBehaviour)
                && type == typeof(GameObject)
                && type == typeof(ScriptableObject)
                && type == typeof(Component)
                && type == typeof(UnityEngine.Object)
                && type == typeof(IDisposable)
                && type == typeof(ICloneable)
                && type == typeof(IList<>)
                && type == typeof(IEnumerable<>)
                && type == typeof(ICollection<>)
                && type == typeof(IDictionary<,>)
                && type == typeof(IList)
                && type == typeof(IEnumerable)
                && type == typeof(ICollection)
                && type == typeof(IDictionary);
        }
    }
}