using System;
using System.Runtime.InteropServices;

namespace AllInOneLauncher.Logic
{
    public static partial class NativeMethods
    {
        public static readonly uint SW_RESTORE = 0x09;

        public static readonly int GWL_STYLE = -16;
        public static readonly int WS_BORDER = 0x00800000;
        public static readonly int WS_THICKFRAME = 0x00040000;
        public static readonly int WS_MINIMIZEBOX = 0x00020000;
        public static readonly int WS_MAXIMIZEBOX = 0x00010000;
        public static readonly int WS_SYSMENU = 0x00800000;
        public static readonly int WS_DLGFRAME = 0x00400000;

        public static readonly uint SWP_NOZORDER = 0x0004;

        [LibraryImport("user32.dll")]
        internal static partial int ShowWindow(IntPtr hWnd, uint Msg);

        [LibraryImport("user32.dll", EntryPoint = "GetWindowLong")]
        internal static partial int GetWindowLongPtr(IntPtr hWnd, int nIndex);

        [LibraryImport("user32.dll", EntryPoint = "SetWindowLong")]
        internal static partial int SetWindowLongPtr(IntPtr hWnd, int nIndex, uint dwNewLong);

        [LibraryImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static partial bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

        [LibraryImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static partial bool SetForegroundWindow(IntPtr hWnd);

        [LibraryImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static partial bool BringWindowToTop(IntPtr hWnd);

        [LibraryImport("user32.dll", StringMarshalling = StringMarshalling.Utf16)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static partial bool SetWindowText(IntPtr hWnd, string title);

        [LibraryImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static partial bool GetWindowRect(IntPtr hwnd, ref Rect rectangle);

        [DllImport("user32.dll", BestFitMapping = false, CharSet = CharSet.Ansi, ThrowOnUnmappableChar = true)]
        internal static extern bool EnumDisplaySettings(string? deviceName, int modeNum, ref DEVMODE devMode);

        [LibraryImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static partial bool ClipCursor(ref RECT lpRect);

        [LibraryImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static partial bool ClipCursor(IntPtr lpRect);
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct DEVMODE
    {
        internal const int CCHDEVICENAME = 0x20;
        internal const int CCHFORMNAME = 0x20;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x20)]
        public string dmDeviceName;
        public short dmSpecVersion;
        public short dmDriverVersion;
        public short dmSize;
        public short dmDriverExtra;
        public int dmFields;
        public int dmPositionX;
        public int dmPositionY;
        public int dmDisplayOrientation;
        public int dmDisplayFixedOutput;
        public short dmColor;
        public short dmDuplex;
        public short dmYResolution;
        public short dmTTOption;
        public short dmCollate;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x20)]
        public string dmFormName;
        public short dmLogPixels;
        public int dmBitsPerPel;
        public int dmPelsWidth;
        public int dmPelsHeight;
        public int dmDisplayFlags;
        public int dmDisplayFrequency;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct RECT
    {
        public int Left;
        public int Top;
        public int Right;
        public int Bottom;

        public static RECT Zero => new() { Left = -1, Top = -1, Right = -1, Bottom = -1 };
        public readonly bool IsZero() => Left == -1 && Top == -1 && Right == -1 && Bottom == -1;
        public readonly bool Contains(System.Drawing.Point p) => new System.Drawing.Rectangle(Left, Top, Right, Bottom).Contains(p);
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct Rect
    {
        public int Left;
        public int Top;
        public int Right;
        public int Bottom;
    }
}