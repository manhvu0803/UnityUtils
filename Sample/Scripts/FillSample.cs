using System;
using System.Collections;
using System.Collections.Generic;
using Sample.Scripts;
using UnityEngine;

namespace Vun.UnityUtils.sample
{
    public class FillSample : MonoBehaviour, IMoveRandom
    {
        [SerializeField, AutoFill]
        protected AudioSource AudioSource;

        [SerializeField, AutoFill(FillOption.FromScene)]
        protected Collider[] Colliders;

        [field: SerializeField, AutoFill(FillOption.FromChildren)]
        public List<Rigidbody> ChildrenBodies { get; private set; }

        [field: SerializeField, AutoFill(FillOption.FromScene, includeInactive: true)]
        private CustomCollection<Rigidbody> _bodyBuffer = new();

        [SerializeField, AutoFill]
        private FillSample2 _fillSample2;

        [field: AutoFill]
        public Collider Collider { get; private set; }

        protected void Start()
        {
            Debug.Log(transform.GetHierarchyInfo());
        }

        public void MoveRandomBodies(int count = 4, float positionX = 15.2f)
        {
            ChildrenBodies.QuickSample(count, _bodyBuffer);

            foreach (var body in _bodyBuffer)
            {
                Debug.Log(body.name);
                body.transform.SetPositionX(positionX);
            }
        }
    }

    [Serializable]
    public class CustomCollection<T> : ICollection<T>
    {
        [SerializeField]
        private List<T> _list = new();

        public IEnumerator<T> GetEnumerator()
        {
            return _list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        public void Add(T item)
        {
            _list.Add(item);
        }

        public void Clear()
        {
            _list.Clear();
        }

        public bool Contains(T item)
        {
            return _list.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            _list.CopyTo(array, arrayIndex);
        }

        public bool Remove(T item)
        {
            return _list.Remove(item);
        }

        public int Count => _list.Count;

        public bool IsReadOnly => ((ICollection<T>)_list).IsReadOnly;
    }
}