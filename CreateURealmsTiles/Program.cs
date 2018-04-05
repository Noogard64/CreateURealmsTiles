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

            Console.ReadKey();





        }

        
    }
}
