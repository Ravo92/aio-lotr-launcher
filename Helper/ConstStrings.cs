using Helper.Properties;

namespace Helper
{
    public static class ConstStrings
    {
        public const string C_GAMEFOLDER_NAME_EN = "The Battle for Middle-earth (tm)";
        public const string C_APPDATAFOLDER_NAME_EN = "My Battle for Middle-earth Files";
        public const string C_EAFOLDER_NAME = "EA Games";

        public const string C_PATCHZIP06_NAME = "Patch_1.06.7z";
        public const string C_PATCHZIP29_NAME = "Patch_2.22v29.7z";
        public const string C_PATCHZIP30_NAME = "Patch_2.22v30.7z";
        public const string C_PATCHZIP31_NAME = "Patch_2.22v31.7z";

        //////////////////////////////////////////////////////////////////////////////

        // 222v29
        public const string C_MAIN_PATCH_FILE = "_patch222.big";
        public const string C_TEXTURES_PATCH_FILE = "_patch222newtextures.big";
        public const string C_LIBRARIES_PATCH_FILE = "_patch222libraries.big";
        public const string C_BASES_PATCH_FILE = "_patch222bases.big";
        public const string C_MAPS_PATCH_FILE = "_wsmaps222.big";
        public const string C_ASSET_PATCH_FILE = "asset.dat.222V29";

        // 3.0 and above
        public const string C_ENGLISHPATCH_V30_FILE = "_englishpatch222.big";
        public const string C_MAIN_PATCH_V30_FILE = "_patch222.big";
        public const string C_LIBRARIES_PATCH_V30_FILE = "_patch222libraries.big";
        public const string C_TEXTURES_PATCH_V30_FILE = "_patch222textures.big";
        public const string C_BASES_PATCH_V30_FILE = "_patch222bases.big";
        public const string C_MAPS_PATCH_V30_FILE = "_patch222maps.big";
        public const string C_ASSET_PATCH_V30_FILE = "asset.dat.222V31";

        //////////////////////////////////////////////////////////////////////////////

        public const string C_OPTIONAL_PATCH_FILE = "_patch222optional.big";

        public const string C_OPTIONSINI_FILENAME = "Options.ini";

        public const int C_UPDATE_VERSION = 31;
        
        public const string C_UPDATEMD5_HASH = "802e9a19070f08550c57fe1121231fa2";
        public const string C_MAINGAMEFILE_MD5_HASH = "97258f514dce6eb4d5d110dbb4d3cca3";
        public const string C_LANGPACK_EN_MD5_HASH = "fde15ba61be8c1b321e13a4ba9d8c4a8";

        public const string C_MAINGAMEFILE_ZIP = "BFME1.7z";
        public const string C_LANGPACK_EN_ZIP = "LangPack_EN.7z";

        public const string C_MAIN_GAME_FILE = "lotrbfme.exe";
        public const string C_MAIN_ASSET_FILE = "asset.dat";
        public const string C_103_ASSET_FILE = "asset.dat.103";
        public const string C_ORIGINAL_ASSET_FILE = "asset.dat.BACKUP";

        public const string C_TWITCHCHANNEL_NAME = "beyondstandards";

        public const string C_TOOLFOLDER_NAME = "Tools";
        public const string C_PATCHFOLDER_NAME = "Patches";
        public const string C_DOWNLOADFOLDER_NAME = "Downloads";
        public const string C_BETAFOLDER_NAME = "Beta";

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
            if (RegistryService.ReadRegKey("appData") != "ValueNotFound")
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
