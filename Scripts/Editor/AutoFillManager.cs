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
            var fields = component.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

            foreach (var field in fields)
            {
                if (Attribute.GetCustomAttribute(field, typeof(AutoFillAttribute)) is AutoFillAttribute attribute)
                {
                    ProcessField(component, field, attribute);
                }
            }
        }

        private static void ProcessField(MonoBehaviour component, FieldInfo fieldInfo, AutoFillAttribute attribute)
        {
            if (fieldInfo.FieldType.IsArray)
            {
                ProcessArrayField(component, fieldInfo, attribute);
                return;
            }

            if (GetGenericCollectionDefinition(fieldInfo.FieldType) != null)
            {
                ProcessCollection(component, fieldInfo, attribute);
                return;
            }

            if (fieldInfo.GetValue(component) as UnityEngine.Object != null)
            {
                return;
            }

            if (!fieldInfo.FieldType.IsSubclassOf(typeof(Component)))
            {
                Debug.LogError($"{nameof(AutoFillAttribute)} can't be applied to {fieldInfo.DeclaringType}.{fieldInfo.Name}");
                return;
            }

            var fieldValue = component.GetComponent(fieldInfo.FieldType, attribute.FillOption, attribute.IncludeInactive);
            fieldInfo.SetValue(component, fieldValue);
            LogSuccess(component, fieldInfo);
        }

        private static void ProcessCollection(MonoBehaviour component, FieldInfo fieldInfo, AutoFillAttribute attribute)
        {
            var fieldValue = fieldInfo.GetValue(component);

            if (fieldValue != null)
            {
                var countProperty = fieldInfo.FieldType.GetProperty("Count");
                var itemCount = (int)countProperty!.GetValue(fieldValue);

                if (itemCount > 0)
                {
                    return;
                }
            }

            var genericArgument = fieldInfo.FieldType.GetGenericArguments()[0];
            var components = component.GetComponents(genericArgument, attribute.FillOption, attribute.IncludeInactive);

            try
            {
                // ReSharper disable once CoVariantArrayConversion
                fieldValue = Activator.CreateInstance(fieldInfo.FieldType, components);
            }
            // The field doesn't have an IEnumerable constructor so we use ICollection.Add instead
            catch (MissingMethodException)
            {
                try
                {
                    fieldValue = Activator.CreateInstance(fieldInfo.FieldType);
                }
                catch (MissingMethodException)
                {
                    Debug.LogError($"{fieldInfo.FieldType.Name} ({fieldInfo.DeclaringType}.{fieldInfo.Name}) doesn't have a default constructor nor a IEnumerable constructor");
                    return;
                }

                var addMethod = fieldInfo.FieldType.GetMethod("Add");
                var input = new object[1];

                foreach (var item in components)
                {
                    input[0] = item;
                    addMethod!.Invoke(fieldValue, input);
                }
            }

            fieldInfo.SetValue(component, fieldValue);
            LogSuccess(component, fieldInfo);
        }

        private static void ProcessArrayField(MonoBehaviour component, FieldInfo fieldInfo, AutoFillAttribute attribute)
        {
            if (fieldInfo.GetValue(component) is Array { Length: > 0 })
            {
                return;
            }

            var elementType = fieldInfo.FieldType.GetElementType();

            if (!elementType?.IsSubclassOf(typeof(Component)) ?? true)
            {
                Debug.LogError($"{nameof(AutoFillAttribute)} can't be applied to {fieldInfo.DeclaringType}.{fieldInfo.Name}");
                return;
            }

            fieldInfo.SetValue(component, component.GetComponents(elementType, attribute.FillOption, attribute.IncludeInactive));
            LogSuccess(component, fieldInfo);
        }

        private static void LogSuccess(MonoBehaviour component, FieldInfo fieldInfo)
        {
            EditorUtility.SetDirty(component);
            var fieldName = fieldInfo.Name.Replace("<", "").Replace(">k__BackingField", "");
            Debug.Log($"Filled {fieldName} of {component.name}");
        }

        private static Type GetGenericCollectionDefinition(Type type)
        {
            var collectionType = typeof(ICollection<>);

            // Check all interfaces implemented by the type
            foreach (var interfaceType in type.GetInterfaces())
            {
                if (!interfaceType.IsGenericType)
                {
                    continue;
                }

                var genericDefinition = interfaceType.GetGenericTypeDefinition();

                if (collectionType.IsAssignableFrom(genericDefinition))
                {
                    return genericDefinition;
                }
            }

            return null;
        }
    }
}