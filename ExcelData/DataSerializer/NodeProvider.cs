using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;

namespace ExcelData.DataSerializer
{
    /// <summary>
    /// Responsible to provide and cache info about nodes of object graph.
    /// </summary>
    internal class NodeProvider
    {
        private readonly SerializerSettings settings;
        private readonly IDictionary<Type, INode> nodesCacheByType = new Dictionary<Type, INode>();
        private readonly IDictionary<PropertyInfo, INode> nodesCacheByProperty = new Dictionary<PropertyInfo, INode>();

        public NodeProvider(SerializerSettings settings)
        {
            this.settings = settings;
        }

        public INode GetNode(Type type)
        {
            if (nodesCacheByType.ContainsKey(type))
            {
                return (INode)nodesCacheByType[type].Clone();
            }

            INode node;
            PrimitiveTypeDescription primitiveTypeDescription;
            CollectionTypeDescription collectionTypeDescription;
            CompositeTypeDescription compositeTypeDescription;

            if (settings.PrimitiveProvider.TryGetDescription(type, out primitiveTypeDescription))
            {
                node = new PrimitiveNode(primitiveTypeDescription);
            }
            else if (settings.CollectionProvider.TryGetDescription(type, out collectionTypeDescription))
            {
                node = new CollectionNode(collectionTypeDescription);
            }
            else if (settings.CompositeProvider.TryGetDescription(type, out compositeTypeDescription))
            {
                node = new CompositeNode(compositeTypeDescription);
            }
            else
            {
                throw new SerializationException(string.Format("Can not serialize type '{0}'", type));
            }

            nodesCacheByType[type] = node;
            return (INode)node.Clone();
        }

        public INode GetNode(PropertyInfo propertyInfo)
        {
            if (nodesCacheByProperty.ContainsKey(propertyInfo))
            {
                return (INode)nodesCacheByProperty[propertyInfo].Clone();
            }

            INode node;
            PrimitiveTypeDescription primitiveTypeDescription;
            CollectionTypeDescription collectionTypeDescription;
            CompositeTypeDescription compositeTypeDescription;

            if (settings.PrimitiveProvider.TryGetDescription(propertyInfo, out primitiveTypeDescription))
            {
                node = new PrimitiveNode(primitiveTypeDescription);
            }
            else if (settings.CollectionProvider.TryGetDescription(propertyInfo, out collectionTypeDescription))
            {
                node = new CollectionNode(collectionTypeDescription);
            }
            else if (settings.CompositeProvider.TryGetDescription(propertyInfo, out compositeTypeDescription))
            {
                node = new CompositeNode(compositeTypeDescription);
            }
            else
            {
                throw new SerializationException(string.Format("Can not serialize property '{0}'", propertyInfo));
            }

            nodesCacheByProperty[propertyInfo] = node;
            return (INode)node.Clone();
        }

        public NodeName GetNodeName(Type type, object obj=null)
        {
            return settings.NameProvider.GetNodeName(type,obj);
        }

        public NodeName GetNodeName(PropertyInfo propertyInfo, object obj=null)
        {
            return settings.NameProvider.GetNodeName(propertyInfo,obj);
        }

        public IDictionary<NodeName, PropertyInfo> GetNodeNames(IEnumerable<PropertyInfo> properties)
        {
            Dictionary<NodeName, PropertyInfo> nodeNames=new Dictionary<NodeName, PropertyInfo>();
            Dictionary<string, int> conflictedElementNames = new Dictionary<string, int>();
            Dictionary<string, int> conflictedAttributeNames = new Dictionary<string, int>();

            NodeName nodeName;
            foreach (PropertyInfo p in properties)
            {
                nodeName = GetNodeName(p);
                nodeNames.Add(nodeName, p);
                if (nodeName.HasElementName)
                {
                    if (conflictedElementNames.ContainsKey(nodeName.ElementName))
                    {
                        conflictedElementNames[nodeName.ElementName] += 1;
                    }
                    else
                    {
                        conflictedElementNames[nodeName.ElementName] = 1;
                    }
                }

                if (nodeName.HasAttributeName)
                {
                    if (conflictedAttributeNames.ContainsKey(nodeName.AttributeName))
                    {
                        conflictedAttributeNames[nodeName.AttributeName] += 1;
                    }
                    else
                    {
                        conflictedAttributeNames[nodeName.AttributeName] = 1;
                    }
                }
            }

            foreach(var iter in conflictedElementNames)
            {
                if (iter.Value>1)
                {
                    throw new SerializationException(string.Format("There are more than one property mapped to element name '{0}'", iter.Key));
                }
            }


            foreach (var iter in conflictedAttributeNames)
            {
                if (iter.Value > 1)
                {
                    throw new SerializationException(string.Format("There are more than one property mapped to element name '{0}'", iter.Key));
                }
            }
            
            return nodeNames;
        }
    }
}