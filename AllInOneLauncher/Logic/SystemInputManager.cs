using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace AllInOneLauncher.Logic
{
    internal partial class SystemInputManager
    {
        [LibraryImport("user32.dll", SetLastError = true, EntryPoint = "SetWindowsHookExW")]
        internal static partial IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

        [LibraryImport("user32.dll", SetLastError = true, EntryPoint = "SetWindowsHookExW")]
        internal static partial IntPtr SetWindowsHookEx(int idHook, LowLevelMouseProc lpfn, IntPtr hMod, uint dwThreadId);

        [LibraryImport("kernel32.dll", EntryPoint = "GetModuleHandleW", SetLastError = true, StringMarshalling = StringMarshalling.Utf16)]
        public static partial IntPtr GetModuleHandle(string lpModuleName);

        [LibraryImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static partial bool ClipCursor(ref RECT lpRect);

        [LibraryImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static partial bool ClipCursor(IntPtr lpRect);

        [LibraryImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static partial bool GetClipCursor(out RECT lpRect);

        [LibraryImport("user32.dll", SetLastError = true)]
        internal static partial IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        [LibraryImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static partial bool SetForegroundWindow(IntPtr hWnd);

        [LibraryImport("user32.dll")]
        private static partial IntPtr GetForegroundWindow();

        [LibraryImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static partial bool BringWindowToTop(IntPtr hWnd);

        [LibraryImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static partial bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

        public static void ActivateWindow(IntPtr handle)
        {
            SetForegroundWindow(handle);
            BringWindowToTop(handle);
        }

        public delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);
        public delegate IntPtr LowLevelMouseProc(int nCode, IntPtr wParam, IntPtr lParam);

        private static readonly LowLevelKeyboardProc _keyboardProc = KeyboardHookCallback;
        private static readonly LowLevelMouseProc _mouseProc = MouseHookCallback;
        private static IntPtr _keyboardHookID = IntPtr.Zero;
        private static IntPtr _mouseHookID = IntPtr.Zero;
        private static IntPtr _targetHWnd = IntPtr.Zero;

        public const int WH_KEYBOARD_LL = 13;
        public const int WH_MOUSE_LL = 14;

        private const int WM_LBUTTONDOWN = 0x0201;
        private const int WM_RBUTTONDOWN = 0x0204;

        private static RECT CurrentClipRect;

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;

            public static RECT Zero => new() { Left = 0, Top = 0, Right = 0, Bottom = 0 };

            public readonly bool IsZero() => Left == 0 && Top == 0 && Right == 0 && Bottom == 0;

            public readonly bool Contains(POINT point) => point.x >= Left && point.x <= Right && point.y >= Top && point.y <= Bottom;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public int x;
            public int y;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct MSLLHOOKSTRUCT
        {
            public POINT pt;
            public int mouseData;
            public int flags;
            public int time;
            public IntPtr dwExtraInfo;
        }

        public static void Init()
        {
            _keyboardHookID = SetKeyboardHook(_keyboardProc);
            _mouseHookID = SetMouseHook(_mouseProc);
        }

        public static void SetTargetHWnd(IntPtr hWnd)
        {
            _targetHWnd = hWnd;
        }

        private static IntPtr KeyboardHookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0)
            {
                int vkCode = Marshal.ReadInt32(lParam);
                if ((Control.ModifierKeys == Keys.Alt && vkCode == (int)Keys.Tab) || vkCode == (int)Keys.LWin || vkCode == (int)Keys.RWin)
                {
                    ReleaseCursor();
                }
            }

            CheckAndClipCursorToTargetWindow();

            return CallNextHookEx(_keyboardHookID, nCode, wParam, lParam);
        }

        private static void CheckAndClipCursorToTargetWindow()
        {
            IntPtr foregroundWindow = GetForegroundWindow();
            if (foregroundWindow == _targetHWnd && _targetHWnd != IntPtr.Zero)
            {
                ClipCursorToWindow(_targetHWnd);
            }
        }

        private static void ClipCursorToWindow(IntPtr hWnd)
        {
            if (GetWindowRect(hWnd, out RECT rect))
            {
                ClipCursor(ref rect);
            }
        }

        private static IntPtr MouseHookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0 && (wParam == (IntPtr)WM_LBUTTONDOWN || wParam == (IntPtr)WM_RBUTTONDOWN))
            {
                CheckAndClipCursorToTargetWindow();
            }

            return CallNextHookEx(_mouseHookID, nCode, wParam, lParam);
        }

        public static void ConfineCursor(int x, int y, int width, int height, bool withPadding)
        {
            CurrentClipRect = new RECT
            {
                Left = x + (withPadding ? 3 : 0),
                Top = y + (withPadding ? 27 : 0),
                Right = x + width + (withPadding ? -3 : 0),
                Bottom = y + height + (withPadding ? -3 : 0)
            };
            ClipCursor(ref CurrentClipRect);
        }

        public static void ReleaseCursor()
        {
            CurrentClipRect = RECT.Zero;
            ClipCursor(IntPtr.Zero);
        }

        public static IntPtr SetKeyboardHook(LowLevelKeyboardProc proc)
        {
            using Process curProcess = Process.GetCurrentProcess();
            using ProcessModule curModule = curProcess.MainModule!;
            return SetWindowsHookEx(WH_KEYBOARD_LL, proc, GetModuleHandle(curModule.ModuleName), 0);
        }

        public static IntPtr SetMouseHook(LowLevelMouseProc proc)
        {
            using Process curProcess = Process.GetCurrentProcess();
            using ProcessModule curModule = curProcess.MainModule!;
            return SetWindowsHookEx(WH_MOUSE_LL, proc, GetModuleHandle(curModule.ModuleName), 0);
        }
    }
}