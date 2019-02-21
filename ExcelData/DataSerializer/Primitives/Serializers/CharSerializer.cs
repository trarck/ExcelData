namespace ExcelData.DataSerializer.Serializers
{
    /// <summary>
    /// Responsible to serialize <see cref="char"/> to string and vice versa.
    /// </summary>
    public class CharSerializer : IPrimitiveSerializer
    {
        public string Serialize(object obj)
        {
            var c = (char)obj;
            return new string(new [] { c });
        }

        public object Deserialize(string serializedValue)
        {
            return serializedValue[0];
        }
    }
}