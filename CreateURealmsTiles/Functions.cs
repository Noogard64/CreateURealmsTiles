using System;
using System.IO;
using System.Linq;
using System.Diagnostics;

namespace CreateURealmsTiles
{
    class Functions
    {




        

       
        

        

        static public string[] GetImageFilesAsCollection(string file)
        {
            string fileNameNoExt = Path.GetFileName(file);
            string folderPath = ImageFolderInTempFolder(file);
            string[] files = Directory.GetFiles(folderPath);
            return files;
        }

 

        static public string ImageFolderInTempFolder(string file)
        {
            string fileNameNoExt = Path.GetFileName(file);
            string folder = Variables.GetTempFolder();
            return folder + "\\" + fileNameNoExt.Replace(".png", "");
        }

        static public string GetImageFileNameWithNoExt(string file)
        {
            return Path.GetFileName(file).Replace(".png","");
        }

        
    }
}
