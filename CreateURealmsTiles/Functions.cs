using System;
using System.IO;
using System.Linq;
using System.Diagnostics;

namespace CreateURealmsTiles
{
    class Functions
    {




        static public void WriteToLogFile(string logValue)
        {
            string createText = logValue + Environment.NewLine;
            string logFile = GetLogFile();

            var file = File.AppendText(logFile);
            file.WriteLine(logValue);
            file.Close();
            Console.WriteLine(logValue);
        }

        static public string GetLogFile()
        {
            return GetTempFolder() + "\\log.txt";
        }
        

        

        static public string[] GetImageFilesAsCollection(string file)
        {
            string fileNameNoExt = Path.GetFileName(file);

            string folderPath = ImageFolderInTempFolder(file);

            string[] files = Directory.GetFiles(folderPath);

            return files;

        }

        static public string GetTempFolder()
        {
            return "C:\\Users\\" + Environment.UserName + "\\AppData\\Local\\Temp\\CreateUrealmsTiles";
        }

        static public string ImageFolderInTempFolder(string file)
        {
            string fileNameNoExt = Path.GetFileName(file);
            string folder = GetTempFolder();
            return folder + "\\" + fileNameNoExt.Replace(".png", "");
        }

        static public string GetImageFileNameWithNoExt(string file)
        {
            return Path.GetFileName(file).Replace(".png","");
        }

        static public void MakeImages(string gimpLocation, string file)
        {
            try
            {
                string args = "gimp --as-new --verbose --no-interface -idf --batch-interpreter=python-fu-eval -b \"import sys; sys.path =['.'] + sys.path; import batch_CreateURealmsTileImages; batch_CreateURealmsTileImages.run('" + file + "')\" -b \"pdb.gimp_quit(1)\"";
                var startInfo = new ProcessStartInfo
                {
                    WorkingDirectory = Environment.CurrentDirectory,
                    //WorkingDirectory = @"C:\Users\Sean\Desktop\repos\CreateURealmsTiles\CreateURealmsTiles",
                    WindowStyle = ProcessWindowStyle.Normal,
                    //FileName = @"C:\Program Files\GIMP 2\bin\gimp-2.8.exe",
                    FileName = gimpLocation,
                    RedirectStandardInput = true,
                    UseShellExecute = false,
                    Verb = "runas",
                    Arguments = args
                };
                WriteToLogFile("#################################");

                WriteToLogFile("Executing the file below:");
                WriteToLogFile(gimpLocation);

                WriteToLogFile("Using the arguments below:");
                WriteToLogFile(args);

                WriteToLogFile("From the directory below:");
                WriteToLogFile(Environment.CurrentDirectory.ToString());

                WriteToLogFile("#################################");

                Process.Start(startInfo);
            }
            catch (Exception e)
            {
                WriteToLogFile(e.ToString());
            }
        }
    }
}
