using System;
using System.Globalization;

namespace ExcelData.DataSerializer.Serializers
{
    /// <summary>
    /// Responsible to serialize <see cref="DateTime"/> to string and vice versa.
    /// </summary>
    public class DateTimeSerializer : IPrimitiveSerializer
    {
        private readonly IFormatProvider formatProvider;

        public DateTimeSerializer(IFormatProvider formatProvider)
        {
            if (formatProvider == null) 
                throw new ArgumentNullException("formatProvider");

            this.formatProvider = formatProvider;
        }

        public string Serialize(object obj)
        {
            var value = (DateTime)obj;

            return value.ToUniversalTime().ToString("o", formatProvider);
        }

        public object Deserialize(string serializedValue)
        {
            return DateTime.Parse(serializedValue, formatProvider, DateTimeStyles.AdjustToUniversal);
        }
    }
}