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

        private Dictionary<CodeFomat, CodeGenTemplate> m_CodeTemplates = new Dictionary<CodeFomat, CodeGenTemplate>
        {
            {CodeFomat.CSharp, new CodeGenTemplateCSharp() },
            {CodeFomat.Cpp, new CodeGenTemplate() },
            {CodeFomat.Lua,new CodeGenTemplate() },
            {CodeFomat.Javascript,new CodeGenTemplate() }
        };

        private Dictionary<CodeFomat, string> m_GenCodeFileExts = new Dictionary<CodeFomat, string>
        {
            {CodeFomat.CSharp, ".cs"},
            {CodeFomat.Cpp, ".cpp" },
            {CodeFomat.Lua,".lua" },
            {CodeFomat.Javascript,".js" }
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

        public void SetCodeTemplate(CodeFomat format, CodeGenTemplate templatePath)
        {
            m_CodeTemplates[format] = templatePath;
        }

        public CodeGenTemplate GetCodeTemplate(CodeFomat format)
        {
            CodeGenTemplate genTemplate = null;
            m_CodeTemplates.TryGetValue(format, out genTemplate);
            return genTemplate;
        }

        public string GetGenerateCodeFileExt(CodeFomat format)
        {
            string ext = null;
            m_GenCodeFileExts.TryGetValue(format, out ext);
            return ext;
        }
    }
}
