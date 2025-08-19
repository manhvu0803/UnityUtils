using UnityEngine;

namespace Vun.UnityUtils.Sample
{
    public class FillSample2 : MonoBehaviour
    {
        [field: AutoFill(FillOption.FromScene), SerializeField]
        public FillSample FillSample { get; private set; }

        [AutoFill(FillOption.FromHierarchy)]
        public Rigidbody[] Rigidbodies;
    }
}