namespace Helper
{
    public class PatchPacks
    {
        public int Version { get; set; }
        public string URL { get; set; }
        public string MD5 { get; set; }
        public string FileName { get; set; }
        public string RegistryPathForInstalledProgram { get; set; }
        public string RegistryKeyName { get; set; }
        public string ThirdPartyToolExecutableName { get; set; }
        public bool HasExternalInstaller { get; set; }
        public bool ExternalInstallerHasLaunchAbility { get; set; }

        public Dictionary<string, LanguageFiles> LanguageFiles;
    }
}