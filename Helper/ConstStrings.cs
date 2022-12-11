using System.Drawing.Text;

namespace Helper
{
    public static class ConstStrings
    {
        public static PrivateFontCollection collection = new();
        static ConstStrings()
        {
            collection.AddFontFile(@"Fonts\albertusmt.otf");
            collection.AddFontFile(@"Fonts\AlbertusNova.ttf");
            collection.AddFontFile(@"Fonts\sachwt.ttf");
        }

        public const string C_GAMEFOLDER_NAME = "The Battle for Middle-earth (tm)";
        public const string C_EAFOLDER_NAME = "EA Games";

        public const string C_PATCHFOLDER_NAME = "Patches";

        public const string C_PATCHFOLDER26_NAME = "Patch222_2.41";
        public const string C_PATCHFOLDER27_NAME = "Patch222_2.43";
        public const string C_PATCHFOLDER28_NAME = "Patch222_2.45";

        public const string C_PATCHZIP29_NAME = "Patch_2.22v29.7z";

        public const string C_MAIN_PATCH_FILE = "_patch222.big";
        public const string C_TEXTURES_PATCH_FILE = "_patch222newtextures.big";
        public const string C_LIBRARIES_PATCH_FILE = "_patch222libraries.big";
        public const string C_BASES_PATCH_FILE = "_patch222bases.big";
        public const string C_MAPS_PATCH_FILE = "_wsmaps222.big";
        public const string C_ASSET_PATCH_FILE = "asset.dat.222V29";

        public const string C_OPTIONSINI_FILENAME = "Options.ini";

        public const int C_UPDATE_VERSION = 29;
        public const string C_UPDATEMD5_HASH = "a007b2ea1f87a530c1e412255e1d7896";
        public const string C_MAIN_GAME_FILE = "lotrbfme.exe";
        public const string C_MAIN_ASSET_FILE = "asset.dat";
        public const string C_103_ASSET_FILE = "asset.dat.103";
        public const string C_ORIGINAL_ASSET_FILE = "asset.dat.BACKUP";

        public const string C_TWITCHCHANNEL_NAME = "beyondstandards";

        public const string C_TOOLFOLDER_NAME = "Tools";
        public const string C_PATCH105MAIN_FILENAME = "_patch105.big";
        public const string C_PATCH106MAIN_FILENAME = "_patch106.big";
        public const string C_PATCH106TEXTURES_FILENAME = "_patch106textures.big";

        public const string C_FONT_ALBERTUS_NOVA = "Albertus Nova";

        public const string C_BUTTONSOUND_HOVER = "Helper.Sounds.btnHover.wav";
        public const string C_BUTTONSOUND_CLICK = "Helper.Sounds.btnClick.wav";

        public const string C_THEMESOUND_DEFAULT = "Helper.Sounds.music_default.wav";
        public const string C_THEMESOUND_GONDOR = "Helper.Sounds.music_gondor.wav";
        public const string C_THEMESOUND_ROHAN = "Helper.Sounds.music_rohan.wav";
        public const string C_THEMESOUND_ISENGARD = "Helper.Sounds.music_isengard.wav";
        public const string C_THEMESOUND_MORDOR = "Helper.Sounds.music_mordor.wav";

        public static readonly Image C_BUTTONIMAGE_NEUTR = Image.FromFile(@"Images\btnNeutral.png");
        public static readonly Image C_BUTTONIMAGE_HOVER = Image.FromFile(@"Images\btnHover.png");
        public static readonly Image C_BUTTONIMAGE_CLICK = Image.FromFile(@"Images\btnClick.png");
        public static readonly Image C_BUTTONIMAGE_CLICK_GREEN = Image.FromFile(@"Images\btnClickgr.png"); 

        public static string GameLanguage()
        {
            return RegistryService.ReadRegKey("lang");
        }

        public static string GameInstallPath()
        {
            return RegistryService.ReadRegKey("path");
        }
        public static string GameAppdataFolderName()
        {
            return RegistryService.ReadRegKey("appData");
        }
        public static string GameAppdataFolderPath()
        {
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), GameAppdataFolderName());
        }

        public static Font UseFont(string font, int size)
        {
            font ??= C_FONT_ALBERTUS_NOVA;
            FontFamily _fontFamily = new(font, collection);
            Font _font = new(_fontFamily, size);
            return _font;
        }
    }
}
