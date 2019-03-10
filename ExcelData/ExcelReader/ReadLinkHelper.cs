using System.Collections;
using System.Collections.Generic;
using NPOI.SS.UserModel;

namespace TK.ExcelData
{
    public class ReadLinkHelper
    {
        public struct CellPosition
        {
            public int col;
            public int startRow;
            public int endRow;
        }

        public static CellPosition GetCellPosition(string posString)
        {
            CellPosition cp = new CellPosition();

            int col = 0;
            int startRow = 0;
            int endRow = 0;

            int i = 0;
            //col
            for (; i < posString.Length; ++i)
            {
                char c = posString[i];
                if ('a' <= c && c <= 'z')
                {
                    col = col * 26 + c - 'a' + 1;
                }
                else if ('A' <= c && c <= 'Z')
                {
                    col = col * 26 + c - 'A' + 1;
                }
                else
                {
                    break;
                }
            }
            //索引号从0开始
            cp.col = col - 1;
           
            //start row
            for (; i < posString.Length; ++i)
            {
                char c = posString[i];
                if ('0' <= c && c <= '9')
                {
                    startRow = startRow * 10 + c - '0';
                }
                else
                {
                    break;
                }
            }
            //索引号从0开始
            cp.startRow = startRow - 1;
            i++;
            //end row
            for (; i < posString.Length; ++i)
            {
                char c = posString[i];
                if ('0' <= c && c <= '9')
                {
                    endRow = endRow * 10 + c - '0';
                }
                else
                {
                    break;
                }
            }
            //索引号从0开始
            cp.endRow = endRow - 1;
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
                start.startRow = 0;
                start.col = 0;
                start.endRow = -1;
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

            return ReadListData(linkSheet, cp.col, cp.startRow,cp.endRow, t);
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
            return ReadHelper.ReadDictionary(linkSheet, schema, keyField,cp.startRow+Constance.SchemaDataRow,cp.col,null, removeKeyFieldInElement);
        }
    }
}
