namespace Helper
{
    public class PatchPacks
    {
        public int Index { get; set; }
        public int MajorVersion { get; set; }
        public int MinorVersion { get; set; }
        public int Revision { get; set; }
        public List<string> URLs { get; set; }
        public string MD5 { get; set; }
        public string FileName { get; set; }
        public string RegistryPathForInstalledProgram { get; set; }
        public string RegistryKeyName { get; set; }
        public string ThirdPartyToolExecutableName { get; set; }
        public bool HasExternalInstaller { get; set; }
        public bool ExternalInstallerHasLaunchAbility { get; set; }
        public bool HasSubPatchOrHotfix { get; set; }

        public Dictionary<string, LanguageFiles> LanguageFiles;
    }
}