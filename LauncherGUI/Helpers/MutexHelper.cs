using System.Diagnostics;
using System;
using System.Threading;
using System.Runtime.InteropServices;

namespace LauncherGUI.Helpers
{
    internal static class MutexHelper
    {
        internal static Mutex _mutex;
        internal static bool MutexAlreadyExists { get; set; }

        [DllImport("user32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);
        [DllImport("user32.dll")]
        private static extern bool IsIconic(IntPtr hWnd);
        [DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        private const int SW_RESTORE = 9;
        internal static bool MutexExists()
        {
            bool createdNew;
            Mutex mutex = new(true, ConstStringsHelper.C_MUTEX_NAME, out createdNew);

            if (!createdNew)
            {
                mutex.Dispose();
                return true;
            }
            else
            {
                mutex.ReleaseMutex();
                mutex.Dispose();
                return false;
            }
        }

        internal static void BringApplicationInFrontOrCreateNewMutex()
        {
            if (MutexExists())
            {
                try
                {
                    Process currentProcess = Process.GetCurrentProcess();
                    Process[] processes = Process.GetProcessesByName(currentProcess.ProcessName);

                    foreach (Process process in processes)
                    {
                        if (process.Id != currentProcess.Id)
                        {
                            IntPtr hWnd = process.MainWindowHandle;
                            if (hWnd != IntPtr.Zero)
                            {
                                if (IsIconic(hWnd))
                                {
                                    ShowWindow(hWnd, SW_RESTORE);
                                }
                                else
                                {
                                    SetForegroundWindow(hWnd);
                                }
                                break;
                            }
                        }
                    }

                    MutexAlreadyExists = true;
                }
                catch (Exception ex)
                {
                    LogHelper.LoggerGUI.Error(ex, "Mutex Related Error!");
                }
            }
            else
            {
                MutexAlreadyExists = false;
                _mutex = new(true, ConstStringsHelper.C_MUTEX_NAME);
            }
        }
    }
}
