using System;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;

namespace PatchLauncher.Classes
{
    public class ConstStrings
    {
        public PrivateFontCollection collection = new();
        public const string gameFolderName = "The Battle for Middle-earth (tm)";

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

        public Font UseFont(string font, int size)
        {
            collection.AddFontFile(@"Fonts\albertusmt.otf");
            collection.AddFontFile(@"Fonts\AlbertusNova.ttf");
            collection.AddFontFile(@"Fonts\sachwt.ttf");

            switch (font)
            {
                case "Albertus MT":
                    {
                        FontFamily _fontFamily = new("Albertus MT", collection);
                        Font _font = new(_fontFamily, size);
                        return _font;
                    }
                case "Albertus Nova":
                    {
                        FontFamily _fontFamily = new("Albertus Nova", collection);
                        Font _font = new(_fontFamily, size);
                        return _font;
                    }
                case "SachaWynterTight":
                    {
                        FontFamily _fontFamily = new("SachaWynterTight", collection);
                        Font _font = new(_fontFamily, size);
                        return _font;
                    }
                default:
                    {
                        FontFamily _fontFamily = new("Albertus Nova", collection);
                        Font _font = new(_fontFamily, size);
                        return _font;
                    }
            }
        }
        public static Image ButtonImageNeutral()
        {
            return Image.FromFile("Images\\btnNeutral.png");
        }

        public static Image ButtonImageHover()
        {
            return Image.FromFile("Images\\btnHover.png");
        }

        public static Image ButtonImageClick()
        {
            return Image.FromFile("Images\\btnClick.png");
        }
        public static string ButtonSoundHover()
        {
            return "Sounds\\btnHover.wav";
        }

        public static string ButtonSoundClick()
        {
            return "Sounds\\btnClick.wav";
        }
    }
}
