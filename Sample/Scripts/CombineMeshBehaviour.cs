using System.Collections.Generic;
using UnityEngine;

namespace Vun.UnityUtils
{
    public sealed class CombineMeshBehaviour : MonoBehaviour
    {
        [SerializeField]
        private List<MeshFilter> _meshes;

        public bool AutoRunAtStart;

        public bool DestroyOldObjects;

        public bool AddMeshCollider;

        public bool IsMeshColliderConvex;

        private void OnValidate()
        {
            this.Fill(ref _meshes);
        }

        private void Start()
        {
            if (AutoRunAtStart)
            {
                CombineMeshes();
            }
        }

        public void AddToCombine(MeshFilter meshFilter)
        {
            _meshes.Add(meshFilter);
        }

        public GameObject CombineMeshes()
        {
            var meshDataList = new List<CombineMeshBuilder.MeshData>(_meshes.Count);
            Material material = null;

            foreach (var meshFilter in _meshes)
            {
                if (meshFilter == null)
                {
                    continue;
                }

                meshDataList.Add(new CombineMeshBuilder.MeshData
                {
                    Mesh = meshFilter.sharedMesh,
                    TransformMatrix = meshFilter.transform.localToWorldMatrix
                });

                // ReSharper disable once LocalVariableHidesMember
                if (meshFilter.TryGetComponent<Renderer>(out var renderer))
                {
                    material ??= renderer.sharedMaterial;
                }

                if (DestroyOldObjects)
                {
                    meshFilter.gameObject.DestroyUnconditionally();
                }
            }

            var combinedMesh = CombineMeshBuilder.CombineMeshes(meshDataList);
            _meshes.Clear();
            return CreateCombinedObject(combinedMesh, material, gameObject.layer, tag);
        }

        private GameObject CreateCombinedObject(Mesh combinedMesh, Material material, int layer, string newTag)
        {
            var combinedObject = new GameObject("CombinedObject")
            {
                layer = layer,
                tag = newTag
            };

            var combinedMeshFilter = combinedObject.AddComponent<MeshFilter>();
            combinedMeshFilter.sharedMesh = combinedMesh;

            if (material != null)
            {
                var combinedRenderer = combinedObject.AddComponent<MeshRenderer>();
                combinedRenderer.sharedMaterial = material;
            }

            if (!AddMeshCollider)
            {
                return combinedObject;
            }

            // ReSharper disable once LocalVariableHidesMember
            var collider = combinedObject.AddComponent<MeshCollider>();
            collider.sharedMesh = combinedMesh;
            collider.convex = IsMeshColliderConvex;
            return combinedObject;
        }
    }
}