using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using System.Reflection;
using System.Text;
using Debug = UnityEngine.Debug;
using Object = System.Object;
using uDebugger = UnityEngine.Debugger;
using System.Diagnostics;

namespace SGF
{
    public static class DebuggerExtension
    {
        [Conditional("EnableLog")]
        public static void Log(this object obj, string message)
        {
            if(!uDebugger.EnableLog)
            {
                return;
            }

            uDebugger.Log(GetLogTag(obj), message);
        }

        public static void LogError(this object obj, string message)
        {
            uDebugger.LogError(GetLogTag(obj), (string)message);
        }

        public static void LogWarning(this object obj, string message)
        {
            uDebugger.LogWarning(GetLogTag(obj), (string)message);
        }

        //---------------------------------------------------------
        [Conditional("EnableLog")]

        public static void Log(this object obj, string format, params object[] args)
        {
            if (!uDebugger.EnableLog)
            {
                return;
            }

            string message = string.Format(format, args);
            uDebugger.Log(GetLogTag(obj), message);
        }

        public static void LogError(this object obj, string format, params object[] args)
        {
            string message = string.Format(format, args);
            uDebugger.LogError(GetLogTag(obj), message);
        }

        public static void LogWarning(this object obj, string format, params object[] args)
        {
            string message = string.Format(format, args);
            uDebugger.LogWarning(GetLogTag(obj), message);
        }

        private static string GetLogTag(object obj)
        {
            FieldInfo field = obj.GetType().GetField("LOG_TAG");
            if(field != null)
            {
                return (string)field.GetValue(obj);
            }
            return obj.GetType().Name;
        }
    }


}


