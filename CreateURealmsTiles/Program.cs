using System;
using System.IO;
using System.Diagnostics;

namespace CreateURealmsTiles
{
    class Program
    {
        static void Main(string[] args)
        {
            //Setup
            Functions.CreateTempFolder();
            Functions.CreateLogFile();


            //Does GIMP exist in the default directory?
            string gimpExePath = Functions.GetGimpExePath();
            Functions.WriteToLogFile("GIMP path is [" + gimpExePath + "]");


            //Get the name of the file
            string file = Functions.GetFile();
            Functions.WriteToLogFile("File is located here: [" + file + "]");

            //Execute GIMP-Python
            Functions.MakeImages(gimpExePath, file);
            Console.WriteLine("When GIMP has finished processing the image, press any key to continue.");
            Console.ReadKey();


            //Get Files as a collection
            string[] files = Functions.GetImageFilesAsCollection(file);

            //Create new JSON file
            string newJsonFileName = Functions.CreateJSONFileFromTemplate(file);

            foreach (string image in files)
            {
                if (image.Contains("json") == false)
                {
                    Functions.WriteToLogFile("#################################");
                    Functions.WriteToLogFile("Uploading [" + image + "] to Imgur.");
                    var replace = UploadImage.UploadImageToImgur(image);

                    var find = Functions.textToFindInJSON(image);

                    Functions.WriteToLogFile("Adding [" + image + "] to JSON file.");
                    Functions.UpdateJSONFile(newJsonFileName, find, replace);

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
