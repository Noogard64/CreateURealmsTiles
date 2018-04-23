using System;
using System.Diagnostics;

namespace CreateURealmsTiles
{
    class GIMP
    {
        static public void ExecuteGIMPScript(string gimpLocation, string args)
        {
            try
            {
                var startInfo = new ProcessStartInfo
                {
                    WorkingDirectory = Environment.CurrentDirectory,
                    //WorkingDirectory = @"C:\Users\Sean\Desktop\repos\CreateURealmsTiles\CreateURealmsTiles",
                    WindowStyle = ProcessWindowStyle.Normal,
                    //FileName = @"C:\Program Files\GIMP 2\bin\gimp-2.8.exe",
                    FileName = gimpLocation,
                    RedirectStandardInput = true,
                    UseShellExecute = false,
                    Verb = "runas",
                    Arguments = args
                };
                Logging.WriteToLogFile("#################################");

                Logging.WriteToLogFile("Executing the file below:");
                Logging.WriteToLogFile(gimpLocation);

                Logging.WriteToLogFile("Using the arguments below:");
                Logging.WriteToLogFile(args);

                Logging.WriteToLogFile("From the directory below:");
                Logging.WriteToLogFile(Environment.CurrentDirectory.ToString());

                Logging.WriteToLogFile("#################################");

                Process.Start(startInfo);
            }
            catch (Exception e)
            {
                Logging.WriteToLogFile(e.ToString());
            }

        }

        static public void MakeImages(string gimpLocation, string file)
        {
            string args = "gimp --as-new --verbose --no-interface -idf --batch-interpreter=python-fu-eval -b \"import sys; sys.path =['.'] + sys.path; import batch_CreateURealmsTileImages; batch_CreateURealmsTileImages.run('" + file + "')\" -b \"pdb.gimp_quit(1)\"";
            ExecuteGIMPScript(gimpLocation, args);
        }

    }
}
