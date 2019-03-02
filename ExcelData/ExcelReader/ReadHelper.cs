using System.Collections.Generic;
using NPOI.SS.UserModel;

namespace TK.ExcelData
{
    public class ReadHelper
    {
        public static List<string> GetHeader(ISheet sheet, int headerOffset = 0,int colOffset=0)
        {
            List<string> header = new List<string>();

            //first row is header as default
            IRow headerRow = sheet.GetRow(sheet.FirstRowNum + headerOffset);
            for(int i=headerRow.FirstCellNum+colOffset;i<headerRow.LastCellNum;++i)
            {
                header.Add(headerRow.GetCell(i).StringCellValue);
            }
            return header;
        }

        public static List<Field> PrepareHeaderFields(List<string> header, Schema schema)
        {
            List<Field> headerFields = new List<Field>();
            foreach (string name in header)
            {
                headerFields.Add(schema.GetField(name));
            }

            return headerFields;
        }

        public static object GetCellValue(ICell cell, TypeInfo dataType)
        {
            switch (dataType.sign)
            {
                case TypeInfo.Sign.Int:
                    return ReadHelper.GetIntValue(cell);
                case TypeInfo.Sign.Float:
                    return ReadHelper.GetFloatValue(cell);
                case TypeInfo.Sign.Long:
                    return ReadHelper.GetLongValue(cell);
                case TypeInfo.Sign.Double:
                    return ReadHelper.GetDoubleValue(cell);
                case TypeInfo.Sign.Boolean:
                    return ReadHelper.GetBoolValue(cell);
                case TypeInfo.Sign.String:
                    return ReadHelper.GetStringValue(cell);
                case TypeInfo.Sign.Array:
                default:
                    break;
            }
            return null;
        }

        public static int GetIntValue(ICell cell)
        {
            switch (cell.CellType)
            {
                case CellType.Numeric:
                    return (int)cell.NumericCellValue;
                case CellType.String:
                    return int.Parse(cell.StringCellValue);
                case CellType.Boolean:
                    return cell.BooleanCellValue ? 1 : 0;
                default:
                    throw new System.Exception("can't convert to int from " + cell.CellType);
            }
        }

        public static long GetLongValue(ICell cell)
        {
            switch (cell.CellType)
            {
                case CellType.Numeric:
                    return (long)cell.NumericCellValue;
                case CellType.String:
                    return long.Parse(cell.StringCellValue);
                default:
                    throw new System.Exception("can't convert to long from " + cell.CellType);
            }
        }

        public static float GetFloatValue(ICell cell)
        {
            switch (cell.CellType)
            {
                case CellType.Numeric:
                    return (float)cell.NumericCellValue;
                case CellType.String:
                    return float.Parse(cell.StringCellValue);
                default:
                    throw new System.Exception("can't convert to float from " + cell.CellType);
            }
        }

        public static double GetDoubleValue(ICell cell)
        {
            switch (cell.CellType)
            {
                case CellType.Numeric:
                    return cell.NumericCellValue;
                case CellType.String:
                    return double.Parse(cell.StringCellValue);
                default:
                    throw new System.Exception("can't convert to double from " + cell.CellType);
            }
        }

        public static bool GetBoolValue(ICell cell)
        {
            switch (cell.CellType)
            {
                case CellType.Numeric:
                    return cell.NumericCellValue != 0;
                case CellType.String:
                    return bool.Parse(cell.StringCellValue);
                case CellType.Boolean:
                    return cell.BooleanCellValue;
                default:
                    throw new System.Exception("can't convert to bool from " + cell.CellType);
            }
        }

        public static string GetStringValue(ICell cell)
        {
            switch (cell.CellType)
            {
                case CellType.Numeric:
                    return cell.NumericCellValue.ToString();
                case CellType.Boolean:
                    return cell.BooleanCellValue.ToString();
                default:
                    return cell.StringCellValue;
            }
        }       
    }
}