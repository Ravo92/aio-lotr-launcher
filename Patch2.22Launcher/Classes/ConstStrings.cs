using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatchLauncher.Classes
{
    public class ConstStrings
    {
        public static string GameLanguage()
        {
            return RegistryFunctions.ReadRegKey("lang");
        }

        public static string GameInstallPath()
        {
            return RegistryFunctions.ReadRegKey("path");
        }
        public static string GameAppdataFolder()
        {
            return RegistryFunctions.ReadRegKey("appData");
        }
        public static string GameAppdataFolderPath()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\" + GameAppdataFolder() + "\\";
        }
        public static string OptionsIniFileName()
        {
            return "Options.ini";
        }
    }
}
