using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
#if ODIN_INSPECTOR
using Sirenix.Utilities.Editor;
#endif
using Object = UnityEngine.Object;

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

        /// <summary>
        /// Traverse the <see cref="SerializedProperty.propertyPath"/>
        /// to get the <see cref="FieldInfo"/> of <c>property</c>
        /// </summary>
        public static FieldInfo GetFieldInfo(this SerializedProperty property, bool searchInBaseClasses = false)
        {
            var currentType = property.serializedObject.targetObject.GetType();
            FieldInfo fieldInfo = null;

            // Traverse the SerializedProperty hierarchy
            foreach (var part in property.propertyPath.Split('.'))
            {
                // Traverse the class hierarchy
                while (currentType != null && currentType != typeof(MonoBehaviour) && currentType != typeof(Component))
                {
                    fieldInfo = currentType!.GetField(part, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

                    if (fieldInfo != null)
                    {
                        currentType = fieldInfo.FieldType;
                        break;
                    }

                    if (!searchInBaseClasses)
                    {
                        break;
                    }

                    currentType = currentType.BaseType;
                }
            }

            return fieldInfo;
        }


        /// <remarks>Hasn't been tested on arrays and lists</remarks>
        public static bool TryGetParentProperty(this SerializedProperty property, out SerializedProperty parentProperty)
        {
            var propertyPath = property.propertyPath;
            var lastIndex = propertyPath.LastIndexOf(".", StringComparison.Ordinal);

            if (lastIndex < 0)
            {
                parentProperty = null;
                return false;
            }

            parentProperty = property.serializedObject.FindProperty(propertyPath[..lastIndex]);
            return true;
        }

        public static void SetDisplay(this VisualElement element, bool isShown)
        {
            element.style.display = isShown ? DisplayStyle.Flex : DisplayStyle.None;
        }

        public static T ObjectField<T>(string label, T target, bool allowSceneObjects = true) where T : Object
        {
            return EditorGUILayout.ObjectField("Controller", target, typeof(T), allowSceneObjects) as T;
        }

#if ODIN_INSPECTOR
        public static T OdinPolymorphicObjectField<T>(string label, T target, bool allowSceneObjects = true) where T : class
        {
            return SirenixEditorFields.PolymorphicObjectField(label, target, typeof(T), allowSceneObjects) as T;
        }

        public static T OdinObjectField<T>(string label, T target, bool allowSceneObjects = true) where T : Object
        {
            return SirenixEditorFields.UnityObjectField(new GUIContent(label), target, typeof(T), allowSceneObjects) as T;
        }
#endif
    }
}