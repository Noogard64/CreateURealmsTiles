using System;
using System.IO;

namespace CreateURealmsTiles
{
    class Logging
    {
        static public void CreateLogFile()
        {
            string logFile = Variables.GetLogFile();

            if (File.Exists(logFile))
            {
                File.Delete(logFile);
            }

            var file = File.CreateText(logFile);
            file.Close();
        }

        static public void WriteToLogFile(string logValue)
        {
            string createText = logValue + Environment.NewLine;
            string logFile = Variables.GetLogFile();

            var file = File.AppendText(logFile);
            file.WriteLine(logValue);
            file.Close();
            Console.WriteLine(logValue);
        }
    }
}
