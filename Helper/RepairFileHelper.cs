namespace Helper
{
    public class RepairFileHelper
    {
        public static async Task<bool> RepairFeature(bool EAXSupport, int LatestPatchVersion, string InstalledLanguageISOCode)
        {
            PatchPacks _patchpack222 = JSONDataListHelper._DictionaryPatchPacksSettings[LatestPatchVersion];
            LanguageFiles patchPackLanguages = _patchpack222.LanguageFiles[InstalledLanguageISOCode];

            try
            {
                return true;
            }
            catch (Exception ex)
            {
                using StreamWriter file = new(Path.Combine(ConstStrings.C_LOGFOLDER_NAME, ConstStrings.C_ERRORLOGGING_FILE), append: true);
                await file.WriteLineAsync(ConstStrings.LogTime + ex.ToString());
            }
            return false;
        }
    }
}
