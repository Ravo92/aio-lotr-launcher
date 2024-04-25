using Serilog;
using System;
using System.IO;

namespace LauncherGUI.Helpers
{
    public class LogHelper
    {
        private static readonly string logFolderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ConstStringsHelper.C_LOGFOLDER_NAME, DateTime.Now.ToString(format: "yyyy-MM-dd"));

        public static readonly ILogger LoggerGUI = new LoggerConfiguration().MinimumLevel.Information().WriteTo.File(Path.Combine(logFolderPath, ConstStringsHelper.C_LOGFILE_BFME1LAUNCHER_NAME)).CreateLogger();
        public static readonly ILogger LoggerRestarter = new LoggerConfiguration().MinimumLevel.Information().WriteTo.File(Path.Combine(logFolderPath, ConstStringsHelper.C_LOGFILE_RESTARTER_NAME)).CreateLogger();

        public static readonly ILogger LoggerWebView2 = new LoggerConfiguration().MinimumLevel.Information().WriteTo.File(Path.Combine(logFolderPath, ConstStringsHelper.C_LOGFILE_WEBVIEW2_NAME)).CreateLogger();

        public static readonly ILogger LoggerZipFile = new LoggerConfiguration().MinimumLevel.Information().WriteTo.File(Path.Combine(logFolderPath, ConstStringsHelper.C_LOGFILE_ZIPFILE_NAME)).CreateLogger();
        public static readonly ILogger LoggerRegistryTools = new LoggerConfiguration().MinimumLevel.Information().WriteTo.File(Path.Combine(logFolderPath, ConstStringsHelper.C_LOGFILE_REGISTRY_NAME)).CreateLogger();

        public static readonly ILogger LoggerPatchModDetection = new LoggerConfiguration().MinimumLevel.Information().WriteTo.File(Path.Combine(logFolderPath, ConstStringsHelper.C_LOGFILE_PATCHMODDETECTION_NAME)).CreateLogger();
        public static readonly ILogger LoggerRepairFile = new LoggerConfiguration().MinimumLevel.Information().WriteTo.File(Path.Combine(logFolderPath, ConstStringsHelper.C_LOGFILE_REPAIRFILE_NAME)).CreateLogger();
        public static readonly ILogger LoggerShortcuts = new LoggerConfiguration().MinimumLevel.Information().WriteTo.File(Path.Combine(logFolderPath, ConstStringsHelper.C_LOGFILE_SHORTCUTS_NAME)).CreateLogger();
    }
}