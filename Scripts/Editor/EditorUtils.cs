using UnityEditor;
using UnityEngine;

namespace Vun.UnityUtils
{
    public static class EditorUtils
    {
        /// <summary>
        /// Find and return the first asset of type T 
        /// </summary>
        /// <returns>Asset of type <c>T</c> or <c>default</c> if none is found</returns>
        public static T FindAsset<T>(string query = "") where T : Object
        {
            return FindAssets<T>(query)[0];
        }
        
        /// <summary>
        /// Find all assets of type <c>T</c>
        /// </summary>
        /// <returns>Non-null array of type <c>T</c></returns>
        public static T[] FindAssets<T>(string query = "") where T : Object
        {
            var guids = AssetDatabase.FindAssets($"t:{typeof(T).Name} {query}");
            var result = new T[guids.Length];

            for (var i = 0; i < guids.Length; ++i)
            {
                var assetPath = AssetDatabase.GUIDToAssetPath(guids[i]);
                result[i] = AssetDatabase.LoadAssetAtPath<T>(assetPath);
            }

            return result;
        }
    }
}