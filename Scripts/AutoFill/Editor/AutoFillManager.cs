#if VUN_ENABLE_AUTO_FILL
using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Vun.UnityUtils.AutoFill.Editor
{
    [InitializeOnLoad]
    public static class AutoFillManager
    {
        private const BindingFlags FieldBindingFlags = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public;

        private static bool _isProcessing;

        static AutoFillManager()
        {
            ObjectChangeEvents.changesPublished += OnObjectChange;
            Debug.Log($"{nameof(AutoFillManager)} subscribed to object change events");
        }

        private static void OnObjectChange(ref ObjectChangeEventStream stream)
        {
            // Since this function is called on object changes and might propagate change itself,
            // this will prevent infinite loops from happening
            if (_isProcessing)
            {
                return;
            }

            _isProcessing = true;

            for (var i = 0; i < stream.length; i++)
            {
                try
                {
                    var eventType = stream.GetEventType(i);

                    switch (eventType)
                    {
                        case ObjectChangeKind.ChangeGameObjectStructure:
                            stream.GetChangeGameObjectStructureEvent(i, out var structureEventArgs);
                            ProcessInstanceId(structureEventArgs.instanceId);
                            break;
                        case ObjectChangeKind.ChangeGameObjectOrComponentProperties:
                            stream.GetChangeGameObjectOrComponentPropertiesEvent(i, out var propertiesEventArgs);
                            ProcessInstanceId(propertiesEventArgs.instanceId);
                            break;
                    }
                }
                catch (Exception exception)
                {
                    Debug.LogException(exception);
                }
            }

            _isProcessing = false;
        }

        private static void ProcessInstanceId(int instanceId)
        {
            var unityObject = EditorUtility.InstanceIDToObject(instanceId);

            switch (unityObject)
            {
                case GameObject gameObject:
                    ProcessGameObject(gameObject);
                    break;
                case MonoBehaviour monoBehaviour:
                    ProcessComponent(monoBehaviour);
                    break;
            }
        }

        private static void ProcessGameObject(GameObject gameObject)
        {
            var components = gameObject.GetComponents<MonoBehaviour>();

            foreach (var component in components)
            {
                ProcessComponent(component);
            }
        }

        private static void ProcessComponent(MonoBehaviour component)
        {
            var serializedComponent = new SerializedObject(component);
            using var property = serializedComponent.GetIterator();
            var hierarchy = new Stack<object>();
            hierarchy.Push(component);

            while (property.NextVisible(enterChildren: !property.isArray))
            {
                // Base MonoBehaviour has depth of -1, so hierarchy depth is Count - 2
                while (hierarchy.Count - 1 > property.depth)
                {
                    hierarchy.Pop();
                }

                var parent = hierarchy.Peek();

                // ReSharper disable once SwitchStatementMissingSomeEnumCasesNoDefault
                switch (property.propertyType)
                {
                    case SerializedPropertyType.Generic when property.isArray:
                    {
                        ProcessArrayProperty(component, parent, property);
                        break;
                    }
                    case SerializedPropertyType.ObjectReference:
                    {
                        ProcessProperty(component, parent, property);
                        break;
                    }
                }

                var fieldInfo = parent.GetType().GetField(property.name, FieldBindingFlags);

                if (fieldInfo != null)
                {
                    hierarchy.Push(fieldInfo.GetValue(parent));
                }
            }

            serializedComponent.ApplyModifiedProperties();
        }

        private static void ProcessArrayProperty(MonoBehaviour component, object parent, SerializedProperty property)
        {
            if (property.arraySize > 0)
            {
                return;
            }

            var fieldInfo = parent.GetType().GetField(property.name, FieldBindingFlags);

            if (fieldInfo == null)
            {
                Debug.LogError($"Can't find {property.name} in {parent.GetType()}");
                return;
            }

            if (!fieldInfo.TryGetElementType(out var elementType) || !elementType.IsSubclassOf(typeof(Component)))
            {
                Debug.LogError($"{nameof(AutoFillAttribute)} can't be applied to {fieldInfo.DeclaringType}.{fieldInfo.Name}");
                return;
            }

            if (!fieldInfo.TryGetAttribute(out AutoFillAttribute autoFill))
            {
                return;
            }

            var components = component.GetComponents(elementType, autoFill.FillOption, autoFill.IncludeInactive);
            property.arraySize = components.Length;

            for (var i = 0; i < components.Length; ++i)
            {
                var elementProperty = property.GetArrayElementAtIndex(i);
                elementProperty.objectReferenceValue = components[i];
            }

            Finish(component, property, components);
        }

        private static void ProcessProperty(MonoBehaviour component, object parent, SerializedProperty property)
        {
            if (property.objectReferenceValue != null)
            {
                return;
            }

            var fieldInfo = parent.GetType().GetField(property.name, FieldBindingFlags);

            if (!fieldInfo!.FieldType.IsSubclassOf(typeof(Component)))
            {
                Debug.LogError($"{nameof(AutoFillAttribute)} can't be applied to {fieldInfo.DeclaringType}.{fieldInfo.Name}");
                return;
            }

            if (!fieldInfo.TryGetAttribute(out AutoFillAttribute autoFill))
            {
                return;
            }

            property.objectReferenceValue = component.GetComponent(fieldInfo.FieldType, autoFill.FillOption, autoFill.IncludeInactive);
            Finish(component, property, property.objectReferenceValue);
        }

        private static void Finish(MonoBehaviour component, SerializedProperty property, UnityEngine.Object fieldValue)
        {
            if (fieldValue != null)
            {
                Finish(component, property, fieldValue.ToString());
            }
        }

        private static void Finish(MonoBehaviour component, SerializedProperty property, Array array)
        {
            if (array is { Length: > 0 })
            {
                Finish(component, property, $"{array.Length} item(s)");
            }
        }

        private static void Finish(MonoBehaviour component, SerializedProperty property, string valueString)
        {
            EditorUtility.SetDirty(component);
            var path = property.propertyPath.Replace(">k__BackingField", "").Replace("<", "");
            Debug.Log($"<color=green>Filled {component.gameObject.name}.{component.GetType().Name}.{path} with {valueString}</color>");
        }
    }
}
#endif