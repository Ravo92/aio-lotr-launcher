using System;
using System.Windows;

namespace AllInOneLauncher.Logic
{
    public static class SystemWindowManager
    {
        public static void UpdateWindow(IntPtr handle, int xPos, int yPos, int xRes, int yRes, bool removeBorder = true)
        {
            if (IsWindowFullscreen(handle))
                return;

            NativeMethods.ShowWindow(handle, NativeMethods.SW_RESTORE);

            if (removeBorder)
            {
                long currentStyle = NativeMethods.GetWindowLongPtr(handle, NativeMethods.GWL_STYLE);
                currentStyle &= ~(NativeMethods.WS_BORDER | NativeMethods.WS_DLGFRAME | NativeMethods.WS_THICKFRAME | NativeMethods.WS_MINIMIZEBOX | NativeMethods.WS_MAXIMIZEBOX | NativeMethods.WS_SYSMENU);
                NativeMethods.SetWindowLongPtr(handle, NativeMethods.GWL_STYLE, (uint)currentStyle);
            }

            NativeMethods.SetWindowPos(handle, handle, xPos, yPos, xRes, yRes, NativeMethods.SWP_NOZORDER);
            NativeMethods.SetForegroundWindow(handle);
        }

        public static void ActivateWindow(IntPtr handle)
        {
            NativeMethods.ShowWindow(handle, NativeMethods.SW_RESTORE);
            NativeMethods.SetForegroundWindow(handle);
            NativeMethods.BringWindowToTop(handle);
        }

        public static bool IsWindowFullscreen(IntPtr handle)
        {
            Rect rect = default;
            NativeMethods.GetWindowRect(handle, ref rect);
            return rect.Left == -32000 && rect.Top == -32000;
        }
    }
}
