using Helper.Properties;
using System.Drawing.Text;
using System.Runtime.InteropServices;

namespace Helper
{
    public static class FontHelper
    {
        public static readonly byte[] C_FONT_ALBERTUS = Resources.AlbertusMT;
        public static readonly byte[] C_FONT_SACHWT = Resources.sachwt;

        [DllImport("gdi32.dll")]
        private static extern IntPtr AddFontMemResourceEx(IntPtr pbFont, uint cbFont, IntPtr pdv, [In] ref uint pcFonts);
        private static PrivateFontCollection Pfc { get; set; }

        static FontHelper()
        {
            Pfc ??= new PrivateFontCollection();
            AddMemoryFont(C_FONT_ALBERTUS);
            AddMemoryFont(C_FONT_SACHWT);
        }

        public static void AddMemoryFont(byte[] fontResource)
        {
            IntPtr p;
            uint c = 0;

            p = Marshal.AllocCoTaskMem(fontResource.Length);
            Marshal.Copy(fontResource, 0, p, fontResource.Length);
            AddFontMemResourceEx(p, (uint)fontResource.Length, IntPtr.Zero, ref c);
            Pfc.AddMemoryFont(p, fontResource.Length);
            Marshal.FreeCoTaskMem(p);
        }

        public static Font GetFont(int fontIndex, float fontSize = 20, FontStyle fontStyle = FontStyle.Regular)
        {
            return new Font(Pfc.Families[fontIndex], fontSize, fontStyle);
        }

        // Useful method for passing a 4 digit hex string to return the unicode character
        // Some fonts like FontAwesome require this conversion in order to access the characters
        public static string UnicodeToChar(string hex)
        {
            int code = int.Parse(hex, System.Globalization.NumberStyles.HexNumber);
            string unicodeString = char.ConvertFromUtf32(code);
            return unicodeString;
        }
    }
}
