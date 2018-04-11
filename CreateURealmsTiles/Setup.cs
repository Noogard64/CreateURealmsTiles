using System;
using System.IO;

namespace CreateURealmsTiles
{
    class Setup
    {
        static public void CreateTempFolder()
        {
            string tempFolder = Functions.GetTempFolder();
            Directory.CreateDirectory(tempFolder);
        }

        static public void CreateLogFile()
        {
            string logFile = Functions.GetLogFile();

            if (File.Exists(logFile))
            {
                File.Delete(logFile);
            }

            var file = File.CreateText(logFile);
            file.Close();
        }

        static public string GetGimpExeFile()
        {
            string gimpLocation = @"C:\Program Files\GIMP 2\bin\gimp-2.8.exe";

            bool gimpFound = false;

            do
            {
                if (File.Exists(gimpLocation) == false)
                {
                    Functions.WriteToLogFile("'gimp-2.8.exe' not found in default location. Please enter location.");
                    gimpLocation = Console.ReadLine();

                }
                else
                {
                    gimpFound = true;
                }
            } while (gimpFound == false);
            return gimpLocation;
        }

        static public string GetImageFile()
        {
            bool fileFound = false;
            string file;

            Functions.WriteToLogFile("Input filepath of image:");
            do
            {

                file = Console.ReadLine();
                if (File.Exists(file) == false)
                {
                    Functions.WriteToLogFile("Image not found. Try again.");
                }
                else
                {
                    fileFound = true;
                }


            } while (fileFound == false);
            return file;
        }
    }
}
