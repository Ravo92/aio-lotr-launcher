namespace Helper
{
    public class RepairFileHelper
    {
        public static Task RepairFeature(string assemblyName)
        {
            try
            {
                if (Directory.Exists(RegistryService.GameInstallPath(assemblyName)))
                {
                    Directory.Delete(RegistryService.GameInstallPath(assemblyName), true);
                    LogHelper.LoggerGRepairFile.Information(string.Format("Deleted Folder: {0}", RegistryService.GameInstallPath(assemblyName)));
                }
                return Task.CompletedTask;
            }
            catch (Exception ex)
            {
                LogHelper.LoggerGRepairFile.Error(ex.ToString());
                return Task.FromException(ex);
            }
        }

        //TODO: Add Every file from the game to a json with name, relative path from <gameroot> and with md5 checksum to evaluade

        //public static async Task<bool> RepairFeature(bool EAXSupport, int LatestPatchVersion, string InstalledLanguageISOCode)
        //{
        //    PatchPacks _patchpack222 = JSONDataListHelper._DictionaryPatchPacksSettings[LatestPatchVersion];
        //    LanguageFiles patchPackLanguages = _patchpack222.LanguageFiles[InstalledLanguageISOCode];
        //
        //    string gameInstallPath = RegistryService.GameInstallPath();
        //
        //    try
        //    {
        //        bool FlagIsCorrupt = false;
        //
        //        foreach (var fileName in RepairFileHelper._DictionaryRepairFileList)
        //        {
        //            string isMD5Value = MD5Tools.CalculateMD5(Path.Combine(gameInstallPath, fileName.Key));
        //            string shouldMD5Value = fileName.Value;
        //            if (isMD5Value == shouldMD5Value)
        //            {
        //                LogHelper.LoggerGRepairFile.Information(string.Format("File {0} has the correct value: {1}", fileName.Key, shouldMD5Value));
        //            }
        //            else
        //            {
        //                LogHelper.LoggerGRepairFile.Information(string.Format("File {0} ist corrupted and will be reaquired... Value: {1}", fileName.Key, isMD5Value));
        //                FlagIsCorrupt = true;
        //                break;
        //            }
        //        }
        //        if (FlagIsCorrupt)
        //        {
        //            if (Directory.Exists(gameInstallPath))
        //            {
        //                Directory.Delete(gameInstallPath, true);
        //                if (!Directory.Exists(RegistryService.GameInstallPath()))
        //                {
        //                    LogHelper.LoggerGRepairFile.Information(string.Format("Deleted the game folder \"{0}\" sucessfully", gameInstallPath));
        //                }
        //                else
        //                {
        //                    LogHelper.LoggerGRepairFile.Error(string.Format("Was not able to delete the folder \"{0}\".", gameInstallPath));
        //                }
        //            }
        //
        //            if (File.Exists(Path.Combine(Application.StartupPath, ConstStrings.C_DOWNLOADFOLDER_NAME, ConstStrings.C_MAINGAMEFILE_ZIP)))
        //            {
        //                LogHelper.LoggerGRepairFile.Information(string.Format("Checking file \"{0}\"...", ConstStrings.C_MAINGAMEFILE_ZIP));
        //                if (MD5Tools.CalculateMD5(Path.Combine(Application.StartupPath, ConstStrings.C_DOWNLOADFOLDER_NAME, ConstStrings.C_MAINGAMEFILE_ZIP)) != ConstStrings.C_MAINGAMEFILE_ZIP_MD5_HASH)
        //                {
        //                    File.Delete(Path.Combine(Application.StartupPath, ConstStrings.C_DOWNLOADFOLDER_NAME, ConstStrings.C_MAINGAMEFILE_ZIP));
        //                    LogHelper.LoggerGRepairFile.Warning(string.Format("The file \"{0}\" was corrupted and will be reaquired...", ConstStrings.C_MAINGAMEFILE_ZIP));
        //                }
        //                LogHelper.LoggerGRepairFile.Information(string.Format("File \"{0}\" is okay. No action needed.", ConstStrings.C_MAINGAMEFILE_ZIP));
        //            }
        //            if (File.Exists(Path.Combine(Application.StartupPath, ConstStrings.C_DOWNLOADFOLDER_NAME, _languageSettings.LanguagPackName)))
        //            {
        //                LogHelper.LoggerGRepairFile.Information(string.Format("Checking file \"{0}\"...", _languageSettings.LanguagPackName));
        //                if (MD5Tools.CalculateMD5(Path.Combine(Application.StartupPath, ConstStrings.C_DOWNLOADFOLDER_NAME, _languageSettings.LanguagPackName)) != XMLFileHelper.GetXMLGameLanguageMD5Hash(_languageSettings.RegistrySelectedLocale))
        //                {
        //                    File.Delete(Path.Combine(Application.StartupPath, ConstStrings.C_DOWNLOADFOLDER_NAME, ConstStrings.C_MAINGAMEFILE_ZIP));
        //                    LogHelper.LoggerGRepairFile.Warning(string.Format("The file \"{0}\" was corrupted and will be reaquired...", _languageSettings.LanguagPackName));
        //                }
        //                LogHelper.LoggerGRepairFile.Information(string.Format("File \"{0}\" is okay. No action needed.", _languageSettings.LanguagPackName));
        //            }
        //            LogHelper.LoggerGRepairFile.Information("Check for EAX-Support...");
        //            if (EAXSupport)
        //            {
        //                LogHelper.LoggerGRepairFile.Information("EAX-Support is active... Installing neccessary files...");
        //                List<string> _EAXFiles = new() { "dsoal-aldrv.dll", "dsound.dll", "dsound.ini", };
        //                foreach (var file in _EAXFiles)
        //                {
        //                    LogHelper.LoggerGRepairFile.Information("Installed file: " + file);
        //                    File.Copy(Path.Combine(ConstStrings.C_TOOLFOLDER_NAME, file), Path.Combine(gameInstallPath, file), true);
        //                }
        //            }
        //            else
        //            {
        //                LogHelper.LoggerGRepairFile.Information("EAX support disabled via launcher... no action needed.");
        //            }
        //            LogHelper.LoggerGRepairFile.Information("We are now renewing every file...");
        //            await WinFormsMainGUI.InstallUpdatRepairRoutine();
        //        }
        //        else
        //        {
        //            LogHelper.LoggerGRepairFile.Information(string.Format("Detected game language: \"{0}\"", _languageSettings.RegistrySelectedLanguage));
        //            LogHelper.LoggerGRepairFile.Information(string.Format("Reinstalling installed language: \"{0}\" ...", _languageSettings.RegistrySelectedLanguage));
        //            if (File.Exists(Path.Combine(Application.StartupPath, ConstStrings.C_DOWNLOADFOLDER_NAME, _languageSettings.LanguagPackName)))
        //            {
        //                LogHelper.LoggerGRepairFile.Information(string.Format("Checking file \"{0}\"...", _languageSettings.LanguagPackName));
        //                if (MD5Tools.CalculateMD5(Path.Combine(Application.StartupPath, ConstStrings.C_DOWNLOADFOLDER_NAME, _languageSettings.LanguagPackName)) != XMLFileHelper.GetXMLGameLanguageMD5Hash(_languageSettings.RegistrySelectedLocale))
        //                {
        //                    File.Delete(Path.Combine(Application.StartupPath, ConstStrings.C_DOWNLOADFOLDER_NAME, ConstStrings.C_MAINGAMEFILE_ZIP));
        //                    LogHelper.LoggerGRepairFile.Warning(string.Format("The file \"{0}\" was corrupted and will be reaquired...", _languageSettings.LanguagPackName));
        //                }
        //                LogHelper.LoggerGRepairFile.Information(string.Format("File \"{0}\" is okay. No action needed.", _languageSettings.LanguagPackName));
        //            }
        //            LogHelper.LoggerGRepairFile.Information("Check for EAX-Support...");
        //            if (EAXSupport)
        //            {
        //                LogHelper.LoggerGRepairFile.Information("EAX-Support is active... Installing neccessary files...");
        //                List<string> _EAXFiles = new() { "dsoal-aldrv.dll", "dsound.dll", "dsound.ini", };
        //                foreach (var file in _EAXFiles)
        //                {
        //                    LogHelper.LoggerGRepairFile.Information("Installed file: " + file);
        //                    File.Copy(Path.Combine(ConstStrings.C_TOOLFOLDER_NAME, file), Path.Combine(gameInstallPath, file), true);
        //                }
        //            }
        //            else
        //            {
        //                LogHelper.LoggerGRepairFile.Information("EAX support disabled via launcher... no action needed.");
        //            }
        //            await InstallRoutine();
        //        }
        //        if (InstalledLanguageISOCode == "de")
        //        {
        //            File.Copy(Path.Combine(ConstStrings.C_TOOLFOLDER_NAME, ConstStrings.C_GERMANLANGUAGE_PATCH_FILE), Path.Combine(gameInstallPath, ConstStrings.C_GERMANLANGUAGE_PATCH_FILE), true);
        //            LogHelper.LoggerGRepairFile.Information(string.Format("Copied German translation for 2.22 \"{0}\" into \"{1}\"", ConstStrings.C_GERMANLANGUAGE_PATCH_FILE, gameInstallPath));
        //        }
        //        //MessageBox.Show(Strings.Msg_RepairDone_Text, Strings.Msg_RepairDone_Title, MessageBoxButtons.OK);
        //    }
        //    catch (Exception ex)
        //    {
        //        LogHelper.LoggerGRepairFile.Error(ex.ToString());
        //    }
        //    return false;
        //}
    }
}