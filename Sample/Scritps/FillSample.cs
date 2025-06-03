using System.Collections.Generic;
using UnityEngine;

namespace Vun.UnityUtils.sample
{
    public class FillSample : MonoBehaviour, IMoveRandom
    {
        [SerializeField]
        protected AudioSource AudioSource;

        [SerializeField]
        protected Collider[] Colliders;

        [field: SerializeField]
        public List<Rigidbody> ChildrenBodies { get; private set; }

        private List<Rigidbody> _bodyBuffer = new();

        public Collider Collider { get; private set; }

        protected void OnValidate()
        {
            this.Fill(ref AudioSource);
            this.Fill(ref Colliders, FillOption.FromAllObjects);
            this.Fill(ref _bodyBuffer, FillOption.FromAllObjects);
            ChildrenBodies = this.GetIfNull(ChildrenBodies);
            Collider = this.GetIfNull(Collider);
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