using System;
using System.Collections.Generic;
using System.Reflection;

namespace ExcelData.DataSerializer
{
    public class NameProvider
    {
        List<INameProvider> nameProviders=new List<INameProvider>();
        List<int> nameProviderPriorities = new List<int>();

        public NodeName GetNodeName(Type type,object obj)
        {

            NodeName nodeName = NodeName.Empty;
            for(int i = 0, l = nameProviders.Count; i < l; ++i)
            {
                nodeName = Merge(nodeName, nameProviders[i].GetNodeName(type,obj));
            }

            return nodeName;
        }

        public NodeName GetNodeName(PropertyInfo propertyInfo,object obj)
        {

            NodeName nodeName = NodeName.Empty;
            for (int i = 0, l = nameProviders.Count; i < l; ++i)
            {
                nodeName = Merge(nodeName, nameProviders[i].GetNodeName(propertyInfo, obj));
            }
            return nodeName;
        }

        public void AddProvider(INameProvider nameProvider, int priority)
        {
            for (int i = 0, l = nameProviders.Count; i < l; ++i)
            {
                if (nameProviderPriorities[i] < priority)
                {
                    nameProviders.Insert(i, nameProvider);
                    nameProviderPriorities.Insert(i, priority);
                    break;
                }
            }
        }

        public void RemoveProvider(INameProvider nameProvider)
        {
            for (int i = 0, l = nameProviders.Count; i < l; ++i)
            {
                if (nameProviders[i] == nameProvider)
                {
                    nameProviders.RemoveAt(i);
                    nameProviderPriorities.RemoveAt(i);
                    break;
                }
            }
        }
                
        private static NodeName Merge(NodeName left, NodeName right)
        {
            var elementName = left.HasElementName ? left.ElementName : right.ElementName;
            var attributeName = left.HasAttributeName ? left.AttributeName : right.AttributeName;
            var itemName = left.HastItemName ? left.ItemName : right.ItemName;

            return new NodeName(elementName, itemName, attributeName);
        }
    }
}
