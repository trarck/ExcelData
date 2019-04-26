using System.Collections.Generic;
using System.IO;
using NPOI.SS.UserModel;
using TK.Excel;

namespace TK.ExcelData
{
    public class ExportSetting
    {
        public delegate bool CheckSheetNeedExportHandle(ISheet sheet);
        public CheckSheetNeedExportHandle checkSheetNeedExportHandle;

        public delegate string SheetExportNameFormatHandle(ISheet sheet);
        public SheetExportNameFormatHandle sheetExportNameFormatHandle;

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
            set { m_HeadModel = value; }
        }

        private string m_TemplateFolder= "ExcelData/CodeGen/Templates";
        public string templateFolder{ get { return m_TemplateFolder; } set { m_TemplateFolder = value; } }

        private Dictionary<CodeFomat, string> m_CodeTemplates = new Dictionary<CodeFomat, string>
        {
            {CodeFomat.CSharp,"UnityCSharpClass.tt" },
            {CodeFomat.Cpp,"CppClass.tt" },
            {CodeFomat.Lua,"LuaClass.tt" },
            {CodeFomat.Javascript,"JavascriptClass.tt" }
        };

        public bool ExportCheck(ISheet sheet)
        {
            if (checkSheetNeedExportHandle != null)
            {
                return checkSheetNeedExportHandle(sheet);
            }
            return ExcelHelper.NeedExport(sheet);
        }

        public string FormatSheetName(ISheet sheet)
        {
            if (sheetExportNameFormatHandle != null)
            {
                return sheetExportNameFormatHandle(sheet);
            }
            return ExcelHelper.GetExportName(sheet);
        }

        public void SetCodeTemplate(CodeFomat format,string templatePath)
        {
            m_CodeTemplates[format] = templatePath;
        }

        public string GetCodeTemplate(CodeFomat format)
        {
            string templatePath = null;
            if(m_CodeTemplates.TryGetValue(format,out templatePath))
            {
                if (Path.IsPathRooted(templatePath))
                {
                    return templatePath;
                }

                if (string.IsNullOrEmpty(templatePath))
                {
                    return Path.Combine(Directory.GetCurrentDirectory(), templatePath);
                }
                else
                {
                    return Path.Combine(templateFolder, templatePath);
                }                
            }
            return null;
        }
    }
}
