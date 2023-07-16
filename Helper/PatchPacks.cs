namespace Helper
{
    public class PatchPacks
    {
        public int Version { get; set; }
        public string URL { get; set; }
        public string MD5 { get; set; }
        public string FileName { get; set; }

        public Dictionary<string, LanguageFiles> LanguageFiles;
    }
}