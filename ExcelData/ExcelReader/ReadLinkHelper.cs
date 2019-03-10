using System.Collections;
using System.Collections.Generic;
using NPOI.SS.UserModel;

namespace TK.ExcelData
{
    public class ReadLinkHelper
    {
        public struct CellPosition
        {
            public int colStart;
            public int colEnd;
            public int rowStart;
            public int rowEnd;
        }

        /// <summary>
        /// 解析出目标位置
        /// A1B2;A1,B2;A1,2;1,B2;1,2
        /// </summary>
        /// <param name="posString"></param>
        /// <returns></returns>
        public static CellPosition GetCellPosition(string posString)
        {
            CellPosition cp = new CellPosition();

            int colStart = 0;
            int colEnd = 0;
            int rowStart = 0;
            int rowEnd = 0;

            int i = 0;

            //col start
            for (; i < posString.Length; ++i)
            {
                char c = posString[i];
                if ('a' <= c && c <= 'z')
                {
                    colStart = colStart * 26 + c - 'a'+1;
                }
                else if ('A' <= c && c <= 'Z')
                {
                    colStart = colStart * 26 + c - 'A'+1;
                }
                else
                {
                    break;
                }
            }
            //从0开始
            if (colStart == 0)
            {
                cp.colStart = 0;
            }
            else
            {
                cp.colStart = colStart - 1;
            }

            //row start
            for (; i < posString.Length; ++i)
            {
                char c = posString[i];
                if ('0' <= c && c <= '9')
                {
                    rowStart = rowStart * 10 + c - '0';
                }
                else
                {
                    if (c == ',')
                    {
                        ++i;
                    }
                    break;
                }
            }
            //从0开始
            if (rowStart == 0)
            {
                cp.rowStart = 0;
            }
            else
            {
                cp.rowStart = rowStart - 1;
            }

            //col end
            for (; i < posString.Length; ++i)
            {
                char c = posString[i];
                if ('a' <= c && c <= 'z')
                {
                    colEnd = colEnd * 26 + c - 'a' + 1;
                }
                else if ('A' <= c && c <= 'Z')
                {
                    colEnd = colEnd * 26 + c - 'A' + 1;
                }
                else
                {
                    break;
                }
            }
            //从0开始。
            cp.colEnd = colEnd -1;

            //end row
            for (; i < posString.Length; ++i)
            {
                char c = posString[i];
                if ('0' <= c && c <= '9')
                {
                    rowEnd = rowEnd * 10 + c - '0';
                }
                else
                {
                    break;
                }
            }
            //索引号从0开始
            cp.rowEnd = rowEnd - 1;
            return cp;
        }

        static string ParseLinkCell(ICell cell, out CellPosition cp)
        {
            string key;
            return ParseLinkCell(cell, out cp, out key);
        }

        static string ParseLinkCell(ICell cell, out CellPosition start,out string key)
        {
            string linkWhere = cell.StringCellValue;

            string linkSheetName = "";

            int pos = linkWhere.IndexOf("!");
            if (pos > -1)
            {
                //表的开始位置
                int endPos = linkWhere.IndexOf(":");
                string linkCellPositionStr = null;
                if (endPos > -1) {
                    linkCellPositionStr = linkWhere.Substring(pos + 1,endPos-pos-1);
                }
                else
                {
                    linkCellPositionStr = linkWhere.Substring(pos + 1);
                }
                start = GetCellPosition(linkCellPositionStr);

                linkSheetName = linkWhere.Substring(0, pos);
            }
            else
            {
                //第一列，第一行
                start = new CellPosition();
                start.rowStart = 0;
                start.colStart = 0;
                start.rowEnd = -1;
                linkSheetName = linkWhere;
            }

            pos = linkWhere.IndexOf(":");
            if (pos > -1)
            {
                key = linkWhere.Substring(pos+1);
            }
            else
            {
                key = null;
            }

            return linkSheetName;
        }
        
        static List<T> ReadList<T>(ISheet sheet, int colIndex, int startRow, int endRow, TypeInfo dataType)
        {
            List<T> list = new List<T>();
            int l = endRow <= 0 ? sheet.LastRowNum : (endRow < sheet.LastRowNum ? endRow : sheet.LastRowNum);
            for (int i = sheet.FirstRowNum + startRow; i <= l; ++i)
            {
                IRow row = sheet.GetRow(i);
                ICell cell = row.GetCell(row.FirstCellNum + colIndex);
                list.Add((T)ReadHelper.GetCellValue(cell, dataType));
            }
            return list;
        }

        static object ReadListData(ISheet sheet, int colIndex, int startRow, int endRow, TypeInfo t)
        {
            switch (t.sign)
            {
                case TypeInfo.Sign.Int:
                    return ReadList<int>(sheet, colIndex,startRow, endRow, t);
                case TypeInfo.Sign.Float:
                    return ReadList<float>(sheet, colIndex, startRow, endRow, t);
                case TypeInfo.Sign.Long:
                    return ReadList<long>(sheet, colIndex, startRow, endRow, t);
                case TypeInfo.Sign.Double:
                    return ReadList<double>(sheet, colIndex, startRow, endRow, t);
                case TypeInfo.Sign.Boolean:
                    return ReadList<bool>(sheet, colIndex, startRow, endRow, t);
                case TypeInfo.Sign.String:
                    return ReadList<string>(sheet, colIndex, startRow, endRow, t);
                default:
                    Schema schema = SchemaReader.ReadSchema(sheet);
                    return ReadHelper.ReadList(sheet, schema,startRow,endRow);
            }
        }

        public static object ReadLinkList(ICell cell, TypeInfo t)
        {
            if (cell == null || cell.StringCellValue=="") return null;

            string linkWhere = cell.StringCellValue;
            CellPosition cp;
            string linkSheetName = ParseLinkCell(cell, out cp);            

            ISheet linkSheet = cell.Sheet.Workbook.GetSheet(linkSheetName);

            return ReadListData(linkSheet, cp.colStart, cp.rowStart,cp.rowEnd, t);
        }
                
        public static object ReadLinkArray(ICell cell, TypeInfo t)
        {
            return ReadLinkList(cell, t); ;
        }

        public static object ReadLinkDict(ICell cell, string keyField=null,bool removeKeyFieldInElement=false)
        {
            if (cell == null || cell.StringCellValue == "") return null;
            string linkWhere = cell.StringCellValue;
            CellPosition cp;
            string cellKey;
            string linkSheetName = ParseLinkCell(cell, out cp,out cellKey);
            if (string.IsNullOrEmpty(keyField))
            {
                keyField = cellKey;
            }

            ISheet linkSheet = cell.Sheet.Workbook.GetSheet(linkSheetName);
            Schema schema = SchemaReader.ReadSchema(linkSheet);

            //内容要跳过头
            return ReadHelper.ReadDictionary(linkSheet, schema, keyField,cp.rowStart,cp.colStart,cp.colEnd+1,null, removeKeyFieldInElement,cp.rowEnd);
        }

        public static object ReadLinkObject(ICell cell, TypeInfo t)
        {
            if (cell == null || cell.StringCellValue == "") return null;
            string linkWhere = cell.StringCellValue;
            CellPosition cp;
            string linkSheetName = ParseLinkCell(cell, out cp);
            if (cp.rowEnd <= 0)
            {
                cp.rowEnd = cp.rowStart;
            }

            ISheet linkSheet = cell.Sheet.Workbook.GetSheet(linkSheetName);
            Schema schema = SchemaReader.ReadSchema(linkSheet);

            //内容要跳过头
            return ReadHelper.ReadList(linkSheet, schema, cp.rowStart, cp.rowEnd, cp.colStart,cp.colEnd+1,null)[0];
        }
    }
}
