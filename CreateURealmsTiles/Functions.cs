using System;
using System.IO;
using System.Linq;
using System.Diagnostics;

namespace CreateURealmsTiles
{
    class Functions
    {

        static public string GetGimpExePath()
        {
            string gimpLocation = @"C:\Program Files\GIMP 2\bin\gimp-2.8.exe";

            bool gimpFound = false;

            do
            {
                if (File.Exists(gimpLocation) == false)
                {
                    Console.WriteLine("'gimp-2.8.exe' not found in default location. Please enter location.");
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

            Console.WriteLine("Input filepath of image:");
            do
            {
                
                file = Console.ReadLine();
                if (File.Exists(file) == false)
                {
                    Console.WriteLine("Image not found. Try again.");
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
            string folderPath = file.Replace(".png", ""); 

            string[] files = Directory.GetFiles(folderPath);

            return files;

        }

        static public string CreateJSONFileFromTemplate(string file)
        {
            string folderPath = file.Replace(".png", "");
            var jsonTemplate = Environment.CurrentDirectory + @"\json_Example.json";
            String[] newJsonFile = folderPath.Split('\\');
            var newJsonFileName = folderPath + @"\" + newJsonFile.Last() + ".json";
            File.Copy(jsonTemplate, newJsonFileName, true);
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

        static public void MakeImages(string gimpLocation, string file)
        {
            try
            {

                var startInfo = new ProcessStartInfo
                {
                    WorkingDirectory = Environment.CurrentDirectory,
                    //WorkingDirectory = @"C:\Users\Sean\Desktop\repos\CreateURealmsTiles\CreateURealmsTiles",
                    WindowStyle = ProcessWindowStyle.Normal,
                    //FileName = @"C:\Program Files\GIMP 2\bin\gimp-2.8.exe",
                    FileName = gimpLocation,
                    RedirectStandardInput = true,
                    UseShellExecute = false,
                    Arguments = "gimp --as-new --verbose --no-interface -idf --batch-interpreter=python-fu-eval -b \"import sys; sys.path =['.'] + sys.path; import batch_CreateURealmsTileImages; batch_CreateURealmsTileImages.run('" + file + "')\" -b \"pdb.gimp_quit(1)\""
                };

                Process.Start(startInfo);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
