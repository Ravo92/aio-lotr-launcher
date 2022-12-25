using System.Drawing.Text;

namespace Helper
{
    public static class ConstStrings
    {
        private static readonly PrivateFontCollection collection = new();
        static ConstStrings()
        {
            collection.AddFontFile(Path.Combine(Application.StartupPath, C_FONTSFOLDER_NAME, "albertusmt.otf"));
            collection.AddFontFile(Path.Combine(Application.StartupPath, C_FONTSFOLDER_NAME, "AlbertusNova.ttf"));
            collection.AddFontFile(Path.Combine(Application.StartupPath, C_FONTSFOLDER_NAME, "sachwt.ttf"));
        }

        public const string C_GAMEFOLDER_NAME_EN = "The Battle for Middle-earth (tm)";
        public const string C_APPDATAFOLDER_NAME_EN = "My Battle for Middle-earth Files";
        public const string C_EAFOLDER_NAME = "EA Games";

        public const string C_PATCHZIP06_NAME = "Patch_1.06.7z";
        public const string C_PATCHZIP26_NAME = "Patch_2.22v26.7z";
        public const string C_PATCHZIP27_NAME = "Patch_2.22v27.7z";
        public const string C_PATCHZIP28_NAME = "Patch_2.22v28.7z";
        public const string C_PATCHZIP29_NAME = "Patch_2.22v29.7z";
        public const string C_PATCHZIP30_NAME = "Patch_2.22v30.7z";
        public const string C_PATCHZIP31_NAME = "Patch_2.22v31.7z";

        //////////////////////////////////////////////////////////////////////////////

        // 222v29 and below
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
        public const string C_ASSET_PATCH_V30_FILE = "asset.dat.222V30";

        //////////////////////////////////////////////////////////////////////////////

        public const string C_OPTIONAL_PATCH_FILE = "_patch222optional.big";

        public const string C_OPTIONSINI_FILENAME = "Options.ini";

        public const int C_UPDATE_VERSION = 30;
        public const string C_UPDATEMD5_HASH = "a007b2ea1f87a530c1e412255e1d7896";
        public const string C_MAIN_GAME_FILE = "lotrbfme.exe";
        public const string C_MAIN_ASSET_FILE = "asset.dat";
        public const string C_103_ASSET_FILE = "asset.dat.103";
        public const string C_ORIGINAL_ASSET_FILE = "asset.dat.BACKUP";

        public const string C_TWITCHCHANNEL_NAME = "beyondstandards";

        public const string C_TOOLFOLDER_NAME = "Tools";
        public const string C_FONTSFOLDER_NAME = "Fonts";
        public const string C_IMAGESFOLDER_NAME = "Images";
        public const string C_PATCHFOLDER_NAME = "Patches";

        //////////////////////////////////////////////////////////////////////////////

        public const string C_PATCH105MAIN_FILENAME = "_patch105.big";
        public const string C_PATCH106MAIN_FILENAME = "_patch106.big";
        public const string C_PATCH106TEXTURES_FILENAME = "_patch106textures.big";
        public const string C_PATCH106APT_FILENAME = "_aptpatch.big";
        public const string C_PATCH106WSMAPS_FILENAME = "_WSMaps.big";

        //////////////////////////////////////////////////////////////////////////////

        public const string C_FONT_ALBERTUS_NOVA = "Albertus Nova";

        public const string C_BUTTONSOUND_HOVER = "Helper.Sounds.btnHover.wav";
        public const string C_BUTTONSOUND_CLICK = "Helper.Sounds.btnClick.wav";

        public const string C_THEMESOUND_DEFAULT = "Helper.Sounds.music_default.wav";
        public const string C_THEMESOUND_GONDOR = "Helper.Sounds.music_gondor.wav";
        public const string C_THEMESOUND_ROHAN = "Helper.Sounds.music_rohan.wav";
        public const string C_THEMESOUND_ISENGARD = "Helper.Sounds.music_isengard.wav";
        public const string C_THEMESOUND_MORDOR = "Helper.Sounds.music_mordor.wav";

        public static readonly Image C_BUTTONIMAGE_NEUTR = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "btnNeutral.png"));
        public static readonly Image C_BUTTONIMAGE_HOVER = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "btnHover.png"));
        public static readonly Image C_BUTTONIMAGE_CLICK = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "btnClick.png"));
        public static readonly Image C_BUTTONIMAGE_CLICK_GREEN = Image.FromFile(Path.Combine(Application.StartupPath, ConstStrings.C_IMAGESFOLDER_NAME, "btnClickgr.png")); 

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

        public static Font UseFont(string font, int size)
        {
            font ??= C_FONT_ALBERTUS_NOVA;
            try {
              FontFamily _fontFamily = new(font, collection);
            } catch {
              // Font name not found in collection, try to load it from the system
              try {
                FontFamily _fontFamily = new(font);
              } catch {
                // Font name not found in system, use default font
                FontFamily _fontFamily = new(FontFamily.GenericSansSerif.Name);
              }
            }
            Font _font = new(_fontFamily, size);
            return _font;
        }
    }
}
