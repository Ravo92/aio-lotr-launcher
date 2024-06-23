using System;
using System.Windows.Controls;

namespace LauncherGUI.Elements
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

        public void Dismiss() => ClosePopup?.Invoke();
    }
}
