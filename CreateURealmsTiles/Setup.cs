using System;
using System.IO;
using System.Windows.Forms;

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


            if (File.Exists(gimpLocation) == false)
            {

                OpenFileDialog openFileDialog1 = new OpenFileDialog
                {

                    Title = "'gimp-2.8.exe' not found in default location. Please select it.",
                    InitialDirectory = @"C:\Program Files\GIMP 2\bin",
                    Filter = "gimp-2.8.exe|gimp-2.8.exe",
                    FilterIndex = 2,
                    RestoreDirectory = true

                };

                openFileDialog1.ShowDialog();

                string results = openFileDialog1.FileName;
                Functions.WriteToLogFile("Custom location for 'gimp-2.8.exe' selected: [" + results + "]");
                

            }

            return gimpLocation;
        }

        static public string GetImageFile()
        {

            OpenFileDialog openFileDialog1 = new OpenFileDialog
            {

                Title = "What image do you want to use?",
                InitialDirectory = @"C:\Users\"+ Environment.UserName + @"\Desktop",
                Filter = "Image Files|*.png;*.jpg;*.jpeg",
                FilterIndex = 2,
                RestoreDirectory = true

            };

            openFileDialog1.ShowDialog();

            string results = openFileDialog1.FileName;
            Functions.WriteToLogFile("Image selected at this location: [" + results + "]");

            return results;

        }
    }
}
