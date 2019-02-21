using System;

namespace ExcelData.DataSerializer.Serializers
{
    /// <summary>
    /// Responsible to serialize <see cref="Guid"/> to string and vice versa.
    /// </summary>
    public class GuidSerializer : IPrimitiveSerializer
    {
        private readonly string guidFormat;
        private readonly IFormatProvider formatProvider;

        public GuidSerializer(string guidFormat, IFormatProvider formatProvider)
        {
            if (guidFormat == null) 
                throw new ArgumentNullException("guidFormat");
            if (formatProvider == null) 
                throw new ArgumentNullException("formatProvider");

            this.guidFormat = guidFormat;
            this.formatProvider = formatProvider;
        }

        public string Serialize(object value)
        {
            var guid = (Guid)value;

            return guid.ToString(guidFormat, formatProvider);
        }

        public object Deserialize(string serializedValue)
        {
            return string.IsNullOrEmpty(guidFormat) ? Guid.Parse(serializedValue) : Guid.ParseExact(serializedValue, guidFormat);
        }
    }
}