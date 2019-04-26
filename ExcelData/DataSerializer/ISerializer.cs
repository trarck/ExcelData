using System.IO;

namespace ExcelData.DataSerializer
{
    interface ISerializer
    {
        void Serialize(object value, Stream stream);
        object Deserialize(Stream steam);
    }
}
