namespace ExcelData.DataSerializer
{
    /// <summary>
    /// Responsible to serialize object to string and vice versa.
    /// </summary>
    public interface IPrimitiveSerializer
    {
        /// <summary>
        /// Serializes value to string.
        /// </summary>
        string Serialize(object value);

        /// <summary>
        /// Deserializes value from string.
        /// </summary>
        object Deserialize(string serializedValue);
    }
}