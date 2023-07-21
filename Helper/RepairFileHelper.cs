using Serilog.Core;
using Serilog;

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
                Logger _log = new LoggerConfiguration().MinimumLevel.Error().WriteTo.File(Path.Combine(ConstStrings.C_LOGFOLDER_NAME, ConstStrings.C_LOGFILE_REPAIRFILE_NAME)).CreateLogger();
                _log.Error(ex.ToString());
            }
            return false;
        }
    }
}