namespace ExcelData.DataSerializer
{
    public class NoNameNormalizer : INameNormalizer
    {
        public string NormalizeName(string name)
        {
            return name;
        }
    }
}