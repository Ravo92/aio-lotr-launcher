using LauncherGUI.Helpers;
using System.Runtime.InteropServices;
using System;
using System.Threading;
using System.Windows;
using System.Diagnostics;
using System.Linq;
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

        protected override void OnStartup(StartupEventArgs e)
        {
            bool createdNew;
            _mutex = new(true, ConstStringsHelper.C_MUTEX_NAME, out createdNew);

            base.OnStartup(e);

            string[] args = Environment.GetCommandLineArgs();
            string argumentToFind = "--SetKey";
            string argument = "";

            if (args.Length > 1)
                argument = args.FirstOrDefault(arg => arg.Contains(argumentToFind))!;


            if (createdNew || (!createdNew && args.Length > 1))
            {
                GameFileToolsHelper.CheckForInstalledGames();

                if (argument is null)
                {
                    MainWindow mainWindow = new("");
                    mainWindow.Show();
                }
                else
                {
                    MainWindow mainWindow = new(argument);
                    mainWindow.Show();
                }
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