using Helper;
using Microsoft.Web.WebView2.Core;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PatchLauncher
{
    class WebView2Helper
    {
        public static async void InitializeWebView2Settings()
        {
            try
            {
                File.WriteAllText(Path.Combine(Application.StartupPath, ConstStrings.C_LOGFOLDER_NAME, "webView2_Version.log"), CoreWebView2Environment.GetAvailableBrowserVersionString());
            }
            catch (WebView2RuntimeNotFoundException)
            {
                await RunWebViewSilentSetupAsync(Path.Combine(Application.StartupPath, ConstStrings.C_TOOLFOLDER_NAME, "MicrosoftEdgeWebview2Setup.exe"));
            }
        }

        private static async Task RunWebViewSilentSetupAsync(string fileName)
        {
            using Process _p = Process.Start(fileName, new[] { "/silent", "/install" });
            await _p.WaitForExitAsync().ConfigureAwait(false);
        }
    }
}