using System;
using System.Drawing;
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

        public const string C_FONT_ALBERTUS_NOVA = "Albertus Nova";

        public static readonly Image C_BUTTONIMAGE_NEUTR = Image.FromFile(@"Images\btnNeutral.png");
        public static readonly Image C_BUTTONIMAGE_HOVER = Image.FromFile(@"Images\btnHover.png");
        public static readonly Image C_BUTTONIMAGE_CLICK = Image.FromFile(@"Images\btnClick.png");
        public static readonly Image C_BUTTONIMAGE_CLICK_GREEN = Image.FromFile(@"Images\btnClickgr.png"); 

        public static readonly string C_BUTTONSOUND_HOVER = @"Sounds\btnHover.wav";
        public static readonly string C_BUTTONSOUND_CLICK = @"Sounds\btnClick.wav";

        public static string GameLanguage()
        {
            return RegistryFunctions.ReadRegKey("lang");
        }

        public static string GameInstallPath()
        {
            return RegistryFunctions.ReadRegKey("path");
        }
        public static string GameAppdataFolder()
        {
            return RegistryFunctions.ReadRegKey("appData");
        }
        public static string GameAppdataFolderPath()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\" + GameAppdataFolder() + "\\";
        }
        public static string OptionsIniFileName()
        {
            return "Options.ini";
        }

        public static Font UseFont(string font, int size)
        {
            font ??= C_FONT_ALBERTUS_NOVA;
            FontFamily _fontFamily = new(font, collection);
            Font _font = new(_fontFamily, size);
            return _font;
            //switch (font)
            //{
            //    case "Albertus MT":
            //        {
            //            FontFamily _fontFamily = new("Albertus MT", collection);
            //            Font _font = new(_fontFamily, size);
            //            return _font;
            //        }
            //    case "Albertus Nova":
            //        {
            //            FontFamily _fontFamily = new("Albertus Nova", collection);
            //            Font _font = new(_fontFamily, size);
            //            return _font;
            //        }
            //    case "SachaWynterTight":
            //        {
            //            FontFamily _fontFamily = new("SachaWynterTight", collection);
            //            Font _font = new(_fontFamily, size);
            //            return _font;
            //        }
            //    default:
            //        {
            //            FontFamily _fontFamily = new("Albertus Nova", collection);
            //            Font _font = new(_fontFamily, size);
            //            return _font;
            //        }
            //}
        }
    }
}
