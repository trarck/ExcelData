using System;
using System.Xml;

namespace ExcelData.DataSerializer.Serializers
{
    /// <summary>
    /// Responsible to serialize <see cref="TimeSpan"/> to string and vice versa according to ISO 8601 standard.
    /// </summary>
    public class Iso8601TimeSpanSerializer : IPrimitiveSerializer
    {
        public string Serialize(object value)
        {
            var timeSpan = (TimeSpan)value;

            return XmlConvert.ToString(timeSpan);
        }

        public object Deserialize(string serializedValue)
        {
            return XmlConvert.ToTimeSpan(serializedValue);
        }
    }
}