using System;
using UnityEngine;

namespace Vun.UnityUtils
{
    public static class Utils
    {
        private static readonly Vector2Int[] Offset2dArray =
        {
            new(0, 1),
            new(1, 1),
            new(1, 0),
            new(1, -1),
            new(0, -1),
            new(-1, -1),
            new(-1, 0),
            new(-1, 1),
        };

        /// <summary>
        /// Read-only array of 2D coordinate offsets starting from the top (0, 1) and goes clockwise.
        /// Useful for checking adjacent coordinates on a grid
        /// </summary>
        public static ReadOnlyMemory<Vector2Int> Offsets2d => Offset2dArray;

        private struct Wrapper<T>
        {
            public T[] Value;
        }

        /// <summary>
        /// Parse an JSON-style array string to an <see cref="Array"/> using Unity <see cref="JsonUtility"/>
        /// </summary>
        /// <param name="json">JSON-style array string ("[1, 2, 3, 4]", .etc)</param>
        public static T[] ArrayFromJson<T>(string json)
        {
            json = $"{{\"Value\":{json}}}";
            var wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
            return wrapper.Value;
        }

        /// <summary>
        /// Get all values of enum <c>T</c>.
        /// </summary>
        /// <returns>Array of values of <c>T</c>. Never <c>null</c></returns>
        public static T[] GetValues<T>() where T : Enum
        {
            if (Enum.GetValues(typeof(T)) is T[] values)
            {
                return values;
            }

            return Array.Empty<T>();
        }

        /// <summary>
        /// A non-boxing but unsafe version of <see cref="Enum.HasFlag"/>.
        /// Support any enum with numeric backing type from <see cref="byte"/> to <see cref="long"/> (<c>long</c>),
        /// but will fail for larger type.
        /// Useful for Burst-compiled code
        /// </summary>
        public static unsafe bool ContainsFlag<T>(this T container, T flag) where T : unmanaged, Enum
        {
            switch (sizeof(T))
            {
                case 1:
                    var byteFlag = *(byte*)&flag;
                    return (*(byte*)&container & byteFlag) == byteFlag;
                case 2:
                    var shortFlag = *(ushort*)&flag;
                    return (*(ushort*)&container & shortFlag) == shortFlag;
                case 4:
                    var intFlag = *(uint*)&flag;
                    return (*(uint*)&container & intFlag) == intFlag;
                case 8:
                    var longFlag = *(ulong*)&flag;
                    return (*(ulong*)&container & longFlag) == longFlag;
                default:
                    throw new ArgumentException("Invalid enum backing type");
            }
        }

        public static Vector3 RandomPointInTriangle(in Vector3 p1, in Vector3 p2, in Vector3 p3)
        {
            var x = Mathf.Sqrt(UnityEngine.Random.Range(0, 1f));
            var y = UnityEngine.Random.Range(0, 1f);
            return (1 - x) * p1 + x * (1 - y) * p2 + x * y * p3;
        }
    }
}