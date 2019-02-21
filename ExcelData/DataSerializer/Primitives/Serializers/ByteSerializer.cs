using System;

namespace ExcelData.DataSerializer.Serializers
{
    /// <summary>
    /// Responsible to serialize <see cref="byte"/> to string and vice versa.
    /// </summary>
    public class ByteSerializer : IPrimitiveSerializer
    {
        private readonly IFormatProvider formatProvider;

        public ByteSerializer(IFormatProvider formatProvider)
        {
            if (formatProvider == null) 
                throw new ArgumentNullException("formatProvider");

            this.formatProvider = formatProvider;
        }

        public string Serialize(object obj)
        {
            var value = (byte)obj;

            return value.ToString(formatProvider);
        }

        public object Deserialize(string serializedValue)
        {
            return byte.Parse(serializedValue, formatProvider);
        }
    }
}