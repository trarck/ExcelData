using System;

namespace ExcelData.DataSerializer.Serializers
{
    /// <summary>
    /// Responsible to serialize <see cref="double"/> to string and vice versa.
    /// </summary>
    public class DoubleSerializer : IPrimitiveSerializer
    {
        private readonly IFormatProvider formatProvider;

        public DoubleSerializer(IFormatProvider formatProvider)
        {
            if (formatProvider == null) 
                throw new ArgumentNullException("formatProvider");

            this.formatProvider = formatProvider;
        }

        public string Serialize(object obj)
        {
            var value = (double)obj;

            return value.ToString(formatProvider);
        }

        public object Deserialize(string serializedValue)
        {
            return double.Parse(serializedValue, formatProvider);
        }
    }
}