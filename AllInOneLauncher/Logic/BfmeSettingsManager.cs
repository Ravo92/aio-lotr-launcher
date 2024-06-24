using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace AllInOneLauncher.Logic
{
    public static class BfmeSettingsManager
    {
        public static string? Get(int game, string optionName)
        {
            string optionsFile = Path.Combine(BfmeRegistryManager.GetBfmeDataPath(game), "Options.ini");

            if (!File.Exists(optionsFile) || File.ReadAllText(optionsFile).Length <= 6)
                File.WriteAllText(optionsFile, DefaultOptions);

            Dictionary<string, string> optionsTable = File.ReadAllText(optionsFile).Split('\n').Where(x => x.Contains(" = ")).ToDictionary(x => x.Split(" = ")[0], x => x.Split(" = ")[1]);

            if (optionsTable.ContainsKey(optionName))
                return optionsTable[optionName];
            else
                return null;
        }

        public static void Set(int game, string optionName, string value)
        {
            string optionsFile = Path.Combine(BfmeRegistryManager.GetBfmeDataPath(game), "Options.ini");

            if (!File.Exists(optionsFile) || File.ReadAllText(optionsFile).Length <= 6)
                File.WriteAllText(optionsFile, DefaultOptions);

            Dictionary<string, string> optionsTable = File.ReadAllText(optionsFile).Split('\n').Where(x => x.Contains(" = ")).ToDictionary(x => x.Split(" = ")[0], x => x.Split(" = ")[1]);

            if(optionsTable.ContainsKey(optionName))
                optionsTable[optionName] = value;
            else
                optionsTable.Add(optionName, value);

            File.WriteAllText(optionsFile, string.Join('\n', optionsTable.Select(x => $"{x.Key} = {x.Value}")));
        }

        public static void EnsureOptionsFile(int game)
        {
            string optionsFile = Path.Combine(BfmeRegistryManager.GetBfmeDataPath(game), "Options.ini");
            if (!File.Exists(optionsFile) || File.ReadAllText(optionsFile).Length <= 6)
                File.WriteAllText(optionsFile, DefaultOptions);
        }

        internal const string DefaultOptions = @"AllHealthBars = yes
AlternateMouseSetup = no
AmbientVolume = 28
AudioLOD = High
Brightness = 50
FixedStaticGameLOD = Medium
FlashTutorial = 0
HasGotOnline = yes
HasSeenLogoMovies = yes
HeatEffects = yes
IdealStaticGameLOD = Medium
IsThreadedLoad = yes
MovieVolume = 25
MusicVolume = 24
Resolution = 1920 1080
SFXVolume = 34
ScrollFactor = 50
StaticGameLOD = Medium
TimesInGame = 0
UnitDecals = yes
UseEAX3 = no
VoiceVolume = 28";
    }
}