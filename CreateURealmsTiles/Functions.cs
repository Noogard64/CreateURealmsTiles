using System;
using System.IO;

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
            do
            {
                Console.WriteLine("Input filepath of image:");
                file = Console.ReadLine();
                if (File.Exists(file) == false)
                {
                    Console.WriteLine("Image not found. Try again.");
                    file = Console.ReadLine();

                }
                else
                {
                    fileFound = true;
                }


            } while (fileFound == false);
            return file;
        }
    }
}
