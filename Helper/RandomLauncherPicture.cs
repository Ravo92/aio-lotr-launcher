namespace PatchLauncher.Helper
{
    public class RandomLauncherPicture
    {
        private static readonly List<string> _pictures = new()
        {
                @"Images\bgEnts.png",
                @"Images\bgGate.png",
                @"Images\bgHelms.png",
                @"Images\bgIsengard.png",
                @"Images\bgLorien.png",
                @"Images\bgOlifant.png",
                @"Images\bgThomb.png",
        };

        private static readonly Random _rnd = new();

        public static string GetRandomizedPicture()
        {
            int imageIndex = _rnd.Next(_pictures.Count);
            return _pictures[imageIndex];
        }
    }
}