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
            var wrapper = JsonUtility.FromJson<Wrapper<T>>($"{{\"Value\":{json}}}");
            return wrapper.Value;
        }
    }
}