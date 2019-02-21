using System;
using System.Globalization;

namespace ExcelData.DataSerializer.Serializers
{
    /// <summary>
    /// Responsible to serialize <see cref="DateTimeOffset"/> to string and vice versa.
    /// </summary>
    public class DateTimeOffsetSerializer : IPrimitiveSerializer
    {
        private readonly IFormatProvider formatProvider;

        public DateTimeOffsetSerializer(IFormatProvider formatProvider)
        {
            if (formatProvider == null) 
                throw new ArgumentNullException("formatProvider");

            this.formatProvider = formatProvider;
        }

        public string Serialize(object obj)
        {
            var value = (DateTimeOffset)obj;

            return value.ToUniversalTime().ToString("o", formatProvider);
        }

        public object Deserialize(string serializedValue)
        {
            return DateTimeOffset.Parse(serializedValue, formatProvider, DateTimeStyles.AssumeUniversal);
        }
    }
}