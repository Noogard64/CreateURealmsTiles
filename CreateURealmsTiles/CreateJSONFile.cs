using System;
using System.IO;

namespace CreateURealmsTiles
{
    class CreateJSONFile
    {
        static public string CreateJSONFileFromTemplate(string file)
        {
            string newOutputFolder = Functions.ImageFolderInTempFolder(file);
            var jsonTemplate = Environment.CurrentDirectory + @"\json_Example.json";
            string fileNameNoExt = Functions.GetImageFileNameWithNoExt(file);
            var newJsonFileName = newOutputFolder + @"\" + fileNameNoExt + ".json";
            File.Copy(jsonTemplate, newJsonFileName, true);
            Logging.WriteToLogFile("JSON created here: [" + newJsonFileName + "]");
            return newJsonFileName;
        }

        static public void UpdateJSONFile(string jsonFile, string find, string replace)
        {
            string fileContent = File.ReadAllText(jsonFile);
            string updatedFileContent = fileContent.Replace(find, replace);
            File.WriteAllText(jsonFile, updatedFileContent);

        }

        static public string GetSearchStringForJSON(string file)
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

    }
}
