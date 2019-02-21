using System;

namespace ExcelData.DataSerializer.Serializers
{
    /// <summary>
    /// Responsible to serialize <see cref="sbyte"/> to string and vice versa.
    /// </summary>
    public class SbyteSerializer : IPrimitiveSerializer
    {
        private readonly IFormatProvider formatProvider;

        public SbyteSerializer(IFormatProvider formatProvider)
        {
            if (formatProvider == null) 
                throw new ArgumentNullException("formatProvider");

            this.formatProvider = formatProvider;
        }

        public string Serialize(object obj)
        {
            var value = (sbyte)obj;

            return value.ToString(formatProvider);
        }

        public object Deserialize(string serializedValue)
        {
            return sbyte.Parse(serializedValue, formatProvider);
        }
    }
}