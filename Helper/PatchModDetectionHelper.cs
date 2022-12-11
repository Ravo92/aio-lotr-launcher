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

        public static bool CopyPatch106(bool activate)
        {
            if (File.Exists(Path.Combine(Application.StartupPath, ConstStrings.C_TOOLFOLDER_NAME, ConstStrings.C_PATCH106MAIN_FILENAME)) && activate == true)
            {
                File.Copy(Path.Combine(Application.StartupPath, ConstStrings.C_TOOLFOLDER_NAME, ConstStrings.C_PATCH105MAIN_FILENAME), Path.Combine(ConstStrings.GameInstallPath(), ConstStrings.C_PATCH105MAIN_FILENAME));
                File.Copy(Path.Combine(Application.StartupPath, ConstStrings.C_TOOLFOLDER_NAME, ConstStrings.C_PATCH106MAIN_FILENAME), Path.Combine(ConstStrings.GameInstallPath(), ConstStrings.C_PATCH106MAIN_FILENAME));
                File.Copy(Path.Combine(Application.StartupPath, ConstStrings.C_TOOLFOLDER_NAME, ConstStrings.C_PATCH106TEXTURES_FILENAME), Path.Combine(ConstStrings.GameInstallPath(), ConstStrings.C_PATCH106TEXTURES_FILENAME));
                return true;
            }
            else if (File.Exists(Path.Combine(ConstStrings.GameInstallPath(), ConstStrings.C_PATCH106MAIN_FILENAME)) && activate == false)
            {
                File.Delete(Path.Combine(ConstStrings.GameInstallPath(), ConstStrings.C_PATCH105MAIN_FILENAME));
                File.Delete(Path.Combine(ConstStrings.GameInstallPath(), ConstStrings.C_PATCH106MAIN_FILENAME));
                File.Delete(Path.Combine(ConstStrings.GameInstallPath(), ConstStrings.C_PATCH106TEXTURES_FILENAME));
                return true;
            }
            else
            {
                return false;
            }
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
