using System;

namespace ExcelData.DataSerializer.Serializers
{
    /// <summary>
    /// Responsible to serialize <see cref="short"/> to string and vice versa.
    /// </summary>
    public class ShortSerializer : IPrimitiveSerializer
    {
        private readonly IFormatProvider formatProvider;

        public ShortSerializer(IFormatProvider formatProvider)
        {
            if (formatProvider == null) 
                throw new ArgumentNullException("formatProvider");

            this.formatProvider = formatProvider;
        }

        public string Serialize(object obj)
        {
            var value = (short)obj;

            return value.ToString(formatProvider);
        }

        public object Deserialize(string serializedValue)
        {
            return short.Parse(serializedValue, formatProvider);
        }
    }
}