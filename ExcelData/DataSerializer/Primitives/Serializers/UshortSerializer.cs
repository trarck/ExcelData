using System;

namespace ExcelData.DataSerializer.Serializers
{
    /// <summary>
    /// Responsible to serialize <see cref="ushort"/> to string and vice versa.
    /// </summary>
    public class UshortSerializer : IPrimitiveSerializer
    {
        private readonly IFormatProvider formatProvider;

        public UshortSerializer(IFormatProvider formatProvider)
        {
            if (formatProvider == null) 
                throw new ArgumentNullException("formatProvider");

            this.formatProvider = formatProvider;
        }

        public string Serialize(object obj)
        {
            var value = (ushort)obj;

            return value.ToString(formatProvider);
        }

        public object Deserialize(string serializedValue)
        {
            return ushort.Parse(serializedValue, formatProvider);
        }
    }
}