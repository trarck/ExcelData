using System;
using System.Collections.Generic;

namespace ExcelData.DataSerializer
{
    internal static class TypeExtensions
    {
        public static bool ImplementsGenericInterface(this Type type, Type interfaceType)
        {
            if (type == null)
                throw new ArgumentNullException("type");
            if (interfaceType == null)
                throw new ArgumentNullException("interfaceType");

            return FindImplementedGenericInterface(type, interfaceType) != null;
        }

        public static Type FindImplementedGenericInterface(this Type type, Type interfaceType)
        {
            if (type == null)
                throw new ArgumentNullException("type");
            if (interfaceType == null)
                throw new ArgumentNullException("interfaceType");

            var interfaces = type.GetInterfaces();
            List<Type> result = new List<Type>();
            Type genericTypeDefinition = null;

            if (interfaceType.IsGenericTypeDefinition)
            {
                for(int i = 0; i < interfaces.Length; ++i)
                {
                    if (interfaces[i].IsGenericType)
                    {
                        genericTypeDefinition = interfaces[i].GetGenericTypeDefinition();
                        if (genericTypeDefinition == interfaceType)
                        {
                            return genericTypeDefinition;
                        }
                    }
                }
                return null;
            }

            for (int i = 0; i < interfaces.Length; ++i)
            {
                if (interfaces[i]== interfaceType)
                {
                    return interfaces[i];
                }
            }

            return null;
        }

        public static Type GetImplementedGenericInterface(this Type type, Type genericTypeDefinitionInterface)
        {
            var interfaces = type.GetInterfaces();
            Type genericTypeDefinition = null;
            for (int i = 0; i < interfaces.Length; ++i)
            {
                if (interfaces[i].IsGenericType)
                {
                    genericTypeDefinition = interfaces[i].GetGenericTypeDefinition();
                    if (genericTypeDefinition == genericTypeDefinitionInterface)
                    {
                        return genericTypeDefinition;
                    }
                }
            }
            return null;
        }
    }
}