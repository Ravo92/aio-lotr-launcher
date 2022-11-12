using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatchLauncher.Classes
{
    internal class ThemenIconList
    {
        public static List<string> GetThemeIcons()
        {
            List<string> _pictures = new()
            {
                @"Images\IcoDefault.png",
                @"Images\IcoGondor.png",
                @"Images\IcoRohan.png",
                @"Images\IcoIsengard.png",
                @"Images\IcoMordor.png",
            };
            return _pictures;
        }
    }
}
