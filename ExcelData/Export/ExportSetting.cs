using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.IO;

namespace TK.ExcelData
{
    public class ExportSetting
    {
        public delegate bool CheckSheetNeedExportHandle(ISheet sheet);
        public CheckSheetNeedExportHandle checkSheetNeedExportHandle;

        public delegate string SheetExportNameFormateHandle(ISheet sheet);
        public SheetExportNameFormateHandle sheetExportNameFomateHandle;

        private bool m_CodeUseSubPath = false;
        public bool codeUseSubPath
        {
            get
            {
                return m_CodeUseSubPath;
            }
            set
            {
                m_CodeUseSubPath = value;
            }
        }

        private bool m_DataUseSubPath = false;
        public bool dataUseSubPath
        {
            get
            {
                return m_DataUseSubPath;
            }
            set
            {
                m_DataUseSubPath = value;
            }
        }

        private HeadModel m_HeadModel = HeadModel.CreateModel();
        public HeadModel headModel
        {
            get { return m_HeadModel; }
            set{ m_HeadModel = value; }
        }

        private Dictionary<CodeFomate, string> m_CodeTemplates = new Dictionary<CodeFomate, string>
        {
            {CodeFomate.CSharp,"ExcelData/CodeGen/Templates/UnityCSharpClass.tt" },
            {CodeFomate.Cpp,"ExcelData/CodeGen/Templates/CppClass.tt" },
            {CodeFomate.Lua,"ExcelData/CodeGen/Templates/LuaClass.tt" },
            {CodeFomate.Javascript,"ExcelData/CodeGen/Templates/JavascriptClass.tt" }
        };

        public bool ExportCheck(ISheet sheet)
        {
            if (checkSheetNeedExportHandle != null)
            {
                return checkSheetNeedExportHandle(sheet);
            }
            return ExcelHelper.NeedExport(sheet);
        }

        public string FomateSheetName(ISheet sheet)
        {
            if (sheetExportNameFomateHandle != null)
            {
                return sheetExportNameFomateHandle(sheet);
            }
            return ExcelHelper.GetExportName(sheet);
        }

        public void SetCodeTemplate(CodeFomate fomate,string templatePath)
        {
            m_CodeTemplates[fomate] = templatePath;
        }

        public string GetCodeTemplate(CodeFomate fomate)
        {
            string templatePath = null;
            if(m_CodeTemplates.TryGetValue(fomate,out templatePath))
            {
                if (Path.IsPathRooted(templatePath))
                {
                    return templatePath;
                }

                return Path.Combine(Directory.GetCurrentDirectory(), templatePath);
            }
            return null;
        }
    }
}
