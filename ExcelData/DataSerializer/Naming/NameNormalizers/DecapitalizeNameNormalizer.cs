namespace ExcelData.DataSerializer
{
    public class DecapitalizeNameNormalizer : INameNormalizer
    {
        public string NormalizeName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return name;
            }

            var firstLetter = name.Substring(0, 1);
            
            return name.Remove(0, 1).Insert(0, firstLetter.ToLowerInvariant());
        }
    }
}