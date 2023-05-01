namespace Helper
{
    public class PatchModDetectionHelper
    {
        public static List<string> AllPatchesAndMods
        {
            get;
            set;
        }

        static PatchModDetectionHelper()
        {
            AllPatchesAndMods = new();

            if (ConstStrings.GameInstallPath() != ConstStrings.C_REGISTRY_SERVICE_NOT_FOUND)
            {
                string filepath = ConstStrings.GameInstallPath();
                DirectoryInfo _directoryInfo = new(filepath);

                foreach (var file in _directoryInfo.GetFiles("_patch*.big"))
                {
                    AllPatchesAndMods.Add(file.Name);
                }
            }

            if (File.Exists(Path.Combine(ConstStrings.GameAppdataFolderPath(), ConstStrings.C_BFME1_MOD_SHADOW_AND_FLAME_11_FILE)) && MD5Tools.CalculateMD5(Path.Combine(ConstStrings.GameAppdataFolderPath(), ConstStrings.C_BFME1_MOD_SHADOW_AND_FLAME_11_FILE)) == ConstStrings.C_BFME1_MOD_SHADOW_AND_FLAME_11_FILE)
            {
                AllPatchesAndMods.Add("Shadow and Flame 1.1");
            }
        }

        public static bool DetectPatch106()
        {
            if (File.Exists(Path.Combine(ConstStrings.GameInstallPath(), ConstStrings.C_PATCH106MAIN_FILENAME)))
            {
                return true;
            }
            return false;
        }

        public static bool DeletePatch106()
        {
            File.Delete(Path.Combine(ConstStrings.GameInstallPath(), ConstStrings.C_PATCH105MAIN_FILENAME));
            File.Delete(Path.Combine(ConstStrings.GameInstallPath(), ConstStrings.C_PATCH106MAIN_FILENAME));
            File.Delete(Path.Combine(ConstStrings.GameInstallPath(), ConstStrings.C_PATCH106TEXTURES_FILENAME));
            File.Delete(Path.Combine(ConstStrings.GameInstallPath(), ConstStrings.C_PATCH106WSMAPS_FILENAME));
            File.Delete(Path.Combine(ConstStrings.GameInstallPath(), ConstStrings.C_PATCH106APT_FILENAME));
            return true;
        }

        public static void DeletePatch222Files()
        {
            File.Delete(Path.Combine(ConstStrings.GameInstallPath(), ConstStrings.C_MAIN_PATCH_FILE));
            File.Delete(Path.Combine(ConstStrings.GameInstallPath(), ConstStrings.C_TEXTURES_PATCH_FILE));
            File.Delete(Path.Combine(ConstStrings.GameInstallPath(), ConstStrings.C_LIBRARIES_PATCH_FILE));
            File.Delete(Path.Combine(ConstStrings.GameInstallPath(), ConstStrings.C_BASES_PATCH_FILE));
            File.Delete(Path.Combine(ConstStrings.GameInstallPath(), ConstStrings.C_MAPS_PATCH_FILE));
            File.Delete(Path.Combine(ConstStrings.GameInstallPath(), ConstStrings.C_MAIN_ASSET_FILE));

            if (File.Exists(Path.Combine(ConstStrings.GameInstallPath(), ConstStrings.C_OPTIONAL_PATCH_FILE)))
            {
                File.Delete(Path.Combine(ConstStrings.GameInstallPath(), ConstStrings.C_OPTIONAL_PATCH_FILE));
            }

            File.Copy(Path.Combine(Application.StartupPath, ConstStrings.C_TOOLFOLDER_NAME, ConstStrings.C_103_ASSET_FILE), Path.Combine(ConstStrings.GameInstallPath(), ConstStrings.C_MAIN_ASSET_FILE));
        }
    }
}
