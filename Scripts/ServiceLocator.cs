using System;
using System.Collections;
using System.Collections.Generic;
#if UNITY_5_3_OR_NEWER
using UnityEngine;
#endif

namespace Vun.UnityUtils
{
    /// <summary>
    /// A simple Service Locator implementation with reflection
    /// </summary>
    public static class ServiceLocator
    {
        // While I dislike having state in a static class, this implementation is simple and effective
        private static readonly Dictionary<Type, object> Services = new();

        /// <summary>
        /// Register <c>service</c> as all of its interfaces and super classes
        /// </summary>
        // Using generics will force type inference, which pass parameter as the type of super class, not current class.
        // So we pass object instead
        public static void Add(object service)
        {
            var type = service.GetType();

            foreach (var interfaceType in type.GetInterfaces())
            {
                if (IsCommonInterface(interfaceType))
                {
                    continue;
                }

                CheckRegisteredServices(type);
                Services[interfaceType] = service;
            }

            while (type != null && !IsCommonType(type))
            {
                CheckRegisteredServices(type);
                Services[type] = service;
                type = type.BaseType;
            }
        }

        /// <summary>
        /// Register <c>service</c> as type <c>T</c>
        /// </summary>
        // This receives object as param instead of T so that it doesn't take priority over Add(object)
        public static void Add<T>(object service)
        {
            var type = typeof(T);

#if UNITY_EDITOR || DEBUG || STRICT_MODE
            if (service is not T)
            {
                throw new ArgumentException($"{service} is not of type {type.FullName}");
            }
#endif

            CheckRegisteredServices(type);
            Services[type] = service;
        }

        private static void CheckRegisteredServices(Type type)
        {
#if UNITY_EDITOR || DEBUG
            if (Services.TryGetValue(type, out var instance) && instance != null)
            {
                Debug.LogWarning($"Overriding {type.FullName} in ServiceLocator");
            }
#endif
        }

        /// <summary>
        /// Get the registered service of type <c>T</c>. Can be <c>null</c>
        /// </summary>
        public static T Get<T>()
        {
            Services.TryGetValue(typeof(T), out var value);
            return (T)value;
        }

        /// <summary>
        /// Remove <c>service</c> from all registered interfaces and classes
        /// </summary>
        public static void Remove(object service)
        {
            var type = service.GetType();

            foreach (var interfaceType in type.GetInterfaces())
            {
                if (!IsCommonInterface(interfaceType))
                {
                    RemoveIfMatch(service, interfaceType);
                }
            }

            while (type != null && !IsCommonType(type))
            {
                RemoveIfMatch(service, type);
                type = type.BaseType;
            }
        }

        /// <summary>
        /// Remove <c>service</c> from registered type <c>T</c>
        /// </summary>
        public static void Remove<T>(object service)
        {
            RemoveIfMatch(service, service.GetType());
        }

        private static bool IsCommonType(Type type)
        {
#if UNITY_5_3_OR_NEWER
            return type == typeof(object)
                || type == typeof(MonoBehaviour)
                || type == typeof(GameObject)
                || type == typeof(ScriptableObject)
                || type == typeof(Component)
                || type == typeof(UnityEngine.Object);
#else
            return type == typeof(object);
#endif
        }

        private static bool IsCommonInterface(Type type)
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

        private static void RemoveIfMatch(object target, Type type)
        {
            if (Services.TryGetValue(type, out var service) && service == target)
            {
                Services.Remove(type);
            }
        }
    }
}