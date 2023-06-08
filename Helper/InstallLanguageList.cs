namespace Helper
{
    public static class InstallLanguageList
    {
        public static Dictionary<string, LanguageSettings> _DictionarylanguageSettings = new()
        {
            { "en_us", new LanguageSettings { RegistrySelectedLanguageName = "English US", RegistrySelectedLanguage = "English", RegistrySelectedLocale = "en_us", LanguagPackName = "LangPack_EN.7z" } },
            { "fr_fr", new LanguageSettings { RegistrySelectedLanguageName = "French", RegistrySelectedLanguage = "French", RegistrySelectedLocale = "fr_fr", LanguagPackName = "LangPack_FR.7z" } },
            { "de", new LanguageSettings { RegistrySelectedLanguageName = "German", RegistrySelectedLanguage = "German", RegistrySelectedLocale = "de", LanguagPackName = "LangPack_DE.7z" } },
            { "it", new LanguageSettings { RegistrySelectedLanguageName = "Italian", RegistrySelectedLanguage = "Italian", RegistrySelectedLocale = "it", LanguagPackName = "LangPack_IT.7z" } },
            { "es", new LanguageSettings { RegistrySelectedLanguageName = "Spanish", RegistrySelectedLanguage = "Spanish", RegistrySelectedLocale = "es", LanguagPackName = "LangPack_ES.7z" } },
            { "sv", new LanguageSettings { RegistrySelectedLanguageName = "Swedish", RegistrySelectedLanguage = "Swedish", RegistrySelectedLocale = "sv", LanguagPackName = "LangPack_SV.7z" } },
            { "nl", new LanguageSettings { RegistrySelectedLanguageName = "Dutch", RegistrySelectedLanguage = "Dutch", RegistrySelectedLocale = "nl", LanguagPackName = "LangPack_NL.7z" } },
            { "pl", new LanguageSettings { RegistrySelectedLanguageName = "Polish", RegistrySelectedLanguage = "Polish", RegistrySelectedLocale = "pl", LanguagPackName = "LangPack_PL.7z" } },
            { "no", new LanguageSettings { RegistrySelectedLanguageName = "Norwegian", RegistrySelectedLanguage = "Norwegian", RegistrySelectedLocale = "no", LanguagPackName = "LangPack_NO.7z" } },
        };
    }
}