using LauncherGUI.Helpers;
using System.Runtime.InteropServices;
using System;
using System.Threading;
using System.Windows;
using System.Diagnostics;
using System.IO;

namespace LauncherGUI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        [LibraryImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static partial bool SetForegroundWindow(IntPtr hWnd);

        private Mutex? _mutex;

        protected override void OnStartup(StartupEventArgs eventArgs)
        {
            bool createdNew;
            _mutex = new(true, ConstStringsHelper.C_MUTEX_NAME, out createdNew);

            if (createdNew)
            {
                GameFileToolsHelper.CheckForInstalledGames();

                MainWindow mainWindow = new();
                mainWindow.Show();
            }
            else
            {
                Process current = Process.GetCurrentProcess();
                foreach (Process process in Process.GetProcessesByName(current.ProcessName))
                {
                    if (process.Id != current.Id)
                    {
                        SetForegroundWindow(process.MainWindowHandle);
                        break;
                    }
                }
                Current.Shutdown();
            }
        }

        protected override void OnExit(ExitEventArgs e)
        {
            if (_mutex is not null)
            {
                if (_mutex.WaitOne(TimeSpan.Zero, true))
                    _mutex.ReleaseMutex();

                _mutex.Dispose();
            }
            base.OnExit(e);
        }
    }
}