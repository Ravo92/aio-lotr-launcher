using System.Diagnostics;
using System.Windows;

namespace LauncherDriver
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly string workingDirectory = AppDomain.CurrentDomain.BaseDirectory;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            string[] args = Environment.GetCommandLineArgs();
            string argumentToFind = "--SetKey";
            string argument = "";

            // Thread.Sleep(2000);

            if (args is not null)
                argument = args.FirstOrDefault(arg => arg.Contains(argumentToFind))!;

            if (argument is not null)
            {
                string wpfAppPath = System.IO.Path.Combine(workingDirectory, "LauncherGUI.exe");
                Process.Start(wpfAppPath, argument);
            }

            Environment.Exit(0 );
        }
    }
}