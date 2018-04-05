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
            try {

                var startInfo = new ProcessStartInfo
                {
                    WorkingDirectory = @"C:\Users\Sean\Desktop\repos\CreateURealmsTiles\CreateURealmsTiles",
                    WindowStyle = ProcessWindowStyle.Normal,
                    FileName = @"C:\Program Files\GIMP 2\bin\gimp-2.8.exe",
                    RedirectStandardInput = true,
                    UseShellExecute = false,
                    Arguments = "gimp --verbose -idf --batch-interpreter=python-fu-eval -b \"import sys; sys.path =['.'] + sys.path; import batch_CreateURealmsTileImages; batch_CreateURealmsTileImages.run('" + file + "')\" -b \"pdb.gimp_quit(1)\""
                };

                Process.Start(startInfo);

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            Console.ReadKey();





        }

        
    }
}
