namespace LauncherDriver
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : System.Windows.Application
    {
        private readonly string workingDirectory = AppDomain.CurrentDomain.BaseDirectory;

        protected override void OnStartup(System.Windows.StartupEventArgs e)
        {
            base.OnStartup(e);

            List<string> allowedArguments = ["--SetKeyBFME1", "--SetKeyBFME2", "--SetKeyROTWK", "--OnlineMode"];
            string[] arguments = Environment.GetCommandLineArgs().Skip(1).ToArray();
            string wpfAppPath = System.IO.Path.Combine(workingDirectory, "LauncherGUI.exe");

            foreach (var argument in arguments)
            {
                if (allowedArguments.Contains(argument))
                {
                    System.Diagnostics.Process.Start(wpfAppPath, argument);
                    break;
                }
            }

            Environment.Exit(0 );
        }
    }
}