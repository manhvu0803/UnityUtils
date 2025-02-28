using System;
using UnityEngine;

namespace Vun.UnityUtils
{
    public static class Utils
    {
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
        /// Extension method version of <see cref="Enum.GetValues"/>
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
        /// A non-boxing version of <see cref="Enum.HasFlag"/>
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
    }
}