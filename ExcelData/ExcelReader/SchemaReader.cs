using System.Collections.Generic;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;
using NPOI.XSSF.UserModel;

namespace TK.ExcelData
{
    public class SchemaReader
    {
        public static Schema ReadSchema(ISheet sheet, 
                                                                int schemaNameRow= Constance.SchemaNameRow, 
                                                                int schemaDataTypeRow = Constance.SchemaDataTypeRow,
                                                                int schemaDescriptionRow = Constance.SchemaDescriptionRow,
                                                                int schemaColOffset=0)
        {
            Schema schema = new Schema();
            schema.name = sheet.SheetName;

            //first row is name
            IRow headerRow = sheet.GetRow(sheet.FirstRowNum + schemaNameRow);
            //third row is data type
            IRow typeRow = sheet.GetRow(sheet.FirstRowNum + schemaDataTypeRow);
            //second row is description
            IRow descriptionRow = sheet.GetRow(sheet.FirstRowNum + schemaDescriptionRow);

            for(int i = headerRow.FirstCellNum+schemaColOffset; i < headerRow.LastCellNum; ++i)
            {
                ICell headCell = headerRow.GetCell(i);
                string name = headCell.StringCellValue;

                ICell descriptionCell = descriptionRow.GetCell(i);
                string description = descriptionCell.StringCellValue;

                TypeInfo dataType = TypeInfo.Object;

                ICell typeCell = typeRow.GetCell(i);
                if (typeCell!=null)
                {
                    dataType = TypeInfo.Parse(typeCell.StringCellValue);
                }

                string comment = "";
                if (headCell.CellComment != null)
                {
                    comment = headCell.CellComment.String.String;
                }

                Field field = new Field(name, dataType, comment,description);
                schema.AddField(field);
            }

            return schema;
        }
    }
}