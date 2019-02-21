using System;

namespace ExcelData.DataSerializer.Serializers
{
    /// <summary>
    /// Responsible to serialize <see cref="Uri"/> to string and vice versa.
    /// </summary>
    public class UriSerializer : IPrimitiveSerializer
    {
        public string Serialize(object value)
        {
            var uri = (Uri)value;

            return uri.AbsoluteUri;
        }

        public object Deserialize(string serializedValue)
        {
            return new Uri(serializedValue, UriKind.RelativeOrAbsolute);
        }
    }
}