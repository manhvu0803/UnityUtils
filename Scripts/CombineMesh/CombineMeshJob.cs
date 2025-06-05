#if UNITY_BURST_EXISTS
using Unity.Burst;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Rendering;

namespace Vun.UnityUtils
{
    /// <remarks>Adapt from https://github.com/Unity-Technologies/MeshApiExamples</remarks>
    [BurstCompile]
    public struct CombineMeshJob : IJobParallelFor
    {
        private static void PrepareTempArray<T>(int vertexCount, ref NativeArray<T> array) where T : struct
        {
            switch (array.IsCreated)
            {
                case true when array.Length >= vertexCount:
                    return;
                case true:
                    array.Dispose();
                    break;
            }

            array = new NativeArray<T>(vertexCount, Allocator.Temp, NativeArrayOptions.UninitializedMemory);
        }

        private static void InitJobDataArray<T>(int meshCount, out NativeArray<T> array) where T : struct
        {
            array = new NativeArray<T>(meshCount, Allocator.TempJob, NativeArrayOptions.UninitializedMemory);
        }

        [ReadOnly]
        public Mesh.MeshDataArray MeshDataArray;

        public Mesh.MeshData OutputMeshData;

        [ReadOnly, DeallocateOnJobCompletion]
        public NativeArray<int> VertexStartIndices;

        [ReadOnly, DeallocateOnJobCompletion]
        public NativeArray<int> MeshStartIndices;

        [ReadOnly, DeallocateOnJobCompletion]
        public NativeArray<float4x4> TransformMatrices;

        public NativeArray<float3x2> Bounds;

        [NativeDisableContainerSafetyRestriction]
        private NativeArray<float3> _tempVertices;

        [NativeDisableContainerSafetyRestriction]
        private NativeArray<float3> _tempNormals;

        public void CreateInputArrays(int meshCount)
        {
            InitJobDataArray(meshCount, out VertexStartIndices);
            InitJobDataArray(meshCount, out MeshStartIndices);
            InitJobDataArray(meshCount, out TransformMatrices);
            InitJobDataArray(meshCount, out Bounds);
        }

        public void Execute(int index)
        {
            var meshData = MeshDataArray[index];
            var vertexCount = meshData.vertexCount;
            var matrix = TransformMatrices[index];
            var vertexStart = VertexStartIndices[index];

            // Allocate temporary arrays for input mesh vertices/normals
            // Apparently these arrays doesn't need to be disposed?
            PrepareTempArray(vertexCount, ref _tempVertices);
            PrepareTempArray(vertexCount, ref _tempNormals);

            // Read input mesh vertices/normals into temporary arrays,
            // this will do any necessary format conversions into float3 data
            meshData.GetVertices(_tempVertices.Reinterpret<Vector3>());
            meshData.GetNormals(_tempNormals.Reinterpret<Vector3>());

            var outputVertices = OutputMeshData.GetVertexData<Vector3>();
            var outputNormals = OutputMeshData.GetVertexData<Vector3>(stream: 1);

            // Transform input mesh vertices/normals, write into destination mesh, and compute transformed mesh bounds.
            var bound = Bounds[index];

            for (var i = 0; i < vertexCount; ++i)
            {
                var vertex = _tempVertices[i];
                vertex = math.mul(matrix, new float4(vertex, 1)).xyz;
                outputVertices[i + vertexStart] = vertex;
                var normal = _tempNormals[i];
                normal = math.normalize(math.mul(matrix, new float4(normal, 0)).xyz);
                outputNormals[i + vertexStart] = normal;
                bound.c0 = math.min(bound.c0, vertex);
                bound.c1 = math.max(bound.c1, vertex);
            }

            Bounds[index] = bound;

            // Write input mesh indices into destination index buffer
            var triangleStart = MeshStartIndices[index];
            var triangleCount = meshData.GetSubMesh(0).indexCount;
            var outputTriangles = OutputMeshData.GetIndexData<int>();

            if (meshData.indexFormat == IndexFormat.UInt16)
            {
                // No, you cannot Reinterpret() ushort array to int array
                var triangles = meshData.GetIndexData<ushort>();

                for (var i = 0; i < triangleCount; ++i)
                {
                    outputTriangles[i + triangleStart] = vertexStart + triangles[i];
                }
            }
            else
            {
                var triangles =  meshData.GetIndexData<int>();

                for (var i = 0; i < triangleCount; ++i)
                {
                    outputTriangles[i + triangleStart] = vertexStart + triangles[i];
                }
            }
        }

        public Bounds GetCombinedBound()
        {
            // Final bounding box of the whole mesh is union of the bounds of individual transformed meshes
            var bounds = new float3x2(new float3(Mathf.Infinity), new float3(Mathf.NegativeInfinity));
            var limit = Bounds.Length;

            for (var i = 0; i < limit; ++i)
            {
                var b = Bounds[i];
                bounds.c0 = math.min(bounds.c0, b.c0);
                bounds.c1 = math.max(bounds.c1, b.c1);
            }

            var center = (bounds.c0 + bounds.c1) * 0.5f;
            var size = bounds.c1 - bounds.c0;
            return new Bounds(center, size);
        }

        ///<remarks>This does not follow disposable pattern on purpose</remarks>
        public void DisposeResource()
        {
            MeshDataArray.Dispose();
            Bounds.Dispose();

            if (_tempNormals.IsCreated)
            {
                _tempNormals.Dispose();
            }

            if (_tempVertices.IsCreated)
            {
                _tempVertices.Dispose();
            }
        }
    }
}
#endif