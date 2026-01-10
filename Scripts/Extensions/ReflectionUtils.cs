using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Vun.UnityUtils
{
    public static class ReflectionUtils
    {
        public static bool IsDerivedFromGeneric(this Type type, Type genericType, out Type genericParam, out int depth)
        {
            depth = 0;

            while (type != null)
            {
                if (type.IsGenericType(genericType))
                {
                    genericParam = type.GetGenericArguments()[0];
                    return true;
                }

                type = type.BaseType;
                depth++;
            }

            genericParam = null;
            return false;
        }

        public static bool IsGenericType(this Type type, Type genericType)
        {
            return type.IsGenericType && type.GetGenericTypeDefinition() == genericType;
        }

        public static string GetNonGenericName(this Type type)
        {
            return type.IsGenericType ? type.Name[..type.Name.IndexOf('`')] : type.Name;
        }

        public static string GetGenericSyntaxName(this Type type)
        {
            var genericArguments = type.GetGenericArguments();

            if (genericArguments.Length <= 0)
            {
                return type.GetNonGenericName();
            }

            var genericNames = genericArguments.Select(GetGenericSyntaxName);
            return $"{type.GetNonGenericName()}<{string.Join(", ", genericNames)}>";
        }

        public static bool IsDerivedGenericInterface(this Type type, Type genericInterface, out Type genericParam, out int depth)
        {
            genericParam = null;
            depth = 0;

            var interfaceTypes = type.GetInterfaces();
            Type targetInterfaceType = null;

            foreach (var interfaceType in interfaceTypes)
            {
                if (interfaceType.IsGenericType(genericInterface))
                {
                    targetInterfaceType = interfaceType;
                    break;
                }
            }

            if (targetInterfaceType == null)
            {
                return false;
            }

            genericParam = targetInterfaceType.GetGenericArguments()[0];

            while (type != null && type != typeof(object))
            {
                type = type.BaseType;
                depth++;
            }

            return true;
        }

        public static bool HasGenericInterface(this Type type, Type genericInterface)
        {
            foreach (var interfaceType in type.GetInterfaces())
            {
                if (interfaceType.IsGenericType(genericInterface))
                {
                    return true;
                }
            }

            return false;
        }

        public static bool TryGetAttribute<T>(this FieldInfo fieldInfo, out T attribute) where T : Attribute
        {
            var customAttribute = fieldInfo.GetCustomAttribute(typeof(T), false);
            attribute = customAttribute as T;
            return attribute != null;
        }

        public static string GetNonBackingFieldName(this FieldInfo fieldInfo)
        {
            var indexOfSign = fieldInfo.Name.IndexOf('>');
            return indexOfSign > 0 ? fieldInfo.Name[1..indexOfSign] : fieldInfo.Name;
        }

        /// <remarks>This doesn't traverse the class hierarchy nor check the interfaces</remarks>
        public static bool IsMethodInBaseType(
            this Type type,
            string methodName,
            BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance,
            params Type[] parameterTypes)
        {
            if (type.BaseType == null)
            {
                return false;
            }

            IEnumerable<MethodInfo> methodInfos = type.BaseType.GetMethods(bindingFlags);

            if (parameterTypes is not { Length: > 0 })
            {
                return methodInfos.Any(method => method.Name == methodName);
            }

            return methodInfos
                .Where(method => method.Name == methodName)
                .Any(methodInfo => methodInfo.HasParameter(parameterTypes));
        }

        public static bool HasParameter(this MethodInfo methodInfo, params Type[] parameterTypes)
        {
            var parameters = methodInfo.GetParameters();

            if (parameters.Length != parameterTypes.Length)
            {
                return false;
            }

            for (var i = 0; i < parameters.Length; i++)
            {
                if (parameters[i].ParameterType == parameterTypes[i])
                {
                    return false;
                }
            }

            return true;
        }
    }
}