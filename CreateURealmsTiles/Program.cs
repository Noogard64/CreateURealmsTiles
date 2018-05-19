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


            //Making JPG and JPEG work
            file = file.Replace(".jpeg", ".png");
            file = file.Replace(".jpg", ".png");

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



            //Functions.WriteToLogFile("Finished! Press any key to close this dialog and open the output folder.");
            //Console.ReadKey();


            var JsonfileName = Path.GetFileName(newJsonFileName);

            var CustomTilesFileLocation = @"C:\Users\" + Environment.UserName + @"\Documents\My Games\Tabletop Simulator\Saves\Saved Objects\CustomTiles\";


            var jsonFileNameAndNewDestination = CustomTilesFileLocation + @"\" + JsonfileName;

            //TODO Create file path and file name for new PNG file location. This image is effectively a thumb nail in TTS
            //var pngFileNameAndNewDestination = CustomTilesFileLocation + @"\" + ;


            //TODO Get the filename and path for the thumbnail PNG
            //var pngFileNameAndPath

            File.Copy(newJsonFileName, jsonFileNameAndNewDestination);

            //TODO Copy the thumbnail image to the CustomTilesFileLocation
            //File.Copy(, pngFileNameAndNewDestination);
            Functions.WriteToLogFile("Finished!");
            Console.ReadKey();

            //string folderPath = Functions.GetTempFolder();
            //Process.Start(folderPath);



        }


    }
}
