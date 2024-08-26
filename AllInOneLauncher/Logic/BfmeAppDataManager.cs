using AllInOneLauncher.Data;
using System;
using System.IO;
using static AllInOneLauncher.Logic.BfmeRegistryManager;

namespace AllInOneLauncher.Logic
{
    internal class BfmeAppDataManager
    {
        internal static readonly string DefaultOptions = $@"
            AllHealthBars = yes
            AlternateMouseSetup = no
            AmbientVolume = 50.000000
            AudioLOD = High
            Brightness = 50
            FixedStaticGameLOD = UltraHigh
            FlashTutorial = 0
            HasGotOnline = yes
            HasSeenLogoMovies = yes
            HeatEffects = yes
            IdealStaticGameLOD = UltraHigh
            IsThreadedLoad = yes
            MovieVolume = 70.000000
            MusicVolume = 70.000000
            Resolution = 1920 1080
            SFXVolume = 70.000000
            ScrollFactor = 50
            StaticGameLOD = UltraHigh
            TimesInGame = 1
            UnitDecals = yes
            UseEAX3 = yes
            VoiceVolume = 70.000000";

        public static void EnsureDefaults(BfmeGame gameType)
        {
            DeleteAppPathRegistryKey(gameType);
            CreateAppPathRegistryKey(gameType);
            EnsureUserDataDirectory(gameType);
            EnsureOptionsFile(gameType);
        }

        private static void EnsureUserDataDirectory(BfmeGame gameType)
        {
            string userDataDirectory = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                GetKeyValue(gameType, BfmeRegistryKey.UserDataLeafName));

            if (!Directory.Exists(userDataDirectory))
            {
                Directory.CreateDirectory(userDataDirectory);
            }
        }

        private static void EnsureOptionsFile(BfmeGame gameType)
        {
            string optionsFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), GetKeyValue(gameType, BfmeRegistryKey.UserDataLeafName), Data.Constants.C_OPTIONSINI_FILENAME);

            if (!File.Exists(optionsFilePath) || File.ReadAllText(optionsFilePath).Length <= 6)
            {
                File.WriteAllText(optionsFilePath, DefaultOptions);
            }
        }
    }
}