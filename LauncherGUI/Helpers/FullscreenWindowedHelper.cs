using System;
using System.Windows;
using System.Diagnostics;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace LauncherGUI.Helpers
{
    internal class FullscreenWindowedHelper
    {
        internal const int xDefaultRes = 1024;
        internal const int yDefaultRes = 768;

        [DllImport("shell32.dll")]
        internal static extern bool IsUserAnAdmin();
        [DllImport("user32.dll")]
        private static extern int ShowWindow(IntPtr hWnd, uint Msg);
        [DllImport("user32.dll", EntryPoint = "GetWindowLong")]
        private static extern int GetWindowLongPtr(IntPtr hWnd, int nIndex);
        [DllImport("user32.dll", EntryPoint = "SetWindowLong")]
        private static extern int SetWindowLongPtr(IntPtr hWnd, int nIndex, uint dwNewLong);
        [DllImport("user32.dll")]
        private static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);
        [DllImport("user32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);
        [DllImport("user32.dll")]
        private static extern bool SetWindowText(IntPtr hWnd, string title);
        [DllImport("user32.dll")]
        private static extern bool GetWindowRect(IntPtr hwnd, ref Rect rectangle);

        private static readonly uint SW_RESTORE = 0x09;

        private static readonly int GWL_STYLE = -16;
        private static readonly int WS_BORDER = 0x00800000;
        private static readonly int WS_THICKFRAME = 0x00040000;
        private static readonly int WS_MINIMIZEBOX = 0x00020000;
        private static readonly int WS_MAXIMIZEBOX = 0x00010000;
        private static readonly int WS_SYSMENU = 0x00800000;
        private static readonly int WS_DLGFRAME = 0x00400000;

        private static readonly uint SWP_NOZORDER = 0x0004;

        /// <summary>
        /// A helper method that calls other methods internally
        /// </summary>ram>
        /// <returns></returns>
        internal static void GoBorderless(IntPtr handle, int xPos, int yPos, int xRes, int yRes, bool removeBorder = true)
        {
            if (WindowIsFullScreen(handle))
                return;

            RestoreWindow(handle);

            if (removeBorder)
                SetBorderless(handle);

            SetWindowPos(handle, handle, xPos, yPos, xRes, yRes, SWP_NOZORDER);
            SetForeground(handle);
        }

        internal static int RestoreWindow(IntPtr handle)
        {
            return ShowWindow(handle, SW_RESTORE);
        }

        internal static int SetBorderless(IntPtr handle)
        {
            long currentStyle = GetWindowLongPtr(handle, GWL_STYLE);
            currentStyle &= ~(WS_BORDER | WS_DLGFRAME | WS_THICKFRAME | WS_MINIMIZEBOX | WS_MAXIMIZEBOX | WS_SYSMENU);
            return SetWindowLongPtr(handle, GWL_STYLE, (uint)currentStyle);
        }

        internal static bool SetForeground(IntPtr handle)
        {
            return SetForegroundWindow(handle);
        }

        internal static int GetScreenResolutionX()
        {
            return Convert.ToInt32(SystemParameters.PrimaryScreenWidth);
        }

        internal static int GetScreenResolutionY()
        {
            return Convert.ToInt32(SystemParameters.PrimaryScreenHeight);
        }

        private static bool WindowIsFullScreen(IntPtr handle)
        {
            Rect rect = default;
            GetWindowRect(handle, ref rect);
            return rect.Left == -32000 && rect.Top == -32000;
        }

        internal static Process GetProcessByFileName(string fileName)
        {
            Process[] processes = Process.GetProcesses();
            foreach (Process process in processes)
            {
                try
                {
                    if (process.MainModule!.FileName.Contains(fileName))
                    {
                        return process;
                    }
                }
                catch (Win32Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            return null;
        }
    }

    /// <summary>
    /// A window rectangle
    /// </summary>
    internal struct Rect
    {
        public int Left { get; set; }
        public int Top { get; set; }
        public int Right { get; set; }
        public int Bottom { get; set; }
    }
}
