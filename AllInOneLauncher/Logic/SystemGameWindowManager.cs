using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace AllInOneLauncher.Logic
{
    internal partial class SystemGameWindowManager
    {
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll", SetLastError = true, EntryPoint = "GetWindowLongPtr", CharSet = CharSet.Auto)]
        private static extern IntPtr GetWindowLongPtr(IntPtr hWnd, int nIndex);

        // SetWindowLongPtr ebenfalls für 64-Bit-Systeme
        [DllImport("user32.dll", SetLastError = true, EntryPoint = "SetWindowLongPtr", CharSet = CharSet.Auto)]
        private static extern IntPtr SetWindowLongPtr(IntPtr hWnd, int nIndex, IntPtr dwNewLong);

        [LibraryImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static partial bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool EnumWindows(EnumWindowsProc lpEnumFunc, IntPtr lParam);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        private static extern int GetClassName(IntPtr hWnd, StringBuilder lpClassName, int nMaxCount);

        [DllImport("user32.dll")]
        private static extern bool GetClientRect(IntPtr hWnd, out RECT lpRect);

        private delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);

        private const int GWL_STYLE = -16;
        private const long WS_BORDER = 0x00800000L;
        private const long WS_DLGFRAME = 0x00400000L;
        private const int SWP_FRAMECHANGED = 0x0020;
        private const int SWP_SHOWWINDOW = 0x0040;
        private static readonly IntPtr HWND_TOP = IntPtr.Zero;

        public struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;

            public readonly int Width => Right - Left;
            public readonly int Height => Bottom - Top;
        }

        public static IntPtr FindWindowByClassName(string className)
        {
            IntPtr windowHandle = IntPtr.Zero;
            _ = EnumWindows(delegate (IntPtr hWnd, IntPtr lParam)
            {
                StringBuilder sb = new(256);
                _ = GetClassName(hWnd, sb, sb.Capacity);
                if (sb.ToString() == className)
                {
                    windowHandle = hWnd;
                    return false;
                }
                return true;
            }, IntPtr.Zero);

            return windowHandle;
        }

        public static bool RemoveWindowBorder(string className, int positionX = 0, int positionY = 0, int resolutionX = 800, int resolutionY = 600, int timeoutMilliseconds = 10000, int pollingInterval = 100)
        {
            IntPtr hWnd = IntPtr.Zero;
            int elapsedTime = 0;

            while (hWnd == IntPtr.Zero && elapsedTime < timeoutMilliseconds)
            {
                hWnd = FindWindowByClassName(className);
                if (hWnd == IntPtr.Zero)
                {
                    Thread.Sleep(pollingInterval);
                    elapsedTime += pollingInterval;
                }
            }

            if (hWnd != IntPtr.Zero)
            {
                IntPtr stylePtr = GetWindowLongPtr(hWnd, GWL_STYLE);
                long style = stylePtr.ToInt64();
                style &= ~(WS_BORDER | WS_DLGFRAME);
                _ = SetWindowLongPtr(hWnd, GWL_STYLE, new IntPtr(style));

                GetClientRect(hWnd, out RECT clientRect);
                int extraWidth = (resolutionX + (resolutionX - clientRect.Width));
                int extraHeight = (resolutionY + (resolutionY - clientRect.Height));

                SetWindowPos(hWnd, HWND_TOP, positionX, positionY, extraWidth, extraHeight, SWP_FRAMECHANGED | SWP_SHOWWINDOW);
                SystemInputManager.SetTargetHWnd(hWnd);
                return true;
            }
            return false;
        }
    }
}
