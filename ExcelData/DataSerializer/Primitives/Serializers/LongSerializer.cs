using System;

namespace ExcelData.DataSerializer.Serializers
{
    /// <summary>
    /// Responsible to serialize <see cref="long"/> to string and vice versa.
    /// </summary>
    public class LongSerializer : IPrimitiveSerializer
    {
        private readonly IFormatProvider formatProvider;

        public LongSerializer(IFormatProvider formatProvider)
        {
            if (formatProvider == null) 
                throw new ArgumentNullException("formatProvider");

            this.formatProvider = formatProvider;
        }

        public string Serialize(object obj)
        {
            var value = (long)obj;

            return value.ToString(formatProvider);
        }

        public object Deserialize(string serializedValue)
        {
            return long.Parse(serializedValue, formatProvider);
        }
    }
}