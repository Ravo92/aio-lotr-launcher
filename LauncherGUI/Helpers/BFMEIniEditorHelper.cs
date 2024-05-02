using System;
using System.IO;
using System.Collections.Generic;
using static LauncherGUI.Helpers.GameSelectorHelper;

namespace LauncherGUI.Helpers
{
    internal class BFMEIniEditorHelper
    {
        public static string ReadKey(string keyName, AvailableBFMEGames assemblyName)
        {
            StreamReader _streamReader = new(Path.Combine(BFMERegistryHelper.GameAppDataFolderPath(assemblyName), ConstStringsHelper.C_OPTIONSINI_FILENAME));
            {
                string keyValue = ConstStringsHelper.C_REGISTRY_SERVICE_WRONG_PARAMETER;
                using (_streamReader)
                {
                    string importedOptionsFileText;
                    while ((importedOptionsFileText = _streamReader.ReadLine()!) != null)
                    {
                        string[] parts = importedOptionsFileText.Split(" = ");
                        if ((parts.Length == 2) && (parts[0] == keyName))
                        {
                            keyValue = parts[1];
                        }
                    }
                }
                return keyValue;
            }
        }

        public static void WriteKey(string keyName, string keyValue, AvailableBFMEGames assemblyName)
        {
            StreamReader _streamReader = new(Path.Combine(BFMERegistryHelper.GameAppDataFolderPath(assemblyName), ConstStringsHelper.C_OPTIONSINI_FILENAME));
            string importedOptionsFileText;
            string changedOptionsFileText;
            importedOptionsFileText = _streamReader.ReadToEnd();
            _streamReader.Close();
            _streamReader.Dispose();

            if (!importedOptionsFileText.Contains(keyName))
            {
                changedOptionsFileText = importedOptionsFileText += (keyName + " = " + keyValue + Environment.NewLine);
            }
            else
            {
                changedOptionsFileText = importedOptionsFileText.Replace(keyName + " = " + ReadKey(keyName, assemblyName), keyName + " = " + keyValue);
            }

            File.WriteAllText(Path.Combine(BFMERegistryHelper.GameAppDataFolderPath(assemblyName), ConstStringsHelper.C_OPTIONSINI_FILENAME), changedOptionsFileText);
        }

        public static void DeleteKey(string keyName, AvailableBFMEGames assemblyName)
        {
            StreamReader _streamReader = new(Path.Combine(BFMERegistryHelper.GameAppDataFolderPath(assemblyName), ConstStringsHelper.C_OPTIONSINI_FILENAME));
            string importedOptionsFileText;
            string changedOptionsFileText;
            importedOptionsFileText = _streamReader.ReadToEnd();
            _streamReader.Close();
            _streamReader.Dispose();

            if (importedOptionsFileText.Contains(keyName))
            {
                changedOptionsFileText = importedOptionsFileText.Replace(keyName + " = " + ReadKey(keyName, assemblyName), "");
            }
            else
            {
                return;
            }

            File.WriteAllText(Path.Combine(BFMERegistryHelper.GameAppDataFolderPath(assemblyName), ConstStringsHelper.C_OPTIONSINI_FILENAME), changedOptionsFileText);
        }

        public static void SetDefaultOptionsFile(AvailableBFMEGames assemblyName)
        {
            List<string> iniOptionsSettingsList =
                [
                     "AllHealthBars = yes",
                     "AlternateMouseSetup = no",
                     "AmbientVolume = 50",
                     "AudioLOD = High",
                     "Brightness = 50",
                     "FixedStaticGameLOD = UltraHigh",
                     "FlashTutorial = 0",
                     "HasGotOnline = yes",
                     "HasSeenLogoMovies = yes",
                     "HeatEffects = yes",
                     "IdealStaticGameLOD = UltraHigh",
                     "IsThreadedLoad = yes",
                     "MovieVolume = 70",
                     "MusicVolume = 70",
                     "Resolution = ",
                     "SFXVolume = 70",
                     "ScrollFactor = 50",
                     "StaticGameLOD = UltraHigh",
                     "TimesInGame = 1",
                     "UnitDecals = yes",
                     "UseEAX3 = yes",
                     "VoiceVolume = 70"
                     ];

            File.WriteAllLines(Path.Combine(BFMERegistryHelper.GameAppDataFolderPath(assemblyName), ConstStringsHelper.C_OPTIONSINI_FILENAME), iniOptionsSettingsList);
        }
    }
}