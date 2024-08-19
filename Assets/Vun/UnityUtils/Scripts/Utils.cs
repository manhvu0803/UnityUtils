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
        /// Parse an JSON-style array string to a real array using Unity's JsonUtility
        /// </summary>
        /// <param name="json">JSON-style array string ("[1, 2, 3, 4]", .etc)</param>
        public static T[] ArrayFromJson<T>(string json)
        {
            json = $"{{\"Value\":{json}}}";
            var wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
            return wrapper.Value;
        }

        /// <summary>
        /// Get all values of enum <c>T</c>
        /// </summary>
        /// <returns>Array of values of <c>T</c>. Never null</returns>
        public static T[] GetAllValues<T>() where T : Enum
        {
            if (Enum.GetValues(typeof(T)) is T[] values)
            {
                return values;
            }

            return Array.Empty<T>();
        }
    }
}