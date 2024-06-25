using System;

namespace AllInOneLauncher.Logic
{
    internal class LauncherGameSelectionManager
    {
        internal static readonly string BFME1 = "lotrbfme.exe";
        internal static readonly string BFME2 = "lotrbfme2.exe";
        internal static readonly string ROTWK = "lotrbfme2ep1.exe";

        internal enum AvailableBFMEGames
        {
            BFME1 = 0,
            BFME2 = 1,
            ROTWK = 2,
            NONE = 3
        }

        public static string GetFileName(AvailableBFMEGames availableBFMEGames)
        {
            return availableBFMEGames switch
            {
                AvailableBFMEGames.BFME1 => BFME1,
                AvailableBFMEGames.BFME2 => BFME2,
                AvailableBFMEGames.ROTWK => ROTWK,
                _ => throw new ArgumentOutOfRangeException(nameof(availableBFMEGames), availableBFMEGames, null)
            };
        }
    }
}