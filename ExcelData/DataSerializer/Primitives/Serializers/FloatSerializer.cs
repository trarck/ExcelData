using System;

namespace ExcelData.DataSerializer.Serializers
{
    /// <summary>
    /// Responsible to serialize <see cref="float"/> to string and vice versa.
    /// </summary>
    public class FloatSerializer : IPrimitiveSerializer
    {
        private readonly IFormatProvider formatProvider;

        public FloatSerializer(IFormatProvider formatProvider)
        {
            if (formatProvider == null) 
                throw new ArgumentNullException("formatProvider");

            this.formatProvider = formatProvider;
        }

        public string Serialize(object obj)
        {
            var value = (float)obj;

            return value.ToString(formatProvider);
        }

        public object Deserialize(string serializedValue)
        {
            return float.Parse(serializedValue, formatProvider);
        }
    }
}