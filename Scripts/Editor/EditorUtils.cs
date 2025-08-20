using UnityEditor;
using UnityEngine;

namespace Vun.UnityUtils
{
    public static class EditorUtils
    {
        /// <summary>
        /// Find and return the first asset of type T 
        /// </summary>
        /// <returns>Asset of type <c>T</c> or <c>null</c> if none is found</returns>
        public static T FindAsset<T>(string query = "", string[] folders = null) where T : Object
        {
            var guids = AssetDatabase.FindAssets($"t:{typeof(T).Name} {query}", folders);
            return guids is { Length: > 0 } ? guids[0].GuidToAsset<T>() : null;
        }
        
        /// <summary>
        /// Find all assets of type <c>T</c>
        /// </summary>
        /// <returns>Non-null array of type <c>T</c></returns>
        public static T[] FindAssets<T>(string query = "", string[] folders = null) where T : Object
        {
            var guids = AssetDatabase.FindAssets($"t:{typeof(T).Name} {query}", folders);
            var assets = new T[guids.Length];

            for (var i = 0; i < guids.Length; ++i)
            {
                assets[i] = guids[i].GuidToAsset<T>();
            }

            return assets;
        }

        public static T GuidToAsset<T>(this string guid) where T : Object
        {
            var assetPath = AssetDatabase.GUIDToAssetPath(guid);
            return AssetDatabase.LoadAssetAtPath<T>(assetPath);
        }
    }
}