using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Vun.UnityUtils
{
    [InitializeOnLoad]
    public static class AnimatorValueManager
    {
        public struct ControllerData
        {
            public AnimatorController Value;

            public string PropertyPath;
        }

        private static readonly Dictionary<Object, Dictionary<string, ControllerData>> ControllerCacheMap = new();

        private static bool _shouldInheritTarget;

        static AnimatorValueManager()
        {
            _shouldInheritTarget = AnimatorValueSettings.InheritTarget;
            RefreshCache();
        }

        public static void RefreshCache()
        {
            ControllerCacheMap.Clear();
        }

        public static ControllerData GetAnimatorControllerData(this SerializedProperty property)
        {
            if (_shouldInheritTarget != AnimatorValueSettings.InheritTarget)
            {
                _shouldInheritTarget = AnimatorValueSettings.InheritTarget;
                ControllerCacheMap.Clear();
            }

            if (property.TryGetControllerFromCache(out var data))
            {
                return data;
            }

            if (TryGetControllerFromHierarchy(property, out data))
            {
                CacheProperty(property, data);
                return data;
            }

            var targetObject = property.serializedObject.targetObject;

            if (targetObject is not StateMachineBehaviour)
            {
                // Fallback to the built-in animator controller field
                var controllerProperty = property.FindPropertyRelative(nameof(AnimatorValue.Controller));
                data.PropertyPath = controllerProperty.propertyPath;
                CacheProperty(property, data);
                return data;
            }

            // We can't access AnimatorController from StateMachineBehaviour,
            // but the behaviour is saved inside the controller asset, so we can exploit that
            var assetPath = AssetDatabase.GetAssetPath(targetObject);

            data = new ControllerData
            {
                Value = AssetDatabase.LoadAssetAtPath<AnimatorController>(assetPath)
            };

            CacheProperty(property, data);
            return data;
        }

        private static bool TryGetControllerFromCache(this SerializedProperty property, out ControllerData data)
        {
            data = default;

            if (property?.serializedObject == null)
            {
                Debug.LogWarning("The property or serialized object is null. This is fine if it only happen once between Editor reloads");
                return false;
            }

            var hostObject = property.serializedObject.targetObject;

            if (!ControllerCacheMap.TryGetValue(hostObject, out var controllerCache))
            {
                return false;
            }

            if (!controllerCache.TryGetValue(property.propertyPath, out var cachedData))
            {
                return false;
            }

            data = cachedData;
            return true;
        }

        // Hierarchy here refers to Inspector fields hierarchy, not Transform hierarchy
        private static bool TryGetControllerFromHierarchy(SerializedProperty property, out ControllerData data)
        {
            while (property != null)
            {
                property.TryGetParentProperty(out var parentProperty);
                property = parentProperty?.Copy() ?? property.serializedObject.GetIterator();

                if (TryGetControllerFromSiblings(property, out data))
                {
                    return true;
                }

                property = parentProperty;
            }

            data = default;
            return false;
        }

        private static bool TryGetControllerFromSiblings(SerializedProperty property, out ControllerData data)
        {
            property.NextVisible(enterChildren: true);

            do
            {
                if (!property.IsTargetControllerProperty())
                {
                    continue;
                }

                data = new ControllerData
                {
                    Value = property.GetControllerValue(),
                    PropertyPath = property.propertyPath
                };

                return true;
            }
            while (property.NextVisible(enterChildren: false));

            data = default;
            return false;
        }

        private static bool IsTargetControllerProperty(this SerializedProperty property)
        {
            var currentFieldInfo = property.GetFieldInfo(searchInBaseClasses: AnimatorValueSettings.InheritTarget);

            if (currentFieldInfo?.GetCustomAttribute<AnimatorValueTargetAttribute>() == null)
            {
                return false;
            }

            return IsAnimatorControllerField(currentFieldInfo) || currentFieldInfo.FieldType == typeof(Animator);
        }

        private static bool IsAnimatorControllerField(FieldInfo fieldInfo)
        {
            var fieldType = fieldInfo?.FieldType;
            return fieldType == typeof(AnimatorController) || fieldType == typeof(RuntimeAnimatorController);
        }

        public static AnimatorController GetControllerValue(this SerializedProperty property)
        {
            return property.objectReferenceValue.GetControllerValue();
        }

        public static AnimatorController GetControllerValue(this object obj)
        {
            var result = obj as AnimatorController;

            if (result != null)
            {
                return result;
            }

            var animator = obj as Animator;
            return animator != null ? animator.runtimeAnimatorController as AnimatorController : null;
        }

        private static void CacheProperty(SerializedProperty property, in ControllerData controllerData)
        {
            var hostObject = property.serializedObject.targetObject;

            if (!ControllerCacheMap.TryGetValue(hostObject, out var controllerCache))
            {
                controllerCache = new Dictionary<string, ControllerData>();
                ControllerCacheMap[hostObject] = controllerCache;
                return;
            }

            controllerCache[property.propertyPath] = controllerData;
        }

        public static void SetLabel(
            this GUIContent label,
            AnimatorController controller,
            AnimatorControllerParameter selectedParameter = null,
            Func<AnimatorControllerParameterType, object> valueGetter = null
        )
        {
            label.text = label.text.GetLabel(controller, selectedParameter, valueGetter);
        }

        public static string GetLabel(
            this string preferredLabel,
            AnimatorController controller,
            AnimatorControllerParameter selectedParameter = null,
            Func<AnimatorControllerParameterType, object> valueGetter = null)
        {
            if (controller == null)
            {
                return preferredLabel;
            }

            // ReSharper disable once ConvertIfStatementToReturnStatement
            if (selectedParameter == null || valueGetter == null)
            {
                return $"{preferredLabel} <b>{controller.name}</b>";
            }

            var value = valueGetter.Invoke(selectedParameter.type);
            return $"{preferredLabel}: <b>{selectedParameter.name} = {value} ({controller.name})</b>";
        }
    }
}