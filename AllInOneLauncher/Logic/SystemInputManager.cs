using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace AllInOneLauncher.Logic
{
    internal class SystemInputManager
    {
        private static readonly NativeMethods.LowLevelKeyboardProc _keyboardProc = KeyboardHookCallback;
        private static readonly NativeMethods.LowLevelMouseProc _mouseProc = MouseHookCallback;
        private static IntPtr _keyboardHookID = IntPtr.Zero;
        private static IntPtr _mouseHookID = IntPtr.Zero;

        private static NativeMethods.RECT CurrentClipRect;

        public static void Init()
        {
            _keyboardHookID = NativeMethods.SetKeyboardHook(_keyboardProc);
            _mouseHookID = NativeMethods.SetMouseHook(_mouseProc);
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
            return NativeMethods.CallNextHookEx(_keyboardHookID, nCode, wParam, lParam);
        }

        private static IntPtr MouseHookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0)
            {
                NativeMethods.MSLLHOOKSTRUCT hookStruct = (NativeMethods.MSLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(NativeMethods.MSLLHOOKSTRUCT));
                if (!CurrentClipRect.IsZero() && CurrentClipRect.Contains(hookStruct.pt))
                {
                    NativeMethods.ClipCursor(ref CurrentClipRect);
                }
            }
            return NativeMethods.CallNextHookEx(_mouseHookID, nCode, wParam, lParam);
        }

        public static void ConfineCursor(int x, int y, int width, int height, bool withPadding)
        {
            CurrentClipRect = new NativeMethods.RECT
            {
                Left = x + (withPadding ? 3 : 0),
                Top = y + (withPadding ? 27 : 0),
                Right = x + width + (withPadding ? -3 : 0),
                Bottom = y + height + (withPadding ? -3 : 0)
            };
            NativeMethods.ClipCursor(ref CurrentClipRect);
        }

        public static void ReleaseCursor()
        {
            CurrentClipRect = NativeMethods.RECT.Zero;
            NativeMethods.ClipCursor(IntPtr.Zero);
        }
    }
}