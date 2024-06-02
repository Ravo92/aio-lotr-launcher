using System;
using System.Linq;
using System.Windows;
using System.Threading;
using System.Diagnostics;
using LauncherGUI.Helpers;
using System.Runtime.InteropServices;
using System.Collections.Generic;

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

        protected override void OnStartup(StartupEventArgs e)
        {
            bool createdNew;
            _mutex = new(true, ConstStringsHelper.C_MUTEX_NAME, out createdNew);

            base.OnStartup(e);

            List<string> allowedArguments = ["--SetKeyBFME1", "--SetKeyBFME2", "--SetKeyROTWK", "--OnlineMode"];
            string[] arguments = Environment.GetCommandLineArgs().Skip(1).ToArray();

            GameFileToolsHelper.CheckForInstalledGames();

            if (!createdNew && arguments.Length > 0)
            {
                foreach (var argument in arguments)
                {
                    if (allowedArguments.Contains(argument))
                    {
                        MainWindow mainWindow = new(argument);
                        mainWindow.Show();
                        return;
                    }
                }
            }
            else if (!createdNew)
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
                return;
            }
            else if (arguments.Length == 0)
            {
                MainWindow mainWindow = new("");
                mainWindow.Show();
                return;
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