using Gma.System.MouseKeyHook;
using System;
using System.Runtime.InteropServices;
using System.Windows;

namespace LauncherGUI.Helpers
{
    internal class CatchMousePointerHelper
    {
        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }

        [DllImport("user32.dll")]
        public static extern bool ClipCursor(ref RECT lpRect);

        [DllImport("user32.dll")]
        public static extern bool ClipCursor(IntPtr lpRect);

        private static IKeyboardMouseEvents? _hook;
        private static RECT _currentClipRect;
        private static bool hookDisposed = false;

        public static void ClipCursorToArea(int left, int top, int right, int bottom, bool removedBorder)
        {
            _currentClipRect = new()
            {
                Left = left,
                Top = top,
                Right = right,
                Bottom = bottom
            };

            if (!removedBorder)
            {
                _currentClipRect.Left += 3;
                _currentClipRect.Top += 27;
                _currentClipRect.Right -= 3;
                _currentClipRect.Bottom -= 3;
            }

            ClipCursor(ref _currentClipRect);
        }

        public static void UnclipCursor()
        {
            ClipCursor(IntPtr.Zero);
        }

        public static void InitializeGlobalHook()
        {
            _hook = Hook.GlobalEvents();
            _hook.KeyDown += Hook_KeyDown;
            _hook.MouseClick += Hook_MouseClick;
            hookDisposed = false;
        }

        public static void TerminateGlobalHook()
        {
            if (_hook != null)
            {
                _hook.KeyDown -= Hook_KeyDown;
                _hook.MouseClick -= Hook_MouseClick;
                _hook.Dispose();
                hookDisposed = true;
            }
        }

        private static void Hook_KeyDown(object? sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.LWin || e.KeyCode == System.Windows.Forms.Keys.RWin ||
                (e.Modifiers == System.Windows.Forms.Keys.Alt && e.KeyCode == System.Windows.Forms.Keys.Tab) ||
                (e.Modifiers == System.Windows.Forms.Keys.Control && e.Modifiers == System.Windows.Forms.Keys.Alt && e.KeyCode == System.Windows.Forms.Keys.Delete))
            {
                UnclipCursor();
            }
        }

        private static void Hook_MouseClick(object? sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (!hookDisposed)
            {
                if (IsPointWithinRect(e.Location, _currentClipRect))
                {
                    ClipCursor(ref _currentClipRect);
                }
            }
        }

        private static bool IsPointWithinRect(System.Drawing.Point point, RECT rect)
        {
            return point.X >= rect.Left && point.X <= rect.Right && point.Y >= rect.Top && point.Y <= rect.Bottom;
        }

        public static void SetupBorderlessGameWindowWithMouseClipping(IntPtr handle, int xPos, int yPos, int xRes, int yRes, bool removeBorder)
        {
            FullscreenWindowedHelper.GoBorderless(handle, xPos, yPos, xRes, yRes, removeBorder);
            InitializeGlobalHook();
            ClipCursorToArea(xPos, yPos, xPos + xRes, yPos + yRes, removeBorder);
        }
    }
}