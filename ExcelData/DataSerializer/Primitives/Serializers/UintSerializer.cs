using System;

namespace ExcelData.DataSerializer.Serializers
{
    /// <summary>
    /// Responsible to serialize <see cref="uint"/> to string and vice versa.
    /// </summary>
    public class UintSerializer : IPrimitiveSerializer
    {
        private readonly IFormatProvider formatProvider;

        public UintSerializer(IFormatProvider formatProvider)
        {
            if (formatProvider == null) 
                throw new ArgumentNullException("formatProvider");

            this.formatProvider = formatProvider;
        }

        public string Serialize(object obj)
        {
            var value = (uint)obj;

            return value.ToString(formatProvider);
        }

        public object Deserialize(string serializedValue)
        {
            return uint.Parse(serializedValue, formatProvider);
        }
    }
}