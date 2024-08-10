using System;
using System.Runtime.InteropServices;
using static AllInOneLauncher.Logic.NativeMethods;

namespace AllInOneLauncher.Logic
{
    public static class SystemWindowManager
    {
        private static int previousState = 1;

        public static void UpdateWindow(IntPtr handle, int xPos, int yPos, int xRes, int yRes, bool removeBorder = true)
        {
            if (IsWindowFullscreen(handle))
                return;

            previousState = ShowWindow(handle, SW_RESTORE);

            if (previousState == 0)
            {

            }

            if (removeBorder)
            {
                long currentStyle = GetWindowLongPtr(handle, GWL_STYLE);
                currentStyle &= ~(WS_BORDER | WS_DLGFRAME | WS_THICKFRAME | WS_MINIMIZEBOX | WS_MAXIMIZEBOX | WS_SYSMENU);
                IntPtr previousStyle = SetWindowLongPtr(handle, GWL_STYLE, (uint)currentStyle);

                if (previousStyle == IntPtr.Zero)
                {
                    int error = Marshal.GetLastWin32Error();
                    if (error != 0)
                    {
                        throw new System.ComponentModel.Win32Exception(error, "SetWindowLongPtr failed.");
                    }
                }
            }

            SetWindowPos(handle, handle, xPos, yPos, xRes, yRes, SWP_NOZORDER);
            SetForegroundWindow(handle);
        }

        public static void ActivateWindow(IntPtr handle)
        {
            previousState = ShowWindow(handle, SW_RESTORE);

            if (previousState == 0)
            {

            }

            SetForegroundWindow(handle);
            BringWindowToTop(handle);
        }

        public static bool IsWindowFullscreen(IntPtr handle)
        {
            Rect rect = default;
            GetWindowRect(handle, ref rect);
            return rect.Left == -32000 && rect.Top == -32000;
        }
    }
}
