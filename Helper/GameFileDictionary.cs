namespace Helper
{
    public class GameFileDictionary
    {
        public Dictionary<string, MainPacks> MainPacks { get; set; }
        public Dictionary<string, PatchPacks[]> PatchPacks { get; set; }
        public Dictionary<string, PatchPacksBeta> PatchPacksBeta { get; set; }
        public Dictionary<string, LanguagePacks[]> LanguagePacks { get; set; }
    }
}