using Gma.System.MouseKeyHook;
using System;

namespace LauncherGUI.Logic
{
    internal class SystemInputManager
    {
        private static IKeyboardMouseEvents? Hook;
        private static RECT CurrentClipRect;

        public static void Init()
        {
            Hook = Gma.System.MouseKeyHook.Hook.GlobalEvents();
            Hook.KeyDown += (s, e) =>
            {
                if ((e.Modifiers == System.Windows.Forms.Keys.Alt && e.KeyCode == System.Windows.Forms.Keys.Tab) || e.KeyCode == System.Windows.Forms.Keys.LWin || e.KeyCode == System.Windows.Forms.Keys.RWin)
                    ReleaseCursor();
            };
            Hook.MouseClick += (s, e) =>
            {
                if (!CurrentClipRect.IsZero() && CurrentClipRect.Contains(e.Location))
                    NativeMethods.ClipCursor(ref CurrentClipRect);
            };
        }

        public static void ConfineCursor(int x, int y, int width, int height, bool withPadding)
        {
            CurrentClipRect = new()
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
            CurrentClipRect = RECT.Zero;
            NativeMethods.ClipCursor(IntPtr.Zero);
        }
    }
}