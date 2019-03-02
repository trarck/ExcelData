using System;
using System.Collections.Generic;

namespace TK.ExcelData
{
    public class Field
    {
        //名称
        string m_Name;

        //描述
        string m_Description;

        //数据类型
        TypeInfo m_Type;

        //注释
        string m_Comment;

        //纯的成员类型
        string m_ExtMemberType;

        //用于dictionary的key指向的字段
        string m_ExtTypeKeyField;

        public Field()
        {

        }

        public Field(string name,TypeInfo type)
        {
            m_Name = name;
            m_Type = type;
        }
        
        public Field(string name, TypeInfo type, string comment,string description)
        {
            m_Name = name;
            m_Type = type;
            m_Comment = comment;
            m_Description = description;
        }

        public string GetMemberType()
        {
            return "";
        }

        public string GetExtTypeKeyField()
        {
         
                return "";
      
        }

        public override string ToString()
        {
            return string.Format("Field[{0}]--{1}",m_Name ,m_Type);
        }

        public string name
        {
            set
            {
                m_Name = value;
            }

            get
            {
                return m_Name;
            }
        }

        public TypeInfo type
        {
            set
            {
                m_Type = value;
            }

            get
            {
                return m_Type;
            }
        }
        
        public string comment
        {
            set
            {
                m_Comment = value;
            }

            get
            {
                return m_Comment;
            }
        }

        public string extMemberType
        {
            get
            {
                return m_ExtMemberType;
            }
        }

        public string extTypeKeyField
        {
            get
            {
                return m_ExtTypeKeyField;
            }
        }

        public string description
        {
            get
            {
                return m_Description;
            }
            set
            {
                m_Description = value;
            }
        }
    }
}