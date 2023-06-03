namespace PatchLauncher
{
    static class GameLanguageHelper
    {
        private static string? registrySelectedLanguageName;
        private static string? registrySelectedLanguage;
        private static string? registrySelectedLocale;
        private static string? languagPackName;

        public static string RegistrySelectedLanguageName
        {
            get { return registrySelectedLanguageName!; }
            set { registrySelectedLanguageName = value; }
        }

        public static string RegistrySelectedLanguage
        {
            get { return registrySelectedLanguage!; }
            set { registrySelectedLanguage = value; }
        }

        public static string RegistrySelectedLocale
        {
            get { return registrySelectedLocale!; }
            set { registrySelectedLocale = value; }
        }

        public static string LanguagPackName
        {
            get { return languagPackName!; }
            set { languagPackName = value; }
        }
    }
}