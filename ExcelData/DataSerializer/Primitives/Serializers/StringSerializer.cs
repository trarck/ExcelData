namespace ExcelData.DataSerializer.Serializers
{
    /// <summary>
    /// Responsible to serialize <see cref="string"/> to string and vice versa.
    /// </summary>
    public class StringSerializer : IPrimitiveSerializer
    {
        public string Serialize(object obj)
        {
            return (string)obj;
        }

        public object Deserialize(string serializedValue)
        {
            return serializedValue;
        }
    }
}