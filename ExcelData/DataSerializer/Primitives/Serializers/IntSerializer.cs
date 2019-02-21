using System;

namespace ExcelData.DataSerializer.Serializers
{
    /// <summary>
    /// Responsible to serialize <see cref="int"/> to string and vice versa.
    /// </summary>
    public class IntSerializer : IPrimitiveSerializer
    {
        private readonly IFormatProvider formatProvider;

        public IntSerializer(IFormatProvider formatProvider)
        {
            if (formatProvider == null) 
                throw new ArgumentNullException("formatProvider");

            this.formatProvider = formatProvider;
        }

        public string Serialize(object obj)
        {
            var value = (int)obj;

            return value.ToString(formatProvider);
        }

        public object Deserialize(string serializedValue)
        {
            return int.Parse(serializedValue, formatProvider);
        }
    }
}