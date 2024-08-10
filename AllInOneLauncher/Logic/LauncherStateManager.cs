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
    internal static class LauncherStateManager
    {
        internal static Dictionary<string, Type>? TypeMap { get; private set; }
        internal static bool IsElevated => new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator);

        internal static void Init()
        {
            if (!File.Exists(ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoamingAndLocal).FilePath))
            {
                Properties.Settings.Default.Upgrade();
                Properties.Settings.Default.Reload();
                Properties.Settings.Default.Save();
            }

            TypeMap = Assembly.GetExecutingAssembly().GetTypes().DistinctBy(x => x.Name).ToDictionary(x => x.Name, x => x);

            if (Properties.Settings.Default.LauncherLanguageSetting == -1)
                Language = 0;
            else
                Language = Properties.Settings.Default.LauncherLanguageSetting;
        }

        internal static bool Visible
        {
            get => MainWindow.Instance!.ShowInTaskbar;
            set
            {
                if (value && MainWindow.Instance!.WindowState == WindowState.Minimized)
                {
                    MainWindow.Instance!.WindowState = WindowState.Normal;
                    MainWindow.Instance!.ShowInTaskbar = true;
                    MainWindow.Instance!.Activate();
                }
                else if (!value && MainWindow.Instance!.WindowState == WindowState.Normal || MainWindow.Instance!.WindowState == WindowState.Maximized)
                {
                    MainWindow.Instance!.WindowState = WindowState.Minimized;
                    MainWindow.Instance!.ShowInTaskbar = false;
                }

                MainWindow.Instance!.TrayIcon.Visibility = value ? Visibility.Collapsed : Visibility.Visible;
            }
        }

        private static int _language;
        internal static int Language
        {
            get => _language;
            set
            {
                _language = value;

                var languageDictionary = new Dictionary<int, string>
                {
                    { 0, "LanguageResources.en.xaml" },
                    { 1, "LanguageResources.de.xaml" },
                    { 2, "LanguageResources.fr.xaml" },
                    { 3, "LanguageResources.it.xaml" },
                    { 4, "LanguageResources.es.xaml" },
                    { 5, "LanguageResources.sv.xaml" },
                    { 6, "LanguageResources.nl.xaml" },
                    { 7, "LanguageResources.pl.xaml" },
                    { 8, "LanguageResources.no.xaml" },
                    { 9, "LanguageResources.ru.xaml" }
                };

                ResourceDictionary resourceDictionary = [];
                if (languageDictionary.TryGetValue(value, out string? resourceFileName))
                {
                    resourceDictionary.Source = new Uri($"..\\..\\..\\Resources\\Dictionary\\{resourceFileName}", UriKind.Relative);
                    Application.Current.Resources.MergedDictionaries.Add(resourceDictionary);
                }
                else
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "Invalid language selection.");
                }

                Properties.Settings.Default.LauncherLanguageSetting = value;
                Properties.Settings.Default.Save();
            }
        }

        internal static void AsElevated(Action action)
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
