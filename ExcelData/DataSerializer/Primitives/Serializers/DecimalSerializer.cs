using System;

namespace ExcelData.DataSerializer.Serializers
{
    /// <summary>
    /// Responsible to serialize <see cref="decimal"/> to string and vice versa.
    /// </summary>
    public class DecimalSerializer : IPrimitiveSerializer
    {
        private readonly IFormatProvider formatProvider;

        public DecimalSerializer(IFormatProvider formatProvider)
        {
            if (formatProvider == null) 
                throw new ArgumentNullException("formatProvider");

            this.formatProvider = formatProvider;
        }

        public string Serialize(object obj)
        {
            var value = (decimal)obj;

            return value.ToString(formatProvider);
        }

        public object Deserialize(string serializedValue)
        {
            return decimal.Parse(serializedValue, formatProvider);
        }
    }
}