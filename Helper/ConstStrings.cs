using Helper.Properties;

namespace Helper
{
    public static class ConstStrings
    {
        public const string C_GAMEFOLDER_NAME_BFME1 = "BFME1";
        public const string C_APPDATAFOLDER_NAME_EN = "My Battle for Middle-earth Files";
        public const string C_GAMETITLE_NAME_EN = "The Battle for Middle-earth";
        public const string C_EAFOLDER_NAME = "EA Games";

        public const string C_MUTEX_NAME = "Patch Launcher 2.22 Ravo92";
        public const string C_LAUNCHER_SHORTCUT_NAME = "Patch 2.22 Launcher";

        public const string C_REGISTRY_SERVICE_WRONG_PARAMETER = "WrongParameter";
        public const string C_REGISTRY_SERVICE_NOT_FOUND = "ValueNotFound";

        public const string C_PATCHZIP06_NAME = "Patch_1.06.7z";
        public const string C_PATCHZIP33_NAME = "Patch_2.22v33.7z";
        public const string C_PATCHZIP34_NAME = "Patch_2.22v34.7z";

        //////////////////////////////////////////////////////////////////////////////

        public const string C_ENGLISHPATCH_FILE = "_englishpatch222.big";
        public const string C_MAIN_PATCH_FILE = "_patch222.big";
        public const string C_LIBRARIES_PATCH_FILE = "_patch222libraries.big";
        public const string C_TEXTURES_PATCH_FILE = "_patch222textures.big";
        public const string C_BASES_PATCH_FILE = "_patch222bases.big";
        public const string C_MAPS_PATCH_FILE = "_patch222maps.big";
        public const string C_ASSET_PATCH_FILE = "asset.dat.222";

        public const string C_GERMANLANGUAGE_PATCH_FILE = "_patch222translation_ger.big";

        // BFME 1 Mods
        public const string C_BFME1_MOD_SHADOW_AND_FLAME_11_FILE = "SaF1.1.big";
        public const string C_BFME1_MOD_SHADOW_AND_FLAME_11_MD5H = "1c35d9ccfa922e9fb6ff0097effdef83";

        //////////////////////////////////////////////////////////////////////////////

        public const string C_OPTIONAL_PATCH_FILE = "_patch222optional.big";

        public const string C_OPTIONSINI_FILENAME = "Options.ini";

        public const string C_RESTARTEREXE_FILENAME = "Restarter.exe";
        public const string C_LAUNCHEREXE_BFME1_FILENAME = "BFME1.exe";
        public const string C_LAUNCHEREXE_BFME2_FILENAME = "BFME2.exe";
        public const string C_LAUNCHEREXE_BFME25_FILENAME = "BFME25.exe";

        public const string C_MAINGAMEFILE_ZIP = "BFME1.7z";
        public const string C_LANGPACK_EN_ZIP = "LangPack_EN.7z";

        public const string C_MAIN_GAME_FILE = "lotrbfme.exe";
        public const string C_WORLDBUILDER_FILE = "worldbuilder.exe";
        public const string C_MAIN_ASSET_FILE = "asset.dat";
        public const string C_103_ASSET_FILE = "asset.dat.103";
        public const string C_ORIGINAL_ASSET_FILE = "asset.dat.BACKUP";

        public const string C_ERRORLOGGING_FILE = "Errors.log";
        public const string C_LAUNCHERSELECTEDINFOFILE = "selectedGameLauncher.txt";
        public const string C_JSON_GAMEDICTIONARY_FILE = "GameFileDictionary.json";

        public const string C_TWITCHCHANNEL_NAME = "beyondstandards";

        public const string C_TOOLFOLDER_NAME = "Tools";
        public const string C_PATCHFOLDER_NAME = "Patches";
        public const string C_DOWNLOADFOLDER_NAME = "Downloads";
        public const string C_BETAFOLDER_NAME = "Beta";
        public const string C_GAMEINSTALLFOLDER_NAME = "Games";
        public const string C_LOGFOLDER_NAME = "Logfiles";
        public const string C_HTMLFOLDER_NAME = "Html";
        public const string C_WEBVIEW2CACHEFOLDER_NAME = "BFME1.exe.WebView2";

        //////////////////////////////////////////////////////////////////////////////

        public const string C_PATCH105MAIN_FILENAME = "_patch105.big";
        public const string C_PATCH106MAIN_FILENAME = "_patch106.big";
        public const string C_PATCH106TEXTURES_FILENAME = "_patch106textures.big";
        public const string C_PATCH106APT_FILENAME = "_aptpatch.big";
        public const string C_PATCH106WSMAPS_FILENAME = "_WSMaps.big";

        public const string C_THEMESOUND_DEFAULT = "Default";
        public const string C_THEMESOUND_GONDOR = "Gondor";
        public const string C_THEMESOUND_ROHAN = "Rohan";
        public const string C_THEMESOUND_ISENGARD = "Isengard";
        public const string C_THEMESOUND_MORDOR = "Mordor";

        public static readonly Image C_BUTTONIMAGE_NEUTR = Resources.btnNeutral;
        public static readonly Image C_BUTTONIMAGE_HOVER = Resources.btnHover;
        public static readonly Image C_BUTTONIMAGE_CLICK = Resources.btnClick;
        public static readonly Image C_BUTTONIMAGE_CLICK_GREEN = Resources.btnClickgr;

        //////////////////////////////////////////////////////////////////////////////

        public static readonly string LogTime = Environment.NewLine + Environment.NewLine + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + Environment.NewLine;
    }
}