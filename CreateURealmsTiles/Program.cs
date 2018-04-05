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
            string file = @"C:\Users\Sean\Desktop\urealms\urealms_tiles\NPC_Dwarf_01.png"; //Functions.GetFile();

            //Execute GIMP-Python
            Functions.MakeImages(gimpExePath, file);

            //Get Files as a collection
            string[] files = Functions.GetImageFilesAsCollection(file);

            //Create new JSON file
            string newJsonFileName = Functions.CreateJSONFileFromTemplate(file);

            foreach (string image in files)
            {

                var replace = UploadImage.UploadImageToImgur(file);

                var find = Functions.textToFindInJSON(file);
                Console.WriteLine(find);

                Functions.UpdateJSONFile(newJsonFileName, find, replace);
            }


            Console.WriteLine("Finished!");
            Console.ReadKey();




        }

        
    }
}
