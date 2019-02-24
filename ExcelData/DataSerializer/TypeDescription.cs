using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelData.DataSerializer
{
    public class TypeDescription
    {
        public enum DescriptionType
        {
            Primitive,
            Collection,
            Composite,
            Custom
        }
        public DescriptionType Type { get; set; }
    }
}
