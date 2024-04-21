using System.Collections.Generic;

namespace LauncherGUI.Helpers
{
    public class LanguagePacksHelper
    {
        public List<string> URLs { get; set; }
        public string MD5 { get; set; }
        public string RegistrySelectedLanguageName { get; set; }
        public string RegistrySelectedLanguage { get; set; }
        public string RegistrySelectedLocale { get; set; }
        public string LanguagePackName { get; set; }
    }
}