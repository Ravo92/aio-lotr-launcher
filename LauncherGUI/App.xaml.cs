using System;
using System.Linq;
using System.Windows;
using System.Threading;
using System.Diagnostics;
using LauncherGUI.Logic;
using System.Collections.Immutable;
using LauncherGUI.Data;

namespace LauncherGUI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        internal static Mutex? Mutex;

        protected override void OnStartup(StartupEventArgs e)
        {
            Mutex = new Mutex(true, Constants.C_MUTEX_NAME, out bool launcherNotOpenAlready);
            bool launcherOpenAlready = !launcherNotOpenAlready;

            base.OnStartup(e);

            if (launcherOpenAlready)
            {
                Process currentProcess = Process.GetCurrentProcess();
                Process.GetProcessesByName(currentProcess.ProcessName).Where(x => x.Id != currentProcess.Id).ToImmutableList().ForEach(x => SystemWindowManager.ActivateWindow(x.MainWindowHandle));
                Current.Shutdown();
            }
            else
            {
                new MainWindow(Environment.GetCommandLineArgs().Skip(1).ToArray());
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
    }
}