namespace Helper
{
    public class PatchModDetectionHelper
    {
        public static Task DeletePatch106ForBFME1(string assemblyName)
        {
            try
            {
                string get106LanguageFileForDelete = "";

                File.Delete(Path.Combine(RegistryService.GameInstallPath(assemblyName), ConstStrings.C_PATCH105MAIN_FILENAME));
                File.Delete(Path.Combine(RegistryService.GameInstallPath(assemblyName), ConstStrings.C_PATCH106MAIN_FILENAME));
                File.Delete(Path.Combine(RegistryService.GameInstallPath(assemblyName), ConstStrings.C_PATCH106TEXTURES_FILENAME));
                File.Delete(Path.Combine(RegistryService.GameInstallPath(assemblyName), ConstStrings.C_PATCH106WSMAPS_FILENAME));
                File.Delete(Path.Combine(RegistryService.GameInstallPath(assemblyName), ConstStrings.C_PATCH106APT_FILENAME));

                IEnumerable<string> get106LanguageFile = Directory.EnumerateFiles(RegistryService.GameInstallPath(assemblyName), "*106*.*");

                if (get106LanguageFile.Any())
                {
                    get106LanguageFileForDelete = get106LanguageFile.First();
                    File.Delete(Path.Combine(get106LanguageFileForDelete));
                }
            }
            catch (Exception ex)
            {
                LogHelper.LoggerPatchModDectection.Error(ex, "");
            }

            return Task.CompletedTask;
        }

        public static Task DeletePatch222FilesForBFME1(string assemblyName)
        {
            try
            {
                File.Delete(Path.Combine(RegistryService.GameInstallPath(assemblyName), ConstStrings.C_MAIN_PATCH_FILE));
                File.Delete(Path.Combine(RegistryService.GameInstallPath(assemblyName), ConstStrings.C_TEXTURES_PATCH_FILE));
                File.Delete(Path.Combine(RegistryService.GameInstallPath(assemblyName), ConstStrings.C_LIBRARIES_PATCH_FILE));
                File.Delete(Path.Combine(RegistryService.GameInstallPath(assemblyName), ConstStrings.C_BASES_PATCH_FILE));
                File.Delete(Path.Combine(RegistryService.GameInstallPath(assemblyName), ConstStrings.C_MAPS_PATCH_FILE));
                File.Delete(Path.Combine(RegistryService.GameInstallPath(assemblyName), ConstStrings.C_MAIN_ASSET_FILE));

                if (File.Exists(Path.Combine(RegistryService.GameInstallPath(assemblyName), ConstStrings.C_GERMANLANGUAGE_PATCH_FILE)))
                {
                    File.Delete(Path.Combine(RegistryService.GameInstallPath(assemblyName), ConstStrings.C_GERMANLANGUAGE_PATCH_FILE));
                }

                File.Copy(Path.Combine(Application.StartupPath, ConstStrings.C_TOOLFOLDER_NAME, ConstStrings.C_BFME1_103_ASSET_FILE), Path.Combine(RegistryService.GameInstallPath(assemblyName), ConstStrings.C_MAIN_ASSET_FILE));
            }
            catch (Exception ex)
            {
                LogHelper.LoggerPatchModDectection.Error(ex, "");
            }

            return Task.CompletedTask;
        }

        public static Task MovePatch109FilesForBFME2(string assemblyName, string patchModName, bool removeFromGameDir)
        {
            string launcherDownloadFolderPatchModPath = Path.Combine(Application.StartupPath, ConstStrings.C_DOWNLOADFOLDER_NAME_BFME2, patchModName);
            try
            {


                if (removeFromGameDir)
                {
                    string[] get109PatchFiles = Directory.GetFiles(RegistryService.GameInstallPath(assemblyName), "*BT2DC*.*");
                    if (get109PatchFiles.Any())
                    {
                        foreach (var file in get109PatchFiles)
                        {
                            File.Move(Path.Combine(file), Path.Combine(launcherDownloadFolderPatchModPath, Path.GetFileName(file)), true);
                        }
                    }
                }
                else
                {
                    string[] get109PatchFiles = Directory.GetFiles(launcherDownloadFolderPatchModPath, "*BT2DC*.*");
                    if (get109PatchFiles.Any())
                    {
                        foreach (var file in get109PatchFiles)
                        {
                            File.Move(file, Path.Combine(RegistryService.GameInstallPath(assemblyName), Path.GetFileName(file)), true);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.LoggerPatchModDectection.Error(ex, "");
            }

            return Task.CompletedTask;
        }
    }
}