using System;
using System.Collections.Generic;
using System.Reflection;

namespace ExcelData.DataSerializer
{
    public class TypeProvider:ITypeProvider
    {
        //精确的
        Dictionary<Type, ITypeProvider> explicitProviders = new Dictionary<Type, ITypeProvider>();
        //模糊的
        List<ITypeProvider> foggyProviders = new List<ITypeProvider>();
        List<int> foggyProviderPriorities = new List<int>();

        public void Register(Type type, ITypeProvider typeProvider)
        {
            explicitProviders.Add(type, typeProvider);
        }

        public void UnRegister(Type type)
        {
            if (explicitProviders.ContainsKey(type))
            {
                explicitProviders.Remove(type);
            }
        }

        public void MergeProviders(Dictionary<Type,ITypeProvider> providers)
        {
            explicitProviders.Merge(providers);
        }

        public void AddProvider(ITypeProvider typeProvider,int priority)
        {
            for (int i = 0, l = foggyProviders.Count; i < l; ++i)
            {
                if (foggyProviderPriorities[i] < priority)
                {
                    foggyProviders.Insert(i, typeProvider);
                    foggyProviderPriorities.Insert(i, priority);
                    break;
                }
            }
        }

        public void RemoveProvider(ITypeProvider typeProvider)
        {
            for (int i = 0, l = foggyProviders.Count; i < l; ++i)
            {
                if (foggyProviders[i] == typeProvider)
                {
                    foggyProviders.RemoveAt(i);
                    foggyProviderPriorities.RemoveAt(i);
                    break;
                }
            }
        }

        public bool TryGetDescription(Type type,out TypeDescription description)
        {

            ITypeProvider typeProvider;
            if (explicitProviders.TryGetValue(type, out typeProvider))
            {
                if (typeProvider.TryGetDescription(type, out description))
                {
                    return true;
                }
            }

            for (int i = 0, l = foggyProviders.Count; i < l; ++i)
            {
                if (foggyProviders[i].TryGetDescription(type, out description))
                {
                    return true;
                }
            }
            description = null;
            return false;
        }

        public bool TryGetDescription(PropertyInfo propertyInfo, out TypeDescription description)
        {
            ITypeProvider typeProvider;
            if (explicitProviders.TryGetValue(propertyInfo.PropertyType, out typeProvider))
            {
                if(typeProvider.TryGetDescription(propertyInfo,out description))
                {
                    return true;
                }
            }

            for (int i = 0, l = foggyProviders.Count; i < l; ++i)
            {
                if (foggyProviders[i].TryGetDescription(propertyInfo, out description))
                {
                    return true;
                }
            }
            description = null;
            return false;
        }
    }
}
