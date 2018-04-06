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

            //Does GIMP exist in the default directory?
            string gimpExePath = Functions.GetGimpExePath();

            //Get the name of the file
            string file = Functions.GetFile();

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
                    Console.WriteLine("#################################");
                    Console.WriteLine("Uploading [" + image + "] to Imgur.");
                    var replace = UploadImage.UploadImageToImgur(image);

                    var find = Functions.textToFindInJSON(image);

                    Console.WriteLine("Adding [" + image + "] to JSON file.");
                    Functions.UpdateJSONFile(newJsonFileName, find, replace);

                    Console.WriteLine("[" + image + "] finished.");

                }
            }

            

            Console.WriteLine("Finished! Press any key to close this dialogand open the output folder.");
            Console.ReadKey();

            string folderPath = Functions.GetTempFolder();
            Process.Start(folderPath);



        }

        
    }
}
