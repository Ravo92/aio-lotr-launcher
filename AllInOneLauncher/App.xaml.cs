using System;
using System.Linq;
using System.Windows;
using System.Threading;
using System.Diagnostics;
using AllInOneLauncher.Logic;
using System.Collections.Immutable;
using AllInOneLauncher.Data;
using WindowsShortcutFactory;
using System.IO;

namespace AllInOneLauncher
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        internal static Mutex? Mutex;
        public static string[] Args = [];

        protected override void OnStartup(StartupEventArgs e)
        {
            Mutex = new Mutex(true, Constants.C_MUTEX_NAME, out bool launcherNotOpenAlready);
            bool launcherOpenAlready = !launcherNotOpenAlready;
            Args = Environment.GetCommandLineArgs().Skip(1).ToArray();

            try
            {
                if (File.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Patch 2.22 Launcher.lnk")))
                    File.Delete(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Patch 2.22 Launcher.lnk"));

                using var shortcut = new WindowsShortcut
                {
                    Path = Process.GetCurrentProcess().MainModule!.FileName ?? "",
                    Description = "All-in-One Launcher by Ravo92 & the Bfme Foundation Project"
                };

                shortcut.Save(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Patch 2.22 Launcher.lnk"));
            }
            catch { }

            base.OnStartup(e);

            if (launcherOpenAlready)
            {
                Process currentProcess = Process.GetCurrentProcess();
                Process.GetProcessesByName(currentProcess.ProcessName).Where(x => x.Id != currentProcess.Id).ToImmutableList().ForEach(x => SystemInputManager.ActivateWindow(x.MainWindowHandle));
                Current.Shutdown();
            }
            else
            {
                App.Current.Resources["VisibleIfNotElevated"] = LauncherStateManager.IsElevated ? Visibility.Collapsed : Visibility.Visible;
                _ = new MainWindow();
            }
        }

        protected override void OnExit(ExitEventArgs e)
        {
            if (Mutex is not null)
            {
                if (Mutex.WaitOne(TimeSpan.Zero, true))
                    Mutex.ReleaseMutex();

                Mutex.Dispose();
            }

            base.OnExit(e);
        }

        public static void ExitImmediately()
        {
            Mutex?.Dispose();
            Mutex = null;
            Environment.Exit(0);
        }
    }
}