using System;
using System.IO;
using System.Text;

namespace UnityEngine
{
    public class Debugger
    {
        public static bool EnableLog = true;
        public static bool EnableTime = true;
        public static bool EnableSave = false;
        public static bool EnableStack = false;
        public static string LogFileDir = Application.persistentDataPath + "/DebuggerLog/";
        public static string LogFileName = string.Empty;
        public static string Prefix = ">";
        public static StreamWriter LogFileWriter = null;

        public static void Log(object message)
        {
            if (!Debugger.EnableLog)
            {
                return;
            }

            string msg = GetLogTime() + message;

            Debug.Log(Prefix + msg, null);
            LogToFile("[I]" + msg);
        }

        public static void Log(object message, Object context)
        {
            if(!Debugger.EnableLog)
            {
                return;
            }

            string msg = GetLogTime() + message;
            Debug.Log(Prefix + msg, context);
            LogToFile("[I]" + msg);
        }

        public static void LogError(object message)
        {
            string msg = GetLogTime() + message;

            Debug.LogError(Prefix + msg, null);
            LogToFile("[E]" + msg, true);
        }

        public static void LogError(object message, Object context)
        {
            string msg = GetLogTime() + message;

            Debug.LogError(Prefix + msg, context);
            LogToFile("[E]" + msg, true);
        }

        public static void LogWarning(object message)
        {
            string msg = GetLogTime() + message;

            Debug.LogWarning(Prefix + msg, null);
            LogToFile("[E]" + msg, true);
        }

        public static void LogWarning(object message, Object context)
        {
            string msg = GetLogTime() + message;

            Debug.LogWarning(Prefix + msg, context);
            LogToFile("[E]" + msg, true);
        }

        //---------------------------------------------------------------
        public static void Log(string tag, string message)
        {
            if (EnableLog)
            {
                message = GetLogText(tag, message);
                Debug.Log(Prefix + message, null);
                LogToFile("[I]" + message, false);
            }
        }

        public static void Log(string tag, string format, params object[] args)
        {
            if (EnableLog)
            {
                string logText = GetLogText(tag, string.Format(format, args));
                Debug.Log(Prefix + logText, null);
                LogToFile("[I]" + logText, false);
            }
        }

        public static void LogWarning(string tag, string message)
        {
            message = GetLogText(tag, message);
            Debug.LogWarning(Prefix + message, null);
            LogToFile("[W]" + message, false);
        }

        public static void LogWarning(string tag, string format, params object[] args)
        {
            string logText = GetLogText(tag, string.Format(format, args));
            Debug.LogWarning(Prefix + logText, null);
            LogToFile("[W]" + logText, false);
        }

        public static void LogError(string tag, string message)
        {
            message = GetLogText(tag, message);
            Debug.LogError(Prefix + message, null);
            LogToFile("[E]" + message, true);
        }

        public static void LogError(string tag, string format, params object[] args)
        {
            string logText = GetLogText(tag, string.Format(format, args));
            Debug.LogError(Prefix + logText, null);
            LogToFile("[E]" + logText, true);
        }

        //---------------------------------------------------------------

        private static string GetLogText(string tag, string message)
        {
            string str = "";
            if (EnableTime)
            {
                str = DateTime.Now.ToString("HH:mm:ss.fff") + " ";
            }
            return (str + tag + "::" + message);
        }

        private static string GetLogTime()
        {
            string str = "";
            if(Debugger.EnableTime)
            {
                DateTime now = DateTime.Now;
                str = now.ToString("HH:mm:ss.fff") + " ";
            }
            return str;
        }

        private static void LogToFile(string message, bool EnableStack = false)
        {
            if(!EnableSave)
            {
                return;
            }

            if(LogFileWriter == null)
            {
                DateTime now = DateTime.Now;
                LogFileName = now.GetDateTimeFormats('s')[0].ToString();
                LogFileName = LogFileName.Replace("-", "_");
                LogFileName = LogFileName.Replace(":", "_");
                LogFileName = LogFileName.Replace(" ", "");
                LogFileName += ".log";

                string fullPath = LogFileDir + LogFileName;
                try
                {
                    if(!Directory.Exists(LogFileDir))
                    {
                        Directory.CreateDirectory(LogFileDir);
                    }

                    LogFileWriter = File.AppendText(fullPath);
                    LogFileWriter.AutoFlush = true;
                }
                catch(Exception e)
                {
                    LogFileWriter = null;
                    Debug.LogError("LogToCacht() " + e.Message + e.StackTrace);
                    return;
                }
            }

            if(LogFileWriter != null)
            {
                try
                {
                    LogFileWriter.WriteLine(message);
                    if(EnableStack || Debugger.EnableStack)
                    {
                        LogFileWriter.WriteLine(StackTraceUtility.ExtractStackTrace());
                    }
                }
                catch(Exception e)
                {
                    Debug.LogError(e.Message);
                    return;
                }
            }
        }
    }
}

