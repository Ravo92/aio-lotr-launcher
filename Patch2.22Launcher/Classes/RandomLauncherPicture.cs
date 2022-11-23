using System;
using System.Collections.Generic;

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
            };

            Random rnd = new();
            int bgPicture = rnd.Next(_pictures.Count);

            return _pictures[bgPicture];
        }
    }
}