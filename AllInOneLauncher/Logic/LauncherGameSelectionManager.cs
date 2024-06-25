using System;

namespace AllInOneLauncher.Logic
{
    public class LauncherGameSelectionManager
    {
        private static readonly string BFME1 = "lotrbfme.exe";
        private static readonly string BFME2 = "lotrbfme2.exe";
        private static readonly string Rotwk = "lotrbfme2ep1.exe";

        public enum BfmeGame
        {
            BFME1 = 0,
            BFME2 = 1,
            Rotwk = 2,
            NONE = 3
        }

        public static string GetFileName(BfmeGame game)
        {
            return game switch
            {
                BfmeGame.BFME1 => BFME1,
                BfmeGame.BFME2 => BFME2,
                BfmeGame.Rotwk => Rotwk,
                _ => throw new ArgumentOutOfRangeException(nameof(game), game, null)
            };
        }
    }
}