using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Vun.UnityUtils.sample
{
    public class LocatorSample : MonoBehaviour
    {
        public int MoveCount = 2;

        public float TargetPositionX = 12;

        protected IEnumerator Start()
        {
            TestSample();
            
            yield return new WaitForSeconds(3);

            var moveRandom = ServiceLocator.Get<IMoveRandom>();
            moveRandom.MoveRandomBodies(MoveCount, TargetPositionX);

            yield return new WaitForSeconds(3);

            FillSample.Instance.MoveRandomBodies(1, TargetPositionX / 2);

            ICollection<int> array = new int[] { 1, 2, 3 };
            var buffer = new List<int>();
            array.Add(2);
        }

        private void TestSample()
        {
            var list = new List<int> { 1, 2, 3 };
            var buffer = new List<int>();

            for (int i = 0; i < 10; ++i)
            {
                list.QuickSample(2, buffer);
                Debug.Log(buffer.JoinToString());
            }
        }
    }
}