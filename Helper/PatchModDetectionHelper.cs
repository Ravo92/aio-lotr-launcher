using System.ComponentModel;

namespace Helper
{
    public class PatchModDetectionHelper
    {
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


            // THIS SECTION IS FOR PATCH 2.22 V30 AND UPWARDS FILES

            File.Delete(Path.Combine(ConstStrings.GameInstallPath(), ConstStrings.C_ENGLISHPATCH_V30_FILE));
            File.Delete(Path.Combine(ConstStrings.GameInstallPath(), ConstStrings.C_MAIN_PATCH_V30_FILE));
            File.Delete(Path.Combine(ConstStrings.GameInstallPath(), ConstStrings.C_LIBRARIES_PATCH_V30_FILE));
            File.Delete(Path.Combine(ConstStrings.GameInstallPath(), ConstStrings.C_TEXTURES_PATCH_V30_FILE));
            File.Delete(Path.Combine(ConstStrings.GameInstallPath(), ConstStrings.C_BASES_PATCH_V30_FILE));
            File.Delete(Path.Combine(ConstStrings.GameInstallPath(), ConstStrings.C_MAPS_PATCH_V30_FILE));

            if (File.Exists(Path.Combine(ConstStrings.GameInstallPath(), ConstStrings.C_OPTIONAL_PATCH_FILE)))
            {
                File.Delete(Path.Combine(ConstStrings.GameInstallPath(), ConstStrings.C_OPTIONAL_PATCH_FILE));
            }

            File.Copy(Path.Combine(Application.StartupPath, ConstStrings.C_TOOLFOLDER_NAME, ConstStrings.C_103_ASSET_FILE), Path.Combine(ConstStrings.GameInstallPath(), ConstStrings.C_MAIN_ASSET_FILE));
        }
    }
}
