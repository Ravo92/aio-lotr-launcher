using System;
using System.Collections.Generic;
using System.Drawing;

namespace AllInOneLauncher.Logic
{
    public static class SystemDisplayManager
    {
        public static List<string> GetAllSupportedResolutions()
        {
            List<string> allResolutions = [];

            DEVMODE vDevMode = new();
            int i = 0;
            while (NativeMethods.EnumDisplaySettings(null, i, ref vDevMode))
            {
                if (vDevMode.dmDisplayFrequency == 60 && vDevMode.dmBitsPerPel == 32 && vDevMode.dmDisplayFixedOutput == 0)
                    allResolutions.Add(vDevMode.dmPelsWidth.ToString() + " " + vDevMode.dmPelsHeight.ToString());

                i++;
            }

            allResolutions.RemoveRange(0, Math.Min(3, allResolutions.Count));
            allResolutions[^1] = allResolutions[^1];

            return allResolutions;
        }

        public static Size GetPrimaryScreenResolution() => new Size(Convert.ToInt32(System.Windows.SystemParameters.PrimaryScreenWidth), Convert.ToInt32(System.Windows.SystemParameters.PrimaryScreenHeight));
    }
}