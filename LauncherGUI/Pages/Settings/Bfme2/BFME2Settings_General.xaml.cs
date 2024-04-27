using LauncherGUI.Helpers;
using System;
using System.IO;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;
using System.Security.Principal;
using static LauncherGUI.Helpers.GameSelectorHelper;

namespace LauncherGUI.Pages.Settings.Launcher
{
    public partial class BFME2Settings_General : UserControl
    {
        private bool _isNotUserInteractionForLanguageDropDown = true;
        public static bool IsElevated => new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator);

        public BFME2Settings_General()
        {
            InitializeComponent();

            if (!GameFileToolsHelper.BFMEAppDataFolderExists(AvailableBFMEGames.BFME2))
                GameFileToolsHelper.CreateBFMEAppDataFolder(AvailableBFMEGames.BFME2);

            if (!GameFileToolsHelper.BFMEOptionsIniFileExists(AvailableBFMEGames.BFME2))
            {
                GameFileToolsHelper.CreateBFMEOptionsIniFile(AvailableBFMEGames.BFME2);
                BFMEIniEditorHelper.SetDefaultOptionsFile(AvailableBFMEGames.BFME2);
            }

            InitializeWindowElements();
        }

        private void InitializeWindowElements()
        {
            ComboBoxResolution.ItemsSource = DesktopResolutionHelper.GetAllSupportedResolutions();
            ComboBoxResolution.SelectedItem = !string.IsNullOrEmpty(Properties.Settings.Default.BFME2ResolutionSetting) ? Properties.Settings.Default.BFME2ResolutionSetting : ComboBoxResolution.Items[^1];
            ComboBoxLanguage.SelectedIndex = Properties.Settings.Default.BFME2LanguageSetting != 0 ? Properties.Settings.Default.BFME2LanguageSetting : 0;

            if (BFMERegistryHelper.ReadRegKeyBFME2("cdKey") is not null)
            {
                string cdKey = BFMERegistryHelper.ReadRegKeyBFME2("cdKey");
                TextBoxCDKey.Text = string.Join("-", Enumerable.Range(0, cdKey.Length / 4).Select(i => cdKey.Substring(i * 4, 4)));
            }

            TextBoxCDKey.MaxLength = 24;

            if (IsElevated)
            {
                TextBoxCDKey.IsReadOnly = false;
                ButtonChangeCdKey.Content = Application.Current.FindResource("SettingsBFMEGeneralCDKeyButtonTextGenerate");
            }
            else
            {
                TextBoxCDKey.IsReadOnly = true;
                TextBoxCDKey.Foreground = Brushes.DimGray;
            }
        }

        private void ComboBoxLanguage_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_isNotUserInteractionForLanguageDropDown)
            {
                _isNotUserInteractionForLanguageDropDown = false;
                return;
            }
            else
            {
                SaveLanguageSettings();
            }
        }

        private void ComboBoxResolution_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SaveResolutionSettings();
        }

        private void SaveLanguageSettings()
        {
            Properties.Settings.Default.BFME2LanguageSetting = ComboBoxLanguage.SelectedIndex;
            Properties.Settings.Default.Save();
        }

        private void SaveResolutionSettings()
        {
            Properties.Settings.Default.BFME2ResolutionSetting = ComboBoxResolution.SelectedItem?.ToString();
            BFMEIniEditorHelper.WriteKey("Resolution", ComboBoxResolution.SelectedValue?.ToString() ?? string.Empty, AvailableBFMEGames.BFME2);
            Properties.Settings.Default.Save();
        }

        private void BFME2ChildSettingsWindow_Unloaded(object sender, System.Windows.RoutedEventArgs e)
        {
            if (IsElevated)
                BFMERegistryHelper.SetRegKey(@"SOFTWARE\WOW6432Node\Electronic Arts\Electronic Arts\The Battle for Middle-earth II\ergc", "", TextBoxCDKey.Text.Replace("-", ""));
        }

        private void ButtonChangeCdKey_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (IsElevated)
            {
                string cdKey = BFMERegistryHelper.RandomCDKeyGenerator(20);
                TextBoxCDKey.Text = string.Join("-", Enumerable.Range(0, cdKey.Length / 4).Select(i => cdKey.Substring(i * 4, 4)));
            }
            else
            {
                ProcessStartInfo startInfo = new()
                {
                    UseShellExecute = true,
                    WorkingDirectory = Environment.CurrentDirectory,
                    FileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "LauncherDriver.exe"),
                    Arguments = "--SetKeyBFME2",
                    Verb = "runas"
                };
                Process.Start(startInfo);

                Environment.Exit(0);
            }
        }

        private void TextBoxCDKey_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox? textBox = sender as TextBox;
            string input = new(textBox!.Text.Where(char.IsLetterOrDigit).ToArray());

            if (input.Length > 0)
            {
                input = input.ToUpper();
                input = string.Join("-", Enumerable.Range(0, (input.Length + 3) / 4).Select(i => input.Substring(i * 4, Math.Min(4, input.Length - i * 4))));
            }

            textBox.TextChanged -= TextBoxCDKey_TextChanged;
            textBox.Text = input;
            textBox.CaretIndex = input.Length;
            textBox.TextChanged += TextBoxCDKey_TextChanged;
        }
    }
}