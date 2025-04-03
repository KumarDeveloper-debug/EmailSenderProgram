using System;
using System.IO;

namespace ErrorLogger
{
    public class Log
    {
        private static readonly string LogFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "EmailLog", "error.log");

        public static void LogError(Exception ex, string message)
        {
            LogError(ex.Message, message);
        }

        public static void LogError(string exception, string message)
        {
            if (!Directory.Exists(Path.GetDirectoryName(LogFilePath)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(LogFilePath));
            }

            using (var writer = new StreamWriter(LogFilePath, true))
            {
                writer.WriteLine($"{DateTime.Now} - {message} - {exception}");
            }
        }
    }
}
