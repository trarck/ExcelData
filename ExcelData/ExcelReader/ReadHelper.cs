using System.Collections.Generic;
using NPOI.SS.UserModel;

namespace TK.ExcelData
{
    public class ReadHelper
    {
        #region header
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
        #endregion

        #region cell
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
        #endregion

        #region row

        static Dictionary<string, object> ReadRowData(IRow row, List<Field> headerFields)
        {
            if (headerFields == null || headerFields.Count == 0) return null;

            Dictionary<string, object> data = new Dictionary<string, object>();
            IEnumerator<ICell> iter = row.GetEnumerator();
            int index = 0;

            Field field;

            while (iter.MoveNext() && index < headerFields.Count)
            {
                field = headerFields[index];
                data[field.name] = GetCellValue(iter.Current, field.type);
                ++index;
            }

            return data;
        }

        static Dictionary<string, object> ReadRowData(IRow row, List<Field> headerFields, int colStartOffset)
        {
            if (headerFields == null || headerFields.Count == 0) return null;

            Dictionary<string, object> data = new Dictionary<string, object>();
            int index = 0;

            Field field;

            //offset 相对于0开始，excel最左边一列不能为空
            for (int i = row.FirstCellNum + colStartOffset; i < row.LastCellNum; ++i)
            {
                field = headerFields[index];
                data[field.name] = GetCellValue(row.GetCell(i), field.type);
                ++index;
            }

            return data;
        }

        static Dictionary<string, object> ReadRowDataFromColIndex(IRow row, List<Field> headerFields, int colStartIndex)
        {
            if (headerFields == null || headerFields.Count == 0) return null;

            Dictionary<string, object> data = new Dictionary<string, object>();
            int index = 0;

            Field field;

            for (int i = colStartIndex; i < row.LastCellNum; ++i)
            {
                field = headerFields[index];
                data[field.name] = GetCellValue(row.GetCell(i), field.type);
                ++index;
            }

            return data;
        }

        #endregion

        #region list
        public static List<object> ReadList(ISheet sheet, Schema schema)
        {
            return ReadList(sheet, schema, Constance.SchemaDataRow, 0, null);
        }

        public static List<object> ReadList(ISheet sheet, Schema schema, int dataStartOffset)
        {
            return ReadList(sheet, schema, dataStartOffset, 0, null);
        }

        public static List<object> ReadList(ISheet sheet, Schema schema, int dataStartOffset, int colStartOffset, List<string> header)
        {

            if (header == null || header.Count == 0)
            {
                header = ReadHelper.GetHeader(sheet, 0, colStartOffset);
            }

            List<Field> headerFields = ReadHelper.PrepareHeaderFields(header, schema);

            List<object> list = new List<object>();

            for (int i = sheet.FirstRowNum + dataStartOffset; i <= sheet.LastRowNum; ++i)
            {
                Dictionary<string, object> record = ReadRowData(sheet.GetRow(i), headerFields, colStartOffset);
                list.Add(record);
            }
            return list;
        }

        #endregion

        #region dictionary

        public static Dictionary<string, object> ReadDictionary(ISheet sheet, Schema schema)
        {
            return ReadDictionary(sheet, schema, "", Constance.SchemaDataRow, 0, null);
        }

        public static Dictionary<string, object> ReadDictionary(ISheet sheet, Schema schema, string keyField)
        {
            return ReadDictionary(sheet, schema, keyField, Constance.SchemaDataRow, 0, null);
        }

        public static Dictionary<string, object> ReadDictionary(ISheet sheet, Schema schema, string keyField, int dataStartOffset, int colStartOffset, List<string> header, bool removeKeyInElement = false)
        {

            if (header == null || header.Count == 0)
            {
                header = ReadHelper.GetHeader(sheet, 0, colStartOffset);
            }

            List<Field> headerFields = ReadHelper.PrepareHeaderFields(header, schema);

            //如果没指定key,则默认使用第一个
            if (string.IsNullOrEmpty(keyField))
            {
                keyField = header[0];
            }

            Dictionary<string, object> dict = new Dictionary<string, object>();

            for (int i = sheet.FirstRowNum + dataStartOffset; i <= sheet.LastRowNum; ++i)
            {
                Dictionary<string, object> record = ReadRowData(sheet.GetRow(i), headerFields, colStartOffset);
                string key = record[keyField].ToString();
                dict[key] = record;
                if (removeKeyInElement)
                {
                    record.Remove(keyField);
                }
            }
            return dict;
        }
        #endregion
    }
}