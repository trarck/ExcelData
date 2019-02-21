using System;

namespace ExcelData.DataSerializer.Serializers
{
    /// <summary>
    /// Responsible to serialize <see cref="TimeSpan"/> to string and vice versa.
    /// </summary>
    public class TimeSpanSerializer : IPrimitiveSerializer
    {
        private readonly string timeSpanFormat;
        private readonly IFormatProvider formatProvider;

        public TimeSpanSerializer(string timeSpanFormat, IFormatProvider formatProvider)
        {
            if (formatProvider == null) 
                throw new ArgumentNullException("formatProvider");

            this.timeSpanFormat = timeSpanFormat;
            this.formatProvider = formatProvider;
        }

        public string Serialize(object obj)
        {
            var value = (TimeSpan)obj;

            return value.ToString(timeSpanFormat, formatProvider);
        }

        public object Deserialize(string serializedValue)
        {
            return TimeSpan.Parse(serializedValue, formatProvider);
        }
    }
}