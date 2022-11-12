using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace PatchLauncher.Classes
{
    public static class RandomLauncherPicture
    {
        public static string RandomizePicture()
        {
            List<string> _pictures = new()
            {
                @"Images\bgEnts.png",
                @"Images\bgGate.png",
                @"Images\bgHelms.png",
                @"Images\bgIsengard.png",
                @"Images\bgLorien.png",
                @"Images\bgOlifant.png",
                @"Images\bgThomb.png",
                @"Images\bgMap.png"
            };

            Random rnd = new();
            int bgPicture = rnd.Next(_pictures.Count);

            return _pictures[bgPicture];
        }
    }
}