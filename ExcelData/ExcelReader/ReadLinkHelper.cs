using System.Collections;
using System.Collections.Generic;
using NPOI.SS.UserModel;

namespace TK.ExcelData
{
    public class ReadLinkHelper
    {
        public struct CellPosition
        {
            public int row;
            public int col;
        }

        public static CellPosition GetCellPosition(string posString)
        {
            CellPosition cp = new CellPosition();

            int row = 0;
            int col = 0;
            int i = 0;

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

            for (; i < posString.Length; ++i)
            {
                char c = posString[i];
                if ('0' <= c && c <= '9')
                {
                    row = row * 10 + c - '0';
                }
                else
                {
                    throw new System.Exception("Parse link cell position errro for c=" + c);
                }
            }
            //索引号从0开始
            cp.row = row - 1;
            cp.col = col - 1;

            return cp;
        }
        static string ParseLinkCell(ICell cell, out CellPosition cp)
        {
            string linkWhere = cell.StringCellValue;

            string linkSheetName = "";

            int pos = linkWhere.IndexOf("!");
            if (pos > -1)
            {
                //表的开始位置
                string linkCellPosition = linkWhere.Substring(pos + 1);
                cp = GetCellPosition(linkCellPosition);

                linkSheetName = linkWhere.Substring(0, pos);
            }
            else
            {
                //第一列，第一行
                cp = new CellPosition();
                cp.row = 0;
                cp.col = 0;

                linkSheetName = linkWhere;
            }
            return linkSheetName;
        }
        
        static List<T> ReadList<T>(ISheet sheet, int rowIndex, int colIndex, TypeInfo dataType)
        {
            List<T> list = new List<T>();

            for (int i = sheet.FirstRowNum + rowIndex; i <= sheet.LastRowNum; ++i)
            {
                IRow row = sheet.GetRow(i);
                ICell cell = row.GetCell(row.FirstCellNum + colIndex);
                list.Add((T)ReadHelper.GetCellValue(cell, dataType));
            }
            return list;
        }

        static object ReadListData(ISheet sheet, int rowIndex, int colIndex, TypeInfo t)
        {
            switch (t.sign)
            {
                case TypeInfo.Sign.Int:
                    return ReadList<int>(sheet, rowIndex, colIndex,t);
                case TypeInfo.Sign.Float:
                    return ReadList<float>(sheet, rowIndex, colIndex, t);
                case TypeInfo.Sign.Long:
                    return ReadList<long>(sheet, rowIndex, colIndex, t);
                case TypeInfo.Sign.Double:
                    return ReadList<double>(sheet, rowIndex, colIndex, t);
                case TypeInfo.Sign.Boolean:
                    return ReadList<bool>(sheet, rowIndex, colIndex, t);
                case TypeInfo.Sign.String:
                    return ReadList<string>(sheet, rowIndex, colIndex, t);
                default:
                    Schema schema = SchemaReader.ReadSchema(sheet);
                    return ReadHelper.ReadList(sheet, schema);
            }
        }

        public static object ReadLinkList(ICell cell, TypeInfo t)
        {
            if (cell == null || cell.StringCellValue=="") return null;

            string linkWhere = cell.StringCellValue;
            CellPosition cp;
            string linkSheetName = ParseLinkCell(cell, out cp);            

            ISheet linkSheet = cell.Sheet.Workbook.GetSheet(linkSheetName);

            return ReadListData(linkSheet, cp.row, cp.col, t);
        }
                
        public static object GetLinkArray(ICell cell, TypeInfo t)
        {
            return ReadLinkList(cell, t); ;
        }

        public static object GetLinkDict(ICell cell, string keyField,bool removeKeyFieldInElement=false)
        {
            if (cell == null || cell.StringCellValue == "") return null;
            string linkWhere = cell.StringCellValue;
            CellPosition cp;
            string linkSheetName = ParseLinkCell(cell, out cp);

            ISheet linkSheet = cell.Sheet.Workbook.GetSheet(linkSheetName);
            Schema schema = SchemaReader.ReadSchema(linkSheet);

            //内容要跳过头
            return ReadHelper.ReadDictionary(linkSheet, schema, keyField,cp.row+Constance.SchemaDataRow,cp.col,null, removeKeyFieldInElement);
        }
    }
}
