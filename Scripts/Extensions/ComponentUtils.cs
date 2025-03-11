using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Vun.UnityUtils
{
    public static class ComponentUtils
    {
        #region Fills
        
        /// <summary>
        /// If <c>target</c> is null, find a component of type <c>T</c> according to <c>FillOption</c> and assign it to <c>target</c>.
        /// Should be use in <c>OnValidate</c> or editor functions
        /// </summary>
        /// <typeparam name="T">Type of targeted component</typeparam>
        /// <param name="component">If <c>option</c> is <c>FromGameObject</c>, <c>FromChildren</c> or <c>FromParent</c>, this will be the starting point</param>
        /// <param name="target">If null, find a component according to <c>option</c> and assign to this</param>
        public static void Fill<T>(this Component component, ref T target, FillOption option = FillOption.FromGameObject) where T : Component
        {
            if (target != null)
            {
                return;
            }

            target = option switch
            {
                FillOption.FromGameObject => component.GetComponent<T>(),
                FillOption.FromChildren => component.GetComponentInChildren<T>(),
                FillOption.FromParent => component.GetComponentInParent<T>(),
                FillOption.FromActiveObjects => Object.FindAnyObjectByType<T>(),
                FillOption.FromAllObjects => Object.FindAnyObjectByType<T>(FindObjectsInactive.Include),
                _ => null
            };
        }

        /// <summary>
        /// If <c>target</c> is null, find a component of type <c>T</c> according to <c>FillOption</c> and assign it to <c>target</c>.
        /// Should be use in <c>OnValidate</c> or editor functions
        /// </summary>
        /// <typeparam name="T">Type of targeted component</typeparam>
        /// <param name="component">If <c>option</c> is <c>FromGameObject</c>, <c>FromChildren</c> or <c>FromParent</c>, this will be the starting point</param>
        /// <param name="target">If null or empty, find components according to <c>option</c> and assign to this</param>
        public static void Fill<T>(
            this Component component,
            ref T[] target,
            FillOption option = FillOption.FromGameObject,
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
                FillOption.FromChildren => component.GetComponentsInChildren<T>(),
                FillOption.FromParent => component.GetComponentsInParent<T>(),
                FillOption.FromActiveObjects => Object.FindObjectsByType<T>(FindObjectsInactive.Exclude, sortMode),
                FillOption.FromAllObjects => Object.FindObjectsByType<T>(FindObjectsInactive.Include, sortMode),
                _ => target
            };
        }

        /// <summary>
        /// If <c>target</c> is null, find a component of type <c>T</c> according to <c>FillOption</c> and assign it to <c>target</c>.
        /// Should be use in <c>OnValidate</c> or editor functions
        /// </summary>
        /// <typeparam name="T">Type of targeted component</typeparam>
        /// <param name="component">If <c>option</c> is <c>FromGameObject</c>, <c>FromChildren</c> or <c>FromParent</c>, this will be the starting point</param>
        /// <param name="target">If null or empty, find components according to <c>option</c> and assign to this</param>
        public static void Fill<T>(
            this Component component,
            ref List<T> target,
            FillOption option = FillOption.FromGameObject,
            FindObjectsSortMode sortMode = FindObjectsSortMode.None)
            where T : Component
        {
            if (target is { Count: > 0 })
            {
                return;
            }

            target ??= new List<T>();
            target.AddRange(component.GetIfNull((T[])null, option, sortMode));
        }

        /// <summary>
        /// For auto properties and fields that can't be passed with <c>ref</c>
        /// </summary>
        /// <returns>Found component if target is null, otherwise return target</returns>
        public static T GetIfNull<T>(Component component, T target, FillOption option = FillOption.FromGameObject) where T : Component
        {
            component.Fill(ref target, option);
            return target;
        }

        /// <summary>
        /// For auto properties and fields that can't be passed with <c>ref</c>
        /// </summary>
        /// <returns>Found components if target is null, otherwise return target</returns>
        public static T[] GetIfNull<T>(this Component component,
            T[] target,
            FillOption option = FillOption.FromGameObject,
            FindObjectsSortMode sortMode = FindObjectsSortMode.None)
            where T : Component
        {
            component.Fill(ref target, option, sortMode);
            return target;
        }

        /// <summary>
        /// For auto properties and fields that can't be passed with <c>ref</c>
        /// </summary>
        /// <returns>Found components if target is null, otherwise return target</returns>
        public static List<T> GetIfNull<T>(this Component component,
            List<T> target,
            FillOption option = FillOption.FromGameObject,
            FindObjectsSortMode sortMode = FindObjectsSortMode.None)
            where T : Component
        {
            component.Fill(ref target, option, sortMode);
            return target;
        }

        #endregion

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
    }
}