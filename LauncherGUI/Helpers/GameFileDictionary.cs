using System.Collections.Generic;

namespace LauncherGUI.Helpers
{
    public class GameFileDictionary
    {
        public Dictionary<string, MainPacksHelper> MainPacks { get; set; }
        public Dictionary<string, PatchPacksHelper[]> PatchPacks { get; set; }
        public Dictionary<string, LanguagePacksHelper[]> LanguagePacks { get; set; }
    }
}