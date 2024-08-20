using AllInOneLauncher.Logic;
using AllInOneLauncher.Pages.Primary;
using AllInOneLauncher.Popups;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Controls;

namespace AllInOneLauncher.Elements
{
    public abstract class PopupBody : UserControl
    {
        public Action<string[]>? OnSubmited;
        public Action? ClosePopup;

        public void Submit(params string[] data)
        {
            OnSubmited?.Invoke(data);
            ClosePopup?.Invoke();
        }

        public void SubmitAsElevated(params string[] data)
        {
            if (LauncherStateManager.IsElevated)
            {
                OnSubmited?.Invoke(data);
                ClosePopup?.Invoke();
            }
            else
            {
                App.Mutex?.Dispose();
                App.Mutex = null;

                ProcessStartInfo debug = new()
                {
                    UseShellExecute = true,
                    WorkingDirectory = Path.GetFullPath("./"),
                    FileName = Path.Combine(Path.GetFullPath("./"), "AllInOneLauncher.exe"),
                    Verb = "runas"
                };
                debug.ArgumentList.Add("--Game");
                debug.ArgumentList.Add(Offline.Instance.gameTabs.SelectedIndex.ToString());
                debug.ArgumentList.Add($"--{this.GetType().Name}");
                foreach (string a in data) debug.ArgumentList.Add(a);
                Process.Start(debug);

                Environment.Exit(0);
            }
        }

        public void Dismiss() => ClosePopup?.Invoke();
    }
}
