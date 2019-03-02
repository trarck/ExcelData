using System.Collections;
using System.Collections.Generic;
using NPOI.SS.UserModel;

namespace TK.ExcelData
{
    public class DataReader
    {

        public static List<object> ReadList(ISheet sheet, Schema schema)
        {
            return ReadList(sheet, schema, EDConstance.SchemaDataRow, 0, null);
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

        public static Dictionary<string, object> ReadDictionary(ISheet sheet, Schema schema)
        {
            return ReadDictionary(sheet, schema, "", EDConstance.SchemaDataRow, 0, null);
        }

        public static Dictionary<string, object> ReadDictionary(ISheet sheet, Schema schema, string keyField)
        {
            return ReadDictionary(sheet, schema, keyField, EDConstance.SchemaDataRow, 0, null);
        }

        public static Dictionary<string, object> ReadDictionary(ISheet sheet, Schema schema, string keyField, int dataStartOffset, int colStartOffset, List<string> header,bool removeKeyInElement=false)
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
                data[field.name] = GetCellValue(iter.Current, field);
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
                data[field.name] = GetCellValue(row.GetCell(i), field);
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
                data[field.name] = GetCellValue(row.GetCell(i), field);
                ++index;
            }

            return data;
        }

        public static object GetCellValue(ICell cell, Field field)
        {
            if (cell != null)
            {
                switch (field.type.sign)
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
                    case TypeInfo.Sign.List:
                        return LinkDataReader.GetLinkData(cell, field.ExtTypeToSystemType());
                    case TypeInfo.Sign.Array:
                        return LinkDataReader.GetLinkArray(cell, field.ExtTypeToSystemType());
                    case TypeInfo.Sign.Dictionary:
                        return LinkDataReader.GetLinkDict(cell, field.extTypeKeyField);
                    default:
                        break;
                }
            }
            return null;
        }
    }
}