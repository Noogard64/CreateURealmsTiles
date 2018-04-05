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
            //string file = @"C:\Users\sean-\Desktop\urealms\Tiles\Regirock.png"; 
            //string file = @"C:\Users\Sean\Desktop\urealms\urealms_tiles\NPC_Dwarf_01.png"; 

            //C:\Users\sean-\Desktop\urealms\Tiles\Regirock.png


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

            string folderPath = file.Replace(".png", "");

            Console.WriteLine("Finished! Press any key to close this dialog. The location of your new JSON file will open.");

            Console.ReadKey();
            Process.Start(folderPath);



        }

        
    }
}
