using AllInOneLauncher.Pages.Primary;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Principal;
using System.Windows;

namespace AllInOneLauncher.Logic
{
    public static class LauncherStateManager
    {
        internal static Dictionary<string, Type> TypeMap = [];
        public static bool IsElevated => new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator);

        public static void Init()
        {
            if (!File.Exists(ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoamingAndLocal).FilePath))
            {
                Properties.Settings.Default.Upgrade();
                Properties.Settings.Default.Reload();
                Properties.Settings.Default.Save();
            }

            TypeMap = Assembly.GetExecutingAssembly().GetTypes().DistinctBy(x => x.Name).ToDictionary(x => x.Name, x => x);
            Language = Properties.Settings.Default.LauncherLanguageSetting;
        }

        public static bool Visible
        {
            get => MainWindow.Instance!.ShowInTaskbar;
            set
            {
                if(value && MainWindow.Instance!.WindowState == WindowState.Minimized)
                {
                    MainWindow.Instance!.WindowState = WindowState.Normal;
                    MainWindow.Instance!.ShowInTaskbar = true;
                    MainWindow.Instance!.Activate();
                }
                else if(!value && MainWindow.Instance!.WindowState == WindowState.Normal || MainWindow.Instance!.WindowState == WindowState.Maximized)
                {
                    MainWindow.Instance!.WindowState = WindowState.Minimized;
                    MainWindow.Instance!.ShowInTaskbar = false;
                }

                MainWindow.Instance!.TrayIcon.Visibility = value ? Visibility.Collapsed : Visibility.Visible;
            }
        }

        public static int Language
        {
            get => Properties.Settings.Default.LauncherLanguageSetting;
            set
            {
                Properties.Settings.Default.LauncherLanguageSetting = value;
                Properties.Settings.Default.Save();

                ResourceDictionary resourceDictionary = [];
                switch (value)
                {
                    case 0:
                        resourceDictionary.Source = new Uri("..\\..\\..\\Resources\\Dictionary\\LanguageResources.en.xaml", UriKind.Relative);
                        break;
                    case 1:
                        resourceDictionary.Source = new Uri("..\\..\\..\\Resources\\Dictionary\\LanguageResources.de.xaml", UriKind.Relative);
                        break;
                }
                Application.Current.Resources.MergedDictionaries.Add(resourceDictionary);
            }
        }

        public static void AsElevated(Action action)
        {
            if (IsElevated)
            {
                action?.Invoke();
            }
            else
            {
                App.Mutex?.Dispose();
                App.Mutex = null;

                string serializedState = "";

                if (MainWindow.Instance!.fullContent.Child is Settings)
                    serializedState = $"--Settings {((Settings)MainWindow.Instance!.fullContent.Child!).Page}";
                else if (MainWindow.Instance!.content.Child is Offline)
                    serializedState = $"--Game {Pages.Primary.Offline.Instance.gameTabs.SelectedIndex}";
                else if (MainWindow.Instance!.content.Child is Online)
                    serializedState = "--Online";

                Process.Start(new ProcessStartInfo()
                {
                    UseShellExecute = true,
                    WorkingDirectory = Path.GetFullPath("./"),
                    FileName = Path.Combine(Path.GetFullPath("./"), "AllInOneLauncher.exe"),
                    Arguments = serializedState,
                    Verb = "runas"
                });

                Environment.Exit(0);
            }
        }
    }
}
