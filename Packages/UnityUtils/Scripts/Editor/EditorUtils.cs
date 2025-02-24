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
        public static T FindAsset<T>() where T : Object
        {
            var guids = AssetDatabase.FindAssets($"t:{nameof(T)}");

            if (guids.Length <= 0)
            {
                return default;
            }
            
            var processorPath = AssetDatabase.GUIDToAssetPath(guids[0]);
            return AssetDatabase.LoadAssetAtPath<T>(processorPath);
        }
        
        /// <summary>
        /// Find all assets of type <c>T</c>
        /// </summary>
        /// <returns>Non-null array of type <c>T</c></returns>
        public static T[] FindAssets<T>() where T : Object
        {
            var guids = AssetDatabase.FindAssets($"t:{typeof(T).Name}");
            var result = new T[guids.Length];

            for (var i = 0; i < guids.Length; ++i)
            {
                var assetPath = AssetDatabase.GUIDToAssetPath(guids[0]);
                result[i] = AssetDatabase.LoadAssetAtPath<T>(assetPath);   
            }

            return result;
        }
    }
}