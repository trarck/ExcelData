using System;

namespace ExcelData.DataSerializer.Serializers
{
    /// <summary>
    /// Responsible to serialize specified enumeration type to string and vice versa.
    /// </summary>
    public class EnumSerializer : IPrimitiveSerializer
    {
        private readonly Type type;

        public EnumSerializer(Type type)
        {
            if (type == null) 
                throw new ArgumentNullException("type");

            this.type = type;
        }

        public string Serialize(object value)
        {
            var obj = (Enum)value;

            return obj.ToString();
        }

        public object Deserialize(string serializedValue)
        {
            return Enum.Parse(type, serializedValue, true);
        }
    }
}