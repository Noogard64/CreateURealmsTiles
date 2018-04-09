using System;
using System.IO;
using System.Linq;
using System.Diagnostics;

namespace CreateURealmsTiles
{
    class Functions
    {
        static public void CreateTempFolder()
        {
            string tempFolder = GetTempFolder();
            Directory.CreateDirectory(tempFolder);
        }

        static public void CreateLogFile()
        {
            string logFile = GetLogFile();

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

        static public string GetGimpExePath()
        {
            string gimpLocation = @"C:\Program Files\GIMP 2\bin\gimp-2.8.exe";

            bool gimpFound = false;

            do
            {
                if (File.Exists(gimpLocation) == false)
                {
                    WriteToLogFile("'gimp-2.8.exe' not found in default location. Please enter location.");
                    gimpLocation = Console.ReadLine();

                }
                else
                {
                    gimpFound = true;
                }
            } while (gimpFound == false);
            return gimpLocation;
        }

        static public string GetFile()
        {
            bool fileFound = false;
            string file;

            WriteToLogFile("Input filepath of image:");
            do
            {
                
                file = Console.ReadLine();
                if (File.Exists(file) == false)
                {
                    WriteToLogFile("Image not found. Try again.");
                }
                else
                {
                    fileFound = true;
                }


            } while (fileFound == false);
            return file;
        }

        static public string[] GetImageFilesAsCollection(string file)
        {
            string fileNameNoExt = Path.GetFileName(file);

            string folderPath = ImageFolderInTempFolder(file);

            string[] files = Directory.GetFiles(folderPath);

            return files;

        }

        static public string CreateJSONFileFromTemplate(string file)
        {
            string newOutputFolder = ImageFolderInTempFolder(file);
            var jsonTemplate = Environment.CurrentDirectory + @"\json_Example.json";
            string fileNameNoExt = GetImageFileNameWithNoExt(file);
            var newJsonFileName = newOutputFolder + @"\" + fileNameNoExt + ".json";
            File.Copy(jsonTemplate, newJsonFileName, true);
            WriteToLogFile("JSON created here: [" + newJsonFileName + "]");
            return newJsonFileName;
        }



        static public void UpdateJSONFile(string jsonFile, string find, string replace)
        {
            string fileContent = File.ReadAllText(jsonFile);
            string updatedFileContent = fileContent.Replace(find, replace);
            File.WriteAllText(jsonFile, updatedFileContent);

        }

        static public string textToFindInJSON(string file)
        {

            string find;
            if (file.Contains("Blind"))
            {
                find = "insert blind url here";
            }
            else if (file.Contains("Burning"))
            {
                find = "insert burning url here";
            }
            else if (file.Contains("Charmed"))
            {
                find = "insert charmed url here";
            }
            else if (file.Contains("Defeated"))
            {
                find = "insert defeated url here";
            }
            else if (file.Contains("Frozen"))
            {
                find = "insert frozen url here";
            }
            else if (file.Contains("Poisoned"))
            {
                find = "insert poisoned url here";
            }
            else if (file.Contains("Silenced"))
            {
                find = "insert silenced url here";
            }
            else if (file.Contains("Stunned"))
            {
                find = "insert stunned url here";
            }
            else
            {
                find = "insert base url here";
            }
            return find;
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
