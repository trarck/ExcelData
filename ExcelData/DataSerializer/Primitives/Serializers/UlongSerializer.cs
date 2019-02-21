using System;

namespace ExcelData.DataSerializer.Serializers
{
    /// <summary>
    /// Responsible to serialize <see cref="ulong"/> to string and vice versa.
    /// </summary>
    public class UlongSerializer : IPrimitiveSerializer
    {
        private readonly IFormatProvider formatProvider;

        public UlongSerializer(IFormatProvider formatProvider)
        {
            if (formatProvider == null) 
                throw new ArgumentNullException("formatProvider");

            this.formatProvider = formatProvider;
        }

        public string Serialize(object obj)
        {
            var value = (ulong)obj;

            return value.ToString(formatProvider);
        }

        public object Deserialize(string serializedValue)
        {
            return ulong.Parse(serializedValue, formatProvider);
        }
    }
}