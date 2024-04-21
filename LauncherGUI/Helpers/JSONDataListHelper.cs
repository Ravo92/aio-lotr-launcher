using System.Collections.Generic;

namespace LauncherGUI.Helpers
{
    internal static class JSONDataListHelper
    {
        internal static MainPacksHelper _MainPackSettings = new();

        internal static Dictionary<int, PatchPacksHelper> _DictionaryPatchPacksSettings = new();
        internal static Dictionary<string, LanguagePacksHelper> _DictionarylanguageSettings = new();
    }
}