using System;

namespace ExcelData.DataSerializer.Serializers
{
    /// <summary>
    /// Responsible to serialize <see cref="bool"/> to string and vice versa.
    /// </summary>
    public class BoolSerializer : IPrimitiveSerializer
    {
        private readonly IFormatProvider formatProvider;

        public BoolSerializer(IFormatProvider formatProvider)
        {
            if (formatProvider == null) 
                throw new ArgumentNullException("formatProvider");

            this.formatProvider = formatProvider;
        }

        public string Serialize(object obj)
        {
            var value = (bool)obj;

            return value.ToString(formatProvider);
        }

        public object Deserialize(string serializedValue)
        {
            return bool.Parse(serializedValue);
        }
    }
}