using System.Collections.Generic;
using UnityEngine;

namespace Vun.UnityUtils.sample
{
    public class FillSample : SingletonBehaviour<FillSample>, IMoveRandom
    {
        [SerializeField]
        protected AudioSource AudioSource;

        [SerializeField]
        protected Collider[] Colliders;

        [field: SerializeField]
        public List<Rigidbody> ChildrenBodies { get; private set; }

        private readonly List<Rigidbody> _bodyBuffer = new();

        protected void OnValidate()
        {
            this.Fill(ref AudioSource);
            this.Fill(ref Colliders, FillOption.FromAllObjects);
            ChildrenBodies = this.GetIfNull(ChildrenBodies, FillOption.FromChildren);
        }

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
}