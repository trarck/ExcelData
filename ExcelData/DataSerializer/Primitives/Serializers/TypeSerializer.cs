using System;

namespace ExcelData.DataSerializer.Serializers
{
    /// <summary>
    /// Responsible to serialize <see cref="Type"/> to string and vice versa.
    /// </summary>
    public class TypeSerializer : IPrimitiveSerializer
    {
        public string Serialize(object value)
        {
            var type = (Type)value;

            return type.FullName;
        }

        public object Deserialize(string serializedValue)
        {
            return Type.GetType(serializedValue);
        }
    }
}