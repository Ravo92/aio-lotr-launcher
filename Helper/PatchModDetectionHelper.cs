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

        public static List<string> DetectPatches222()
        {
            List<string> allFolders = new();

            foreach (string folder in Directory.GetDirectories(ConstStrings.GameInstallPath()))
            {
                string name = folder.Remove(0, folder.LastIndexOf('\\') + 1);
                if (Equals(name, "Patch222_2.41") || Equals(name, "Patch222_2.43") || Equals(name, "Patch222_2.45") || Equals(name, "Patch222_2.47"))
                {
                    allFolders.Add(name);
                }
            }

            foreach (string folder in allFolders)
            {
                Microsoft.VisualBasic.FileIO.FileSystem.MoveDirectory(Path.Combine(ConstStrings.GameInstallPath() ,folder), Path.Combine(Application.StartupPath, "Patches", folder));
            }

            return allFolders;
        }

        public static bool CopyPatch222(bool activate, string version)
        {
            if (File.Exists(Path.Combine(Application.StartupPath, ConstStrings.C_PATCHFOLDER_NAME, version)) && activate == true)
            {
                File.Copy(Path.Combine(ConstStrings.GameInstallPath(), ConstStrings.C_MAIN_ASSET_FILE), Path.Combine(Application.StartupPath, ConstStrings.C_TOOLFOLDER_NAME, ConstStrings.C_ORIGINAL_ASSET_FILE + version));

                if (version == ConstStrings.C_PATCHZIP29_NAME)
                {
                    var progressHandler = new Progress<ExtractionProgress>(progress =>
                    {
                    });

                    ZIPFileHelper _ZIPFileHelper = new();
                    _ZIPFileHelper.ExtractArchive(Path.Combine(ConstStrings.C_PATCHFOLDER_NAME, ConstStrings.C_PATCHZIP29_NAME), ConstStrings.GameInstallPath(), progressHandler);
                }
                else
                {
                    File.Copy(Path.Combine(Application.StartupPath, ConstStrings.C_PATCHFOLDER_NAME, version, ConstStrings.C_MAIN_PATCH_FILE), Path.Combine(ConstStrings.GameInstallPath(), ConstStrings.C_MAIN_PATCH_FILE));
                    File.Copy(Path.Combine(Application.StartupPath, ConstStrings.C_PATCHFOLDER_NAME, version, ConstStrings.C_TEXTURES_PATCH_FILE), Path.Combine(ConstStrings.GameInstallPath(), ConstStrings.C_TEXTURES_PATCH_FILE));
                    File.Copy(Path.Combine(Application.StartupPath, ConstStrings.C_PATCHFOLDER_NAME, version, ConstStrings.C_LIBRARIES_PATCH_FILE), Path.Combine(ConstStrings.GameInstallPath(), ConstStrings.C_LIBRARIES_PATCH_FILE));
                    File.Copy(Path.Combine(Application.StartupPath, ConstStrings.C_PATCHFOLDER_NAME, version, ConstStrings.C_BASES_PATCH_FILE), Path.Combine(ConstStrings.GameInstallPath(), ConstStrings.C_BASES_PATCH_FILE));
                    File.Copy(Path.Combine(Application.StartupPath, ConstStrings.C_PATCHFOLDER_NAME, version, ConstStrings.C_MAPS_PATCH_FILE), Path.Combine(ConstStrings.GameInstallPath(), ConstStrings.C_MAPS_PATCH_FILE));
                    File.Copy(Path.Combine(Application.StartupPath, ConstStrings.C_PATCHFOLDER_NAME, version, ConstStrings.C_ASSET_PATCH_FILE), Path.Combine(ConstStrings.GameInstallPath(), ConstStrings.C_MAIN_ASSET_FILE));
                }
                return true;
            }
            else if (File.Exists(Path.Combine(ConstStrings.GameInstallPath(), ConstStrings.C_PATCH106MAIN_FILENAME)) && activate == false)
            {
                File.Delete(Path.Combine(ConstStrings.GameInstallPath(), ConstStrings.C_MAIN_PATCH_FILE));
                File.Delete(Path.Combine(ConstStrings.GameInstallPath(), ConstStrings.C_TEXTURES_PATCH_FILE));
                File.Delete(Path.Combine(ConstStrings.GameInstallPath(), ConstStrings.C_LIBRARIES_PATCH_FILE));
                File.Delete(Path.Combine(ConstStrings.GameInstallPath(), ConstStrings.C_BASES_PATCH_FILE));
                File.Delete(Path.Combine(ConstStrings.GameInstallPath(), ConstStrings.C_MAPS_PATCH_FILE));
                File.Delete(Path.Combine(ConstStrings.GameInstallPath(), ConstStrings.C_MAIN_ASSET_FILE));

                File.Copy(Path.Combine(Application.StartupPath, ConstStrings.C_TOOLFOLDER_NAME, ConstStrings.C_103_ASSET_FILE), Path.Combine(ConstStrings.GameInstallPath(), ConstStrings.C_MAIN_ASSET_FILE));
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
