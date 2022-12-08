using System.Drawing.Text;

namespace PatchLauncher.Helper
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
        public const string C_OPTIONSINI_FILENAME = "Options.ini";

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
        public static string GameAppdataFolder()
        {
            return RegistryService.ReadRegKey("appData");
        }
        public static string GameAppdataFolderPath()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\" + GameAppdataFolder() + "\\";
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
