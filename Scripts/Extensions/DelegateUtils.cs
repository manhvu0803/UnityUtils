using System;
using UnityEngine;
using UnityEngine.Events;

namespace Vun.UnityUtils
{
    public static class DelegateUtils
    {
        public static void TryInvoke(this Action action)
        {
            try 
            {
                action?.Invoke();
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }

        public static void TryInvoke<T>(this Action<T> action, T arg)
        {
            try
            {
                action?.Invoke(arg);
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }
        
        public static void TryInvoke<T1, T2>(this Action<T1, T2> action, T1 arg1, T2 arg2)
        {
            try
            {
                action?.Invoke(arg1, arg2);
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }

        public static void TryInvoke<T1, T2, T3>(this Action<T1, T2, T3> action, T1 arg1, T2 arg2, T3 arg3)
        {
            try
            {
                action?.Invoke(arg1, arg2, arg3);
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }

        public static void TryInvoke(this UnityEvent unityEvent)
        {
            try
            {
                unityEvent?.Invoke();
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }

        public static void TryInvoke<T>(this UnityEvent<T> unityEvent, T arg)
        {
            try
            {
                unityEvent?.Invoke(arg);
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }
    }
}