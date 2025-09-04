using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Vun.UnityUtils
{
    [InitializeOnLoad]
    public static class AutoFillManager
    {
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
            var property = serializedComponent.GetIterator();
            var lastProperty = property;
            var hierarchy = new Stack<SerializedProperty>();

            while (property.NextVisible(enterChildren: true))
            {
                if (lastProperty.depth < property.depth)
                {
                    hierarchy.Push(lastProperty);
                }
                else
                {
                    PopToDepth(hierarchy, property.depth);
                }

                switch (property.propertyType)
                {
                    case SerializedPropertyType.Generic when property.isArray:
                    {
                        var parent = hierarchy.Count <= 0 ? component : hierarchy.Peek().managedReferenceValue;
                        ProcessArrayProperty(component, parent, property);
                        break;
                    }
                    case SerializedPropertyType.ObjectReference:
                    {
                        var parent = hierarchy.Count <= 0 ? component : hierarchy.Peek().managedReferenceValue;
                        ProcessProperty(component, parent, property);
                        break;
                    }
                }

                lastProperty = property;
            }

            serializedComponent.ApplyModifiedProperties();
        }

        private static void PopToDepth(Stack<SerializedProperty> hierarchy, int depth)
        {
            while (hierarchy.TryPeek(out var parentProperty) && parentProperty.depth > depth)
            {
                hierarchy.Pop();
            }
        }

        private static void ProcessArrayProperty(MonoBehaviour component, object parentProperty, SerializedProperty property)
        {
            if (property.arraySize > 0)
            {
                return;
            }

            var fieldInfo = parentProperty.GetType().GetField(property.name);

            if (!fieldInfo.FieldType.GetElementType()!.IsSubclassOf(typeof(Component)))
            {
                Debug.LogError($"{nameof(AutoFillAttribute)} can't be applied to {fieldInfo.DeclaringType}.{fieldInfo.Name}");
                return;
            }

            if (!fieldInfo.TryGetAttribute(out AutoFillAttribute autoFill))
            {
                return;
            }

            var type = Type.GetType(property.arrayElementType);
            var components = component.GetComponents(type, autoFill.FillOption, autoFill.IncludeInactive);
            
        }

        private static void ProcessProperty(MonoBehaviour component, object parentProperty, SerializedProperty property)
        {
            if (property.objectReferenceValue != null)
            {
                return;
            }

            var fieldInfo = parentProperty.GetType().GetField(property.name);

            if (!fieldInfo.FieldType.IsSubclassOf(typeof(Component)))
            {
                Debug.LogError($"{nameof(AutoFillAttribute)} can't be applied to {fieldInfo.DeclaringType}.{fieldInfo.Name}");
                return;
            }

            if (!fieldInfo.TryGetAttribute(out AutoFillAttribute autoFill))
            {
                return;
            }

            property.objectReferenceValue = component.GetComponent(fieldInfo.FieldType, autoFill.FillOption, autoFill.IncludeInactive);
            Finish(component, fieldInfo);
        }

        private static void Finish(MonoBehaviour component, FieldInfo fieldInfo)
        {
            EditorUtility.SetDirty(component);
            var fieldName = fieldInfo.Name.Replace("<", "").Replace(">k__BackingField", "");
            Debug.Log($"Filled {fieldName} of {component.name}");
        }
    }
}