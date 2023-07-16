namespace Helper
{
    public static class JSONDataListHelper
    {
        public static MainPacks _MainPackSettings = new();
        public static PatchPacksBeta _PatchBetaSettings = new();

        public static Dictionary<int, PatchPacks> _DictionaryPatchPacksSettings = new();
        public static Dictionary<string, LanguagePacks> _DictionarylanguageSettings = new();
    }
}