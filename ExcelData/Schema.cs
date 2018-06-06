using System.Collections.Generic;

namespace TK.ExcelData
{

    public class Schema
    {
        string m_Name;

        List<Field> m_Fields;

        public Schema()
        {
            m_Fields = new List<Field>();
        }

        public void AddField(string name, ExcelDataType type)
        {
            if (!Exists(name))
            {
                Field field = new Field(name, type);
                m_Fields.Add(field);
            }
        }

        public void AddField(string name, ExcelDataType type,string extType)
        {
            if (!Exists(name))
            {
                Field field = new Field(name, type,extType);
                m_Fields.Add(field);
            }
        }

        public void AddField(Field field)
        {
            if (!Exists(field.name))
            {
                m_Fields.Add(field);
            }
        }

        public void RemoveField(string name,ExcelDataType type)
        {
            for (int i=0,l=m_Fields.Count;i< l;++i)
            {
                if (m_Fields[i].name == name)
                {
                    m_Fields.RemoveAt(i);
                    break;
                }
            }
        }

        public void UpdateField(string name, ExcelDataType newType)
        {
            for (int i = 0, l = m_Fields.Count; i < l; ++i)
            {
                if (m_Fields[i].name == name)
                {
                    m_Fields[i].type=newType;
                    break;
                }
            }
        }

        public bool Exists(string name)
        {
            foreach (Field field in m_Fields)
            {
                if (field.name == name)
                {
                    return true;
                }
            }
            return false;
        }

        public Field GetField(string name)
        {
            foreach (Field field in m_Fields)
            {
                if (field.name == name)
                {
                    return field;
                }
            }
            return null;
        }

        string CamelCase(string str)
        {
            if (!string.IsNullOrEmpty(str))
            {
                char[] chars = str.ToCharArray();
                int l = chars.Length;
                int j = 0;
                int i = 0;
                //check first char
                char c = chars[i];
                if (c >= 'a' && c <= 'z')
                {
                    chars[i] = char.ToUpper(c);
                }
                else if (c == '_')
                {
                    ++i;
                }

                for (; i < l; ++i, ++j)
                {
                    if (chars[i] == '_')
                    {
                        if (i + 1 < chars.Length)
                        {
                            c = chars[i + 1];

                            if (c >= 'a' && c <= 'z')
                            {
                                chars[i + 1] = char.ToUpper(c);
                            }

                            if (c != '_')
                            {
                                ++i;
                            }
                        }
                    }

                    if (j != i)
                    {
                        chars[j] = chars[i];
                    }
                }


                str = new string(chars, 0, j);
            }
            return str;
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

        public List<Field> fields
        {
            set
            {
                m_Fields = value;
            }

            get
            {
                return m_Fields;
            }
        }

        public string className
        {
            get
            {
                if (!string.IsNullOrEmpty(m_Name))
                {
                    return CamelCase(m_Name);
                }

                return m_Name;
            }
        }
    }
}