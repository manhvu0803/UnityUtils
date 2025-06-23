using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Sample.Scripts.StateMachine
{
    public class Person : MonoBehaviour
    {
        private static IEnumerator WaitRoutine(float seconds, Action waitDoneCallback)
        {
            yield return new WaitForSeconds(seconds);
            waitDoneCallback.Invoke();
        }

        public string Name;

        public int Age;

        public string Sex;

        public float MoveSpeed = 5;

        public void Say(string something)
        {
            Debug.Log($"{Name} said: {something}");
        }

        public void Wait(float seconds, Action waitDoneCallback)
        {
            StartCoroutine(WaitRoutine(seconds, waitDoneCallback));
        }

        public Vector3 GetTarget()
        {
            return new Vector3
            {
                x = Random.Range(-10f, 10f),
                z = Random.Range(-10f, 10f)
            };
        }
    }
}