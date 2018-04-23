using System;
using System.IO;
using System.Diagnostics;

namespace CreateURealmsTiles
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {

            //Create Temp directory: 
            Directory.CreateDirectory(Variables.GetTempFolder());

            //Create fresh Log File:
            Logging.CreateLogFile();

            //Does GIMP exist in the default directory?
            string gimpExeFile = Setup.GetGimpExeFile();
            Logging.WriteToLogFile("GIMP path is [" + gimpExeFile + "]");


            //Get the name of the file
            string file = Setup.GetImageFile();
            Logging.WriteToLogFile("File is located here: [" + file + "]");
            //#################################################################3

            //Execute GIMP-Python
            GIMP.MakeImages(gimpExeFile, file);
            Console.WriteLine("When GIMP has finished processing the image, press any key to continue.");
            Console.ReadKey();


            //Get Files as a collection
            string[] files = Functions.GetImageFilesAsCollection(file);

            //Create new JSON file
            string newJsonFileName = CreateJSONFile.CreateJSONFileFromTemplate(file);

            foreach (string image in files)
            {
                if (image.Contains("json") == false)
                {
                    Logging.WriteToLogFile("#################################");
                    Logging.WriteToLogFile("Uploading [" + image + "] to Imgur.");
                    var replace = UploadImage.UploadImageToImgur(image);

                    var find = CreateJSONFile.GetSearchStringForJSON(image);

                    Logging.WriteToLogFile("Adding [" + image + "] to JSON file.");
                    CreateJSONFile.UpdateJSONFile(newJsonFileName, find, replace);

                    Logging.WriteToLogFile("[" + image + "] finished.");

                }
            }



            Logging.WriteToLogFile("Finished! Press any key to close this dialog and open the output folder.");
            Console.ReadKey();

            string folderPath = Variables.GetTempFolder();
            Process.Start(folderPath);



        }

        
    }
}
