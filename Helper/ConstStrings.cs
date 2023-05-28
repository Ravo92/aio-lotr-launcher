using Helper.Properties;

namespace Helper
{
    public static class ConstStrings
    {
        public const string C_GAMEFOLDER_NAME_BFME1 = "BFME1";
        public const string C_APPDATAFOLDER_NAME_EN = "My Battle for Middle-earth Files";
        public const string C_EAFOLDER_NAME = "EA Games";

        public const string C_REGISTRY_SERVICE_WRONG_PARAMETER = "WrongParameter";
        public const string C_REGISTRY_SERVICE_NOT_FOUND = "ValueNotFound";

        public const string C_PATCHZIP06_NAME = "Patch_1.06.7z";
        public const string C_PATCHZIP30_NAME = "Patch_2.22v30.7z";
        public const string C_PATCHZIP31_NAME = "Patch_2.22v31.7z";
        public const string C_PATCHZIP32_NAME = "Patch_2.22v32.7z";

        //////////////////////////////////////////////////////////////////////////////

        public const string C_ENGLISHPATCH_FILE = "_englishpatch222.big";
        public const string C_MAIN_PATCH_FILE = "_patch222.big";
        public const string C_LIBRARIES_PATCH_FILE = "_patch222libraries.big";
        public const string C_TEXTURES_PATCH_FILE = "_patch222textures.big";
        public const string C_BASES_PATCH_FILE = "_patch222bases.big";
        public const string C_MAPS_PATCH_FILE = "_patch222maps.big";
        public const string C_ASSET_PATCH_FILE = "asset.dat.222";

        // BFME 1 Patches
        public const string C_BFME1_PATCH_101_FILE = "_patch101.big";
        public const string C_BFME1_PATCH_101_MD5H = "c2be8f668a64dfa9f8b42fe4fd9ff77e";

        // BFME 1 Mods
        public const string C_BFME1_MOD_SHADOW_AND_FLAME_11_FILE = "SaF1.1.big";
        public const string C_BFME1_MOD_SHADOW_AND_FLAME_11_MD5H = "1c35d9ccfa922e9fb6ff0097effdef83";

        //////////////////////////////////////////////////////////////////////////////

        public const string C_OPTIONAL_PATCH_FILE = "_patch222optional.big";

        public const string C_OPTIONSINI_FILENAME = "Options.ini";

        public const int C_UPDATE_VERSION = 32;
        
        public const string C_UPDATEMD5_HASH = "235AC8344C0AA5675C5850D97336BFF3";
        public const string C_MAINGAMEFILE_ZIP_MD5_HASH = "97258f514dce6eb4d5d110dbb4d3cca3";
        public const string C_LANGPACK_EN_MD5_HASH = "fde15ba61be8c1b321e13a4ba9d8c4a8";

        public const string C_MAINGAMEFILE_ZIP = "BFME1.7z";
        public const string C_LANGPACK_EN_ZIP = "LangPack_EN.7z";

        public const string C_MAIN_GAME_FILE = "lotrbfme.exe";
        public const string C_MAIN_ASSET_FILE = "asset.dat";
        public const string C_103_ASSET_FILE = "asset.dat.103";
        public const string C_ORIGINAL_ASSET_FILE = "asset.dat.BACKUP";

        public const string C_ERRORLOGGING_FILE = "Errors.log";

        public const string C_TWITCHCHANNEL_NAME = "beyondstandards";

        public const string C_TOOLFOLDER_NAME = "Tools";
        public const string C_PATCHFOLDER_NAME = "Patches";
        public const string C_DOWNLOADFOLDER_NAME = "Downloads";
        public const string C_BETAFOLDER_NAME = "Beta";
        public const string C_GAMEINSTALLFOLDER_NAME = "Games";
        public const string C_WEBVIEW2CACHEFOLDER_NAME = "PatchLauncherBFME.exe.WebView2";
        public const string C_LOGFOLDER_NAME = "Logfiles";

        //////////////////////////////////////////////////////////////////////////////

        public const string C_PATCH105MAIN_FILENAME = "_patch105.big";
        public const string C_PATCH106MAIN_FILENAME = "_patch106.big";
        public const string C_PATCH106TEXTURES_FILENAME = "_patch106textures.big";
        public const string C_PATCH106APT_FILENAME = "_aptpatch.big";
        public const string C_PATCH106WSMAPS_FILENAME = "_WSMaps.big";

        //////////////////////////////////////////////////////////////////////////////

        public const string C_THEMESOUND_DEFAULT = "Default";
        public const string C_THEMESOUND_GONDOR = "Gondor";
        public const string C_THEMESOUND_ROHAN = "Rohan";
        public const string C_THEMESOUND_ISENGARD = "Isengard";
        public const string C_THEMESOUND_MORDOR = "Mordor";

        public static readonly Image C_BUTTONIMAGE_NEUTR = Resources.btnNeutral;
        public static readonly Image C_BUTTONIMAGE_HOVER = Resources.btnHover;
        public static readonly Image C_BUTTONIMAGE_CLICK = Resources.btnClick;
        public static readonly Image C_BUTTONIMAGE_CLICK_GREEN = Resources.btnClickgr;

        public static string GameLanguage()
        {
            return RegistryService.ReadRegKey("lang");
        }

        public static string GameInstallPath()
        {
            return RegistryService.ReadRegKey("path");
        }

        public static string GameAppdataFolderPath()
        {
            if (RegistryService.ReadRegKey("appData") != C_REGISTRY_SERVICE_NOT_FOUND)
            {
                return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), RegistryService.ReadRegKey("appData"));
            }
            else
            {
                return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), C_APPDATAFOLDER_NAME_EN);
            }
        }
    }
}
