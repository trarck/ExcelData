﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Log
{
    public enum TargetType
    {
        File,
        Console
    }

    public class TargetKey
    {
        TargetType m_TargetType;
        string m_Sign;

        public TargetKey(TargetType targetType,string sign)
        {
            m_TargetType = targetType;
            m_Sign = sign;
        }

        public override bool Equals(object obj)
        {
            TargetKey other = obj as TargetKey;
            if (other!=null)
            {
                return other.m_TargetType == m_TargetType && other.m_Sign == m_Sign; ;
            }
            return false;
        }

        public override int GetHashCode()
        {
            if (string.IsNullOrEmpty(m_Sign))
            {
                return m_TargetType.GetHashCode();
            }
            else
            {
                return (m_TargetType.ToString() + m_Sign).GetHashCode();
            }
        }
    }

    public class TargetManager:IDisposable
    {
        Dictionary<TargetKey, ITarget> m_Targets=new Dictionary<TargetKey, ITarget>();

        public ITarget GetTarget(TargetType targetType, string ext=null)
        {
            ITarget target = null;
            TargetKey key = new TargetKey(targetType, ext);

            if(m_Targets.TryGetValue(key,out target))
            {
                return target;
            }

            switch (targetType)
            {
                case TargetType.Console:
                    ConsoleTarget consoleTarget = new ConsoleTarget();
                    consoleTarget.Init();
                    target = consoleTarget;
                    break;
                case TargetType.File:
                    string filepath = ext;
                    FileTarget fileTarget = new FileTarget(filepath);
                    fileTarget.Init();
                    target = fileTarget;
                    break;
                default:
                    throw new Exception( "Don't support type " + targetType.ToString());
            }

            if (target != null)
            {
                m_Targets[key] = target;
            }

            return target;
        }

        public void Dispose()
        {
            foreach(var iter in m_Targets)
            {
                IDisposable disp = iter.Value as IDisposable;
                if (disp!=null)
                {
                    disp.Dispose();
                }
            }

            m_Targets.Clear();
            m_Targets = null;
        }
    }
}
