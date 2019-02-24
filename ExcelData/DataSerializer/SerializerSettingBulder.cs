using System;
using System.Collections.Generic;
using System.Globalization;

namespace ExcelData.DataSerializer
{
    using Serializers;


    public class SerializerSettingBulder
    {
        private IFormatProvider formatProvider = CultureInfo.InvariantCulture;

        /// <summary>
        /// Specifies to use specified <see cref="IFormatProvider"/>.
        /// </summary>
        public SerializerSettingBulder SetFormatProvider(IFormatProvider provider)
        {
            if (provider == null)
                throw new ArgumentNullException("provider");

            formatProvider = provider;
            return this;
        }

        public ITypeProvider BuildTypeProvider()
        {
            return null;
        }

        public INameProvider BuildNameProvider()
        {
            return null;
        }

        private IDictionary<Type, IPrimitiveSerializer> GetDefaultPrimitiveSerializers()
        {
            return new Dictionary<Type, IPrimitiveSerializer>
                {
                    { typeof(char), new CharSerializer() },
                    { typeof(string), new StringSerializer() },
                    { typeof(short), new ShortSerializer(formatProvider) },
                    { typeof(ushort), new UshortSerializer(formatProvider) },
                    { typeof(byte), new ByteSerializer(formatProvider) },
                    { typeof(sbyte), new SbyteSerializer(formatProvider) },
                    { typeof(int), new IntSerializer(formatProvider) },
                    { typeof(uint), new UintSerializer(formatProvider) },
                    { typeof(long), new LongSerializer(formatProvider) },
                    { typeof(ulong), new UlongSerializer(formatProvider) },
                    { typeof(float), new FloatSerializer(formatProvider) },
                    { typeof(double), new DoubleSerializer(formatProvider) },
                    { typeof(decimal), new DecimalSerializer(formatProvider) },
                    { typeof(bool), new BoolSerializer(formatProvider) },
                    { typeof(TimeSpan), new TimeSpanSerializer(string.Empty, formatProvider) },
                    { typeof(DateTime), new DateTimeSerializer(formatProvider) },
                    { typeof(DateTimeOffset), new DateTimeOffsetSerializer(formatProvider) },
                    { typeof(Uri), new UriSerializer() },
                    { typeof(Guid), new GuidSerializer(string.Empty, formatProvider) },
                    { typeof(Type), new TypeSerializer() }
                };
        }
    }
}
