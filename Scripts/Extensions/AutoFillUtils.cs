using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Vun.UnityUtils
{
    public static class AutoFillUtils
    {
        #region Single component

        /// <summary>
        /// If <c>target</c> is null, find a component of type and assign it to <c>target</c>.
        /// Should be use in <c>OnValidate</c> or editor functions
        /// </summary>
        /// <param name="component">The search starting point</param>
        /// <param name="target">If <c>null</c>, find a component according to <c>option</c> and assign to this</param>
        public static void Fill<T>(
            this Component component,
            ref T target,
            FillOption option = FillOption.FromGameObject,
            bool includeInactive = true)
            where T : Component
        {
            if (target != null)
            {
                return;
            }

            target = option switch
            {
                FillOption.FromGameObject => component.GetComponent<T>(),
                FillOption.FromChildren => component.GetComponentInChildren<T>(includeInactive),
                FillOption.FromParent => component.GetComponentInParent<T>(includeInactive),
                FillOption.FromHierarchy => component.transform.root.GetComponentInChildren<T>(includeInactive),
                FillOption.FromScene => Object.FindAnyObjectByType<T>(GetEnum(includeInactive)),
                _ => null
            };
        }

        public static Object GetComponent(
            this Component component,
            Type type,
            FillOption option = FillOption.FromGameObject,
            bool includeInactive = true)
        {
            return option switch
            {
                FillOption.FromGameObject => component.GetComponent(type),
                FillOption.FromChildren => component.GetComponentInChildren(type, includeInactive),
                FillOption.FromParent => component.GetComponentInParent(type, includeInactive),
                FillOption.FromHierarchy => component.transform.root.GetComponentInParent(type, includeInactive),
                FillOption.FromScene => Object.FindAnyObjectByType(type, GetEnum(includeInactive)),
                _ => null
            };
        }

        #endregion

        /// <summary>
        /// If <c>target</c> is null, find components of type <c>T</c> and assign it to <c>target</c>.
        /// Should be use in <c>OnValidate</c> or editor functions
        /// </summary>
        /// <param name="component">The search starting point</param>
        /// <param name="target">If null or empty, find a component and assign to this</param>
        public static void Fill<T>(
            this Component component,
            ref T[] target,
            FillOption option = FillOption.FromChildren,
            bool includeInactive = true,
            FindObjectsSortMode sortMode = FindObjectsSortMode.None)
            where T : Component
        {
            if (target is { Length: > 0 })
            {
                return;
            }

            target = option switch
            {
                FillOption.FromGameObject => component.GetComponents<T>(),
                FillOption.FromChildren => component.GetComponentsInChildren<T>(includeInactive),
                FillOption.FromParent => component.GetComponentsInParent<T>(includeInactive),
                FillOption.FromHierarchy => component.GetComponentsInHierarchy<T>(includeInactive),
                FillOption.FromScene => Object.FindObjectsByType<T>(GetEnum(includeInactive), sortMode),
                _ => target
            };
        }

        public static T[] GetComponentsInHierarchy<T>(this Component component, bool includeInactive = true) where T : Component
        {
            return component.transform.root.GetComponentsInChildren<T>(includeInactive);
        }

        private static void GetComponentInHierarchy<T>(this Component component, List<T> buffer, bool includeInactive = true) where T : Component
        {
            component.transform.root.GetComponentsInChildren(includeInactive, buffer);
        }

        public static Array GetComponents(
            this Component component,
            Type type,
            FillOption option = FillOption.FromChildren,
            bool includeInactive = true,
            FindObjectsSortMode sortMode = FindObjectsSortMode.None)
        {
            var components = option switch
            {
                FillOption.FromGameObject => component.GetComponents(type),
                FillOption.FromChildren => component.GetComponentsInChildren(type, includeInactive),
                FillOption.FromParent => component.GetComponentsInParent(type, includeInactive),
                FillOption.FromHierarchy => component.GetComponentsInHierarchy(type, includeInactive),
                _ => Array.Empty<Component>()
            };

            if (components.Length > 0)
            {
                // ReSharper disable once CoVariantArrayConversion
                return CreateArray(type, components);
            }

            var objects = Object.FindObjectsByType(type, GetEnum(includeInactive), sortMode);
            return CreateArray(type, objects);
        }

        public static Component[] GetComponentsInHierarchy(this Component component, Type type, bool includeInactive = true)
        {
            return component.transform.root.GetComponentsInChildren(type, includeInactive);
        }

        private static Array CreateArray(Type type, Object[] objects)
        {
            var array = Array.CreateInstance(type, objects.Length);

            for (var i = 0; i < objects.Length; i++)
            {
                array.SetValue(objects[i], i);
            }

            return array;
        }

        /// <summary>
        /// If <c>target</c> is null, find a component of type <c>T</c> and assign it to <c>target</c>.
        /// Should be use in <c>OnValidate</c> or editor functions
        /// </summary>
        /// <typeparam name="T">Type of targeted component</typeparam>
        /// <param name="component">The search starting point</param>
        /// <param name="target">If null or empty, find components according to <c>option</c> and assign to this</param>
        public static void Fill<T>(
            this Component component,
            ref List<T> target,
            FillOption option = FillOption.FromChildren,
            bool includeInactive = true,
            FindObjectsSortMode sortMode = FindObjectsSortMode.None)
            where T : Component
        {
            if (target is { Count: > 0 })
            {
                return;
            }

            target ??= new List<T>();
            target.AddRange(component.GetIfNull((T[])null, option, includeInactive, sortMode));
        }

        public static FindObjectsInactive GetEnum(this bool includeInactive)
        {
            return includeInactive ? FindObjectsInactive.Include : FindObjectsInactive.Exclude;
        }

        #region GetIfNull (for properties)

        /// <summary>
        /// For auto properties and fields that can't be passed with <c>ref</c>
        /// </summary>
        /// <returns>Found component if target is null, otherwise return target</returns>
        public static T GetIfNull<T>(this Component component, T target, FillOption option = FillOption.FromGameObject) where T : Component
        {
            component.Fill(ref target, option);
            return target;
        }

        /// <summary>
        /// For auto properties and fields that can't be passed with <c>ref</c>
        /// </summary>
        /// <returns>Found components if target is null, otherwise return target</returns>
        public static T[] GetIfNull<T>(
            this Component component,
            T[] target,
            FillOption option = FillOption.FromChildren,
            bool includeInactive = true,
            FindObjectsSortMode sortMode = FindObjectsSortMode.None)
            where T : Component
        {
            component.Fill(ref target, option, includeInactive, sortMode);
            return target;
        }

        /// <summary>
        /// For auto properties and fields that can't be passed with <c>ref</c>
        /// </summary>
        /// <returns>Found components if target is null, otherwise return target</returns>
        public static List<T> GetIfNull<T>(
            this Component component,
            List<T> target,
            FillOption option = FillOption.FromChildren,
            bool includeInactive = true,
            FindObjectsSortMode sortMode = FindObjectsSortMode.None)
            where T : Component
        {
            component.Fill(ref target, option, includeInactive, sortMode);
            return target;
        }

        #endregion
    }
}