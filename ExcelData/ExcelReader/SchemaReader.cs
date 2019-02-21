using System.Collections.Generic;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;
using NPOI.XSSF.UserModel;

namespace TK.ExcelData
{
    public class SchemaReader
    {
        public static Schema ReadSchema(ISheet sheet, int schemaNameRow= EDConstance.SchemaNameRow, int schemaDescriptionRow = EDConstance.SchemaDescriptionRow, int schemaDataTypeRow=EDConstance.SchemaDataTypeRow,int schemaColOffset=0)
        {
            Schema schema = new Schema();
            schema.name = sheet.SheetName;

            //first row is name
            IRow headerRow = sheet.GetRow(sheet.FirstRowNum + schemaNameRow);

            //second row is description
            IRow descriptionRow = sheet.GetRow(sheet.FirstRowNum + schemaDescriptionRow);

            //third row is data type
            IRow typeRow = sheet.GetRow(sheet.FirstRowNum + schemaDataTypeRow);

            for(int i = headerRow.FirstCellNum+schemaColOffset; i < headerRow.LastCellNum; ++i)
            {
                ICell headCell = headerRow.GetCell(i);
                string name = headCell.StringCellValue;

                ICell descriptionCell = descriptionRow.GetCell(i);
                string description = descriptionCell.StringCellValue;

                ExcelDataType dataType = ExcelDataType.Object;
                string extType = "";

                ICell typeCell = typeRow.GetCell(i);
                if (typeCell!=null)
                {
                    dataType = Field.Parse(typeCell.StringCellValue, out extType);
                }

                string comment = "";
                if (headCell.CellComment != null)
                {
                    comment = headCell.CellComment.String.String;
                }

                Field field = new Field(name, dataType, extType, comment,description);
                schema.AddField(field);
            }

            return schema;
        }
    }
}