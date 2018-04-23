using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateURealmsTiles
{
    class Variables
    {

        static public string GetLogFile()
        {
            return GetTempFolder() + "\\log.txt";
        }

        static public string GetTempFolder()
        {
            return "C:\\Users\\" + Environment.UserName + "\\AppData\\Local\\Temp\\CreateUrealmsTiles";
        }
    }
}
