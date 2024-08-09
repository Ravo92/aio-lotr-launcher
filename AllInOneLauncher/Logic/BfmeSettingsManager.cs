using System.IO;
using System.Collections.Generic;
using System.Linq;
using AllInOneLauncher.Data;

namespace AllInOneLauncher.Logic
{
    internal static class BfmeSettingsManager
    {
        internal static string? Get(BfmeGame game, string optionName)
        {
            if (!Directory.Exists(BfmeRegistryManager.GetBfmeDataPath(game)))
                Directory.CreateDirectory(BfmeRegistryManager.GetBfmeDataPath(game));

            string optionsFile = Path.Combine(BfmeRegistryManager.GetBfmeDataPath(game), Constants.C_OPTIONSINI_FILENAME);

            if (!File.Exists(optionsFile) || File.ReadAllText(optionsFile).Length <= 6)
                File.WriteAllText(optionsFile, DefaultOptions);

            Dictionary<string, string> optionsTable = File.ReadAllText(optionsFile).Split('\n').Where(x => x.Contains(" = ")).ToDictionary(x => x.Split(" = ")[0], x => x.Split(" = ")[1]);

            if (optionsTable.TryGetValue(optionName, out string? value))
                return value;
            else
                return null;
        }

        internal static void Set(BfmeGame game, string optionName, string value)
        {
            if (!Directory.Exists(BfmeRegistryManager.GetBfmeDataPath(game)))
                Directory.CreateDirectory(BfmeRegistryManager.GetBfmeDataPath(game));

            string optionsFile = Path.Combine(BfmeRegistryManager.GetBfmeDataPath(game), Constants.C_OPTIONSINI_FILENAME);

            if (!File.Exists(optionsFile) || File.ReadAllText(optionsFile).Length <= 6)
                File.WriteAllText(optionsFile, DefaultOptions);

            Dictionary<string, string> optionsTable = File.ReadAllText(optionsFile).Split('\n').Where(x => x.Contains(" = ")).ToDictionary(x => x.Split(" = ")[0], x => x.Split(" = ")[1]);

            if (!optionsTable.TryAdd(optionName, value))
                optionsTable[optionName] = value;
            File.WriteAllText(optionsFile, string.Join('\n', optionsTable.Select(x => $"{x.Key} = {x.Value}")));
        }

        internal static void EnsureOptionsFile(BfmeGame game)
        {
            if (!Directory.Exists(BfmeRegistryManager.GetBfmeDataPath(game)))
                Directory.CreateDirectory(BfmeRegistryManager.GetBfmeDataPath(game));

            string optionsFile = Path.Combine(BfmeRegistryManager.GetBfmeDataPath(game), Constants.C_OPTIONSINI_FILENAME);
            if (!File.Exists(optionsFile) || File.ReadAllText(optionsFile).Length <= 6)
                File.WriteAllText(optionsFile, DefaultOptions);
        }

        internal static string DefaultOptions = $@"AllHealthBars = yes
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
            Resolution = {SystemDisplayManager.GetPrimaryScreenResolution().Width} {SystemDisplayManager.GetPrimaryScreenResolution().Height}
            SFXVolume = 70.000000
            ScrollFactor = 50
            StaticGameLOD = UltraHigh
            TimesInGame = 1
            UnitDecals = yes
            UseEAX3 = yes
            VoiceVolume = 70.000000";
    }
}