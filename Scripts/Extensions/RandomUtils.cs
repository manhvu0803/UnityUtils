#if UNITY_MATHEMATICS_EXISTS
using Unity.Mathematics;
using UnityEngine;
using Random = Unity.Mathematics.Random;

namespace Vun.UnityUtils
{
    public static class RandomUtils
    {
        public static float3 RandomPointInTriangle(this ref Random random, in float3 p1, in float3 p2, in float3 p3)
        {
            var x = Mathf.Sqrt(random.NextFloat());
            var y = random.NextFloat();
            return (1 - x) * p1 + x * (1 - y) * p2 + x * y * p3;
        }

        public static float2 RandomPointInCircle(this ref Random random, float radius)
        {
            // https://stackoverflow.com/questions/5837572/generate-a-random-point-within-a-circle-uniformly
            var randomRadius = radius * math.sqrt(random.NextFloat());
            var angle = random.NextFloat() * 2 * math.PI;
            return randomRadius * new float2(math.cos(angle), math.sin(angle));
        }

        public static float3 RandomPointInSphere(this ref Random random, float radius)
        {
            // https://math.stackexchange.com/questions/87230/picking-random-points-in-the-volume-of-sphere-with-uniform-probability
            var randomRadius = radius * math.pow(random.NextFloat(), 1 / 3f);
            var randomX = random.NextFloat();
            var randomY = random.NextFloat();
            var randomZ = random.NextFloat();
            var magnitude = math.sqrt(randomX * randomX + randomY * randomY + randomZ * randomZ);
            return randomRadius / magnitude * new float3(randomX, randomY, randomZ);
        }
    }
}
#endif