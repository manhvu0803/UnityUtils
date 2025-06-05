#if UNITY_BURST_EXISTS
using System;
using System.Collections.Generic;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Rendering;

namespace Vun.UnityUtils
{
    /// <summary>
    /// A wrapper for <see cref="CombineMeshJob"/>.
    /// Use <see cref="CombineMeshes"/> for instant combining,
    /// or create a new instance and call <see cref="Init"/>, <see cref="ScheduleCombineJob"/> and <see cref="ProcessCombinedData"/> to control the job flow.
    /// </summary>
    /// <remarks>Adapt from https://github.com/Unity-Technologies/MeshApiExamples</remarks>
    public class CombineMeshBuilder
    {
        public const MeshUpdateFlags UPDATE_FLAGS =
            MeshUpdateFlags.DontRecalculateBounds |
            MeshUpdateFlags.DontValidateIndices |
            MeshUpdateFlags.DontNotifyMeshUsers;

        // The source use this number, and it seems reasonable
        private const int INNERLOOP_BATCH_COUNT = 4;

        public struct MeshData
        {
            public Mesh Mesh;

            public Matrix4x4 TransformMatrix;

            public static implicit operator MeshData(CombineInstance combineInstance)
            {
                return new MeshData
                {
                    Mesh = combineInstance.mesh,
                    TransformMatrix = combineInstance.transform
                };
            }
        }

        public static Mesh CombineMeshes(ICollection<MeshData> meshDataCollection)
        {
            return new CombineMeshBuilder()
                .Init(meshDataCollection)
                .ScheduleJob()
                .CompleteJob()
                .ProcessCombinedData();
        }

        private int _vertexCount;

        private int _meshIndexCount;

        private int _meshCount;

        private CombineMeshJob _job;

        private Mesh.MeshDataArray _outputMeshData;

        private JobHandle _jobHandle;

        public bool IsJobCompleted => _jobHandle.IsCompleted;

        public CombineMeshBuilder Init(ICollection<MeshData> meshDataCollection)
        {
            _job = new CombineMeshJob();
            _job.CreateInputArrays(meshDataCollection.Count);
            var inputMeshes = new List<Mesh>(meshDataCollection.Count);

            _vertexCount = 0;
            _meshIndexCount = 0;
            _meshCount = 0;

            foreach (var meshData in meshDataCollection)
            {
                inputMeshes.Add(meshData.Mesh);
                _job.VertexStartIndices[_meshCount] = _vertexCount;
                _job.MeshStartIndices[_meshCount] = _meshIndexCount;
                _job.TransformMatrices[_meshCount] = meshData.TransformMatrix;
                _vertexCount += meshData.Mesh.vertexCount;
                _meshIndexCount += (int)meshData.Mesh.GetIndexCount(0);
                _job.Bounds[_meshCount] = new float3x2(new float3(Mathf.Infinity), new float3(Mathf.NegativeInfinity));
                ++_meshCount;
            }

            _job.MeshDataArray = Mesh.AcquireReadOnlyMeshData(inputMeshes);

            _outputMeshData = Mesh.AllocateWritableMeshData(1);
            _job.OutputMeshData = _outputMeshData[0];
            _job.OutputMeshData.SetIndexBufferParams(_meshIndexCount, IndexFormat.UInt32);

            _job.OutputMeshData.SetVertexBufferParams(
                _vertexCount,
                new VertexAttributeDescriptor(VertexAttribute.Position),
                new VertexAttributeDescriptor(VertexAttribute.Normal, stream: 1)
            );

            return this;
        }

        public CombineMeshBuilder ScheduleJob()
        {
            return ScheduleJob(out _);
        }

        public CombineMeshBuilder ScheduleJob(out JobHandle jobHandle)
        {
            jobHandle = _job.Schedule(_meshCount, INNERLOOP_BATCH_COUNT);
            _jobHandle = jobHandle;
            return this;
        }

        /// <remarks>
        /// Need this (or <see cref="JobHandle.Complete"/> on <see cref="ScheduleJob(out JobHandle)"/>) to be called,
        /// just waiting for <see cref="IsJobCompleted"/> is not enough
        /// </remarks>
        public CombineMeshBuilder CompleteJob()
        {
            _jobHandle.Complete();
            return this;
        }

        /// <summary>
        /// Process mesh data from job and dispose resources.
        /// Can only be call after the job has been completed.
        /// </summary>
        public Mesh ProcessCombinedData()
        {
#if DEBUG
            if (!_jobHandle.IsCompleted)
            {
                throw new Exception("Combine meshes job hasn't completed before processing data");
            }
#endif

            var subMeshDescriptor = new SubMeshDescriptor(0, _meshIndexCount)
            {
                firstVertex = 0,
                vertexCount = _vertexCount
            };

            var combinedMesh = new Mesh();
            subMeshDescriptor.bounds = _job.GetCombinedBound();
            _job.OutputMeshData.subMeshCount = 1;
            _job.OutputMeshData.SetSubMesh(0, subMeshDescriptor, UPDATE_FLAGS);
            Mesh.ApplyAndDisposeWritableMeshData(_outputMeshData, combinedMesh , UPDATE_FLAGS);
            combinedMesh.bounds = subMeshDescriptor.bounds;
            _job.DisposeResource();
            return combinedMesh;
        }
    }
}
#endif