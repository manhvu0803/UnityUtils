using System;
using System.Collections.Generic;
using System.Text;
using Object = UnityEngine.Object;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Vun.UnityUtils
{
    public static class AssetFillUtils
    {
        public enum Area
        {
            All,
            Assets,
            Packages
        }

        /// <summary>
        /// Find asset of type <c>T</c>
        /// </summary>
        /// <returns>Found asset if in the Editor, <c>null</c> otherwise</returns>
        public static T FindAsset<T>(string assetName = "", string label = "", Area area = Area.Assets) where T : Object
        {
#if UNITY_EDITOR
            var filter = BuildFilter(nameof(T), assetName, label, area);
            var guids = AssetDatabase.FindAssets(filter);

            if (guids is not { Length: > 0 })
            {
                return null;
            }

            var assetPath = AssetDatabase.GUIDToAssetPath(guids[0]);
            return AssetDatabase.LoadAssetAtPath<T>(assetPath);
#else
            return null;
#endif
        }


        /// <summary>
        /// Find asset of type <c>type</c>
        /// </summary>
        /// <returns>Found asset if in the Editor, <c>null</c> otherwise</returns>
        public static Object FindAsset(Type type, string assetName = "", string label = "", Area area = Area.Assets)
        {
#if UNITY_EDITOR
            var filter = BuildFilter(type.Name, assetName, label, area);
            var guids = AssetDatabase.FindAssets(filter);

            if (guids is not { Length: > 0 })
            {
                return null;
            }

            var assetPath = AssetDatabase.GUIDToAssetPath(guids[0]);
            return AssetDatabase.LoadAssetAtPath(assetPath, type);
#else
            return null;
#endif
        }


        /// <summary>
        /// Find assets of type <c>T</c>
        /// </summary>
        /// <returns>Found asset array if in the Editor, empty array otherwise</returns>
        public static T[] FindAssets<T>(string assetName = "", string label = "", Area area = Area.Assets) where T : Object
        {
#if UNITY_EDITOR
            var filter = BuildFilter(nameof(T), assetName, label, area);
            var guids = AssetDatabase.FindAssets(filter);

            if (guids is not { Length: > 0 })
            {
                return Array.Empty<T>();
            }

            var assets = new T[guids.Length];

            for (var i = 0; i < guids.Length; i++)
            {
                var assetPath = AssetDatabase.GUIDToAssetPath(guids[i]);
                assets[i] = AssetDatabase.LoadAssetAtPath<T>(assetPath);
            }

            return assets;
#else
            return Array.Empty<T>();
#endif
        }

        private static string BuildFilter(string typeName, string assetName, string label, Area area)
        {
            var builder = new StringBuilder();

            builder.Append("t:");
            builder.Append(typeName);

            builder.Append(" a:");
            builder.Append(area.GetFilterString());

            if (!string.IsNullOrEmpty(label))
            {
                builder.Append(" l:");
                builder.Append(label);
            }

            if (!string.IsNullOrEmpty(assetName))
            {
                builder.Append(" ");
                builder.Append(label);
            }

            return builder.ToString();
        }

        public static string GetFilterString(this Area area)
        {
            return area switch
            {
                Area.All => "all",
                Area.Assets => "assets",
                Area.Packages => "packages",
                _ => ""
            };
        }

        /// <summary>
        /// If <c>assetField</c> is null, find asset of type <c>T</c> and assign to <c>assetField</c>
        /// </summary>
        public static void FillAssetField<T>(ref T assetField) where T : Object
        {
            if (assetField == null)
            {
                assetField = FindAsset<T>();
            }
        }

        /// <summary>
        /// If <c>assetField</c> is null or empty, find assets of type <c>T</c> and assign to <c>assetField</c>
        /// </summary>
        public static void FillAssetField<T>(ref T[] assetField) where T : Object
        {
            if (assetField is not { Length: > 0 })
            {
                assetField = FindAssets<T>();
            }
        }

        /// <summary>
        /// If <c>assetField</c> is null or empty, find assets <c>T</c> and add to <c>assetField</c>
        /// </summary>
        public static void FillAssetField<T>(ref List<T> assetField) where T : Object
        {
            assetField ??= new List<T>();

            if (assetField.Count > 0)
            {
                return;
            }

            assetField.AddRange(FindAssets<T>());
        }
    }
}