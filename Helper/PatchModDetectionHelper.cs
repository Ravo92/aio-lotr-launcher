using Serilog.Core;
using Serilog;

namespace Helper
{
    public class PatchModDetectionHelper
    {
        public static List<string> AllPatchesAndMods
        {
            get;
            set;
        }

        readonly static string GameInstallPath = RegistryService.GameInstallPath();
        readonly static string GameAppdataFolderPath = RegistryService.GameAppdataFolderPath();

        static PatchModDetectionHelper()
        {
            AllPatchesAndMods = new();

            if (GameInstallPath != ConstStrings.C_REGISTRY_SERVICE_NOT_FOUND)
            {
                DirectoryInfo _directoryInfo = new(GameInstallPath);

                foreach (var file in _directoryInfo.GetFiles("_patch*.big"))
                {
                    AllPatchesAndMods.Add(file.Name);
                }
            }

            if (File.Exists(Path.Combine(GameAppdataFolderPath, ConstStrings.C_BFME1_MOD_SHADOW_AND_FLAME_11_FILE)) && MD5Tools.CalculateMD5(Path.Combine(GameAppdataFolderPath, ConstStrings.C_BFME1_MOD_SHADOW_AND_FLAME_11_FILE)) == ConstStrings.C_BFME1_MOD_SHADOW_AND_FLAME_11_FILE)
            {
                AllPatchesAndMods.Add("Shadow and Flame 1.1");
            }
        }

        public static void DeletePatch106()
        {
            try
            {
                File.Delete(Path.Combine(GameInstallPath, ConstStrings.C_PATCH105MAIN_FILENAME));
                File.Delete(Path.Combine(GameInstallPath, ConstStrings.C_PATCH106MAIN_FILENAME));
                File.Delete(Path.Combine(GameInstallPath, ConstStrings.C_PATCH106TEXTURES_FILENAME));
                File.Delete(Path.Combine(GameInstallPath, ConstStrings.C_PATCH106WSMAPS_FILENAME));
                File.Delete(Path.Combine(GameInstallPath, ConstStrings.C_PATCH106APT_FILENAME));
            }
            catch (Exception ex)
            {
                Logger _log = new LoggerConfiguration().MinimumLevel.Error().WriteTo.File(Path.Combine(ConstStrings.C_LOGFOLDER_NAME, ConstStrings.C_LOGFILE_PATCHMODDETECTION_NAME)).CreateLogger();
                _log.Error(ex.ToString());
            }
        }

        public static void DeletePatch222Files()
        {
            File.Delete(Path.Combine(GameInstallPath, ConstStrings.C_MAIN_PATCH_FILE));
            File.Delete(Path.Combine(GameInstallPath, ConstStrings.C_TEXTURES_PATCH_FILE));
            File.Delete(Path.Combine(GameInstallPath, ConstStrings.C_LIBRARIES_PATCH_FILE));
            File.Delete(Path.Combine(GameInstallPath, ConstStrings.C_BASES_PATCH_FILE));
            File.Delete(Path.Combine(GameInstallPath, ConstStrings.C_MAPS_PATCH_FILE));
            File.Delete(Path.Combine(GameInstallPath, ConstStrings.C_MAIN_ASSET_FILE));

            if (File.Exists(Path.Combine(GameInstallPath, ConstStrings.C_OPTIONAL_PATCH_FILE)))
            {
                File.Delete(Path.Combine(GameInstallPath, ConstStrings.C_OPTIONAL_PATCH_FILE));
            }

            if (File.Exists(Path.Combine(GameInstallPath, ConstStrings.C_GERMANLANGUAGE_PATCH_FILE)))
            {
                File.Delete(Path.Combine(GameInstallPath, ConstStrings.C_GERMANLANGUAGE_PATCH_FILE));
            }

            File.Copy(Path.Combine(Application.StartupPath, ConstStrings.C_TOOLFOLDER_NAME, ConstStrings.C_103_ASSET_FILE), Path.Combine(GameInstallPath, ConstStrings.C_MAIN_ASSET_FILE));
        }
    }
}
