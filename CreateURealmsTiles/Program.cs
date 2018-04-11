using System;
using System.IO;
using System.Diagnostics;

namespace CreateURealmsTiles
{
    class Program
    {
        static void Main(string[] args)
        {
            //#################################################################3
            //Setup
            Setup.CreateTempFolder();
            Setup.CreateLogFile();

            //Does GIMP exist in the default directory?
            string gimpExeFile = Setup.GetGimpExeFile();
            Functions.WriteToLogFile("GIMP path is [" + gimpExeFile + "]");


            //Get the name of the file
            string file = Setup.GetImageFile();
            Functions.WriteToLogFile("File is located here: [" + file + "]");
            //#################################################################3

            //Execute GIMP-Python
            Functions.MakeImages(gimpExeFile, file);
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
                    Functions.WriteToLogFile("#################################");
                    Functions.WriteToLogFile("Uploading [" + image + "] to Imgur.");
                    var replace = UploadImage.UploadImageToImgur(image);

                    var find = CreateJSONFile.GetSearchStringForJSON(image);

                    Functions.WriteToLogFile("Adding [" + image + "] to JSON file.");
                    CreateJSONFile.UpdateJSONFile(newJsonFileName, find, replace);

                    Functions.WriteToLogFile("[" + image + "] finished.");

                }
            }



            Functions.WriteToLogFile("Finished! Press any key to close this dialog and open the output folder.");
            Console.ReadKey();

            string folderPath = Functions.GetTempFolder();
            Process.Start(folderPath);



        }

        
    }
}
