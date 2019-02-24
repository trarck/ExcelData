using System;

namespace ExcelData.DataSerializer
{
    /// <summary>
    /// Represents info how to serialize primitive type.
    /// </summary>
    public class PrimitiveTypeDescription:TypeDescription
    {
        public PrimitiveTypeDescription(IPrimitiveSerializer serializer, XmlCharacterType characterType)
        {
            if (serializer == null)
                throw new ArgumentNullException("serializer");

            Serializer = serializer;
            XmlCharacterType = characterType;
        }

        public PrimitiveTypeDescription(IPrimitiveSerializer serializer) : this(serializer, XmlCharacterType.Text)
        {
        }

        /// <summary>
        /// Gets <see cref="IPrimitiveSerializer"/> for current primitive type.
        /// </summary>
        public IPrimitiveSerializer Serializer { get; private set; }

        /// <summary>
        /// Defines type of the text data returned by <see cref="Serializer"/>.
        /// </summary>
        public XmlCharacterType XmlCharacterType { get; private set; }
    }
}