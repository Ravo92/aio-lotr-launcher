using Serilog;

namespace Helper
{
    public class LogHelper
    {
        public static readonly ILogger LoggerBFME1GUI = new LoggerConfiguration().MinimumLevel.Information().WriteTo.File(Path.Combine(ConstStrings.C_LOGFOLDER_NAME, ConstStrings.C_LOGFILE_BFME1LAUNCHER_NAME)).CreateLogger();
        public static readonly ILogger LoggerBFME2GUI = new LoggerConfiguration().MinimumLevel.Information().WriteTo.File(Path.Combine(ConstStrings.C_LOGFOLDER_NAME, ConstStrings.C_LOGFILE_BFME2LAUNCHER_NAME)).CreateLogger();
        public static readonly ILogger LoggerBFME25GUI = new LoggerConfiguration().MinimumLevel.Information().WriteTo.File(Path.Combine(ConstStrings.C_LOGFOLDER_NAME, ConstStrings.C_LOGFILE_BFME25LAUNCHER_NAME)).CreateLogger();
        public static readonly ILogger LoggerRestarter = new LoggerConfiguration().MinimumLevel.Information().WriteTo.File(Path.Combine(ConstStrings.C_LOGFOLDER_NAME, ConstStrings.C_LOGFILE_RESTARTER_NAME)).CreateLogger();

        public static readonly ILogger LoggerWebView2 = new LoggerConfiguration().MinimumLevel.Information().WriteTo.File(Path.Combine(ConstStrings.C_LOGFOLDER_NAME, ConstStrings.C_LOGFILE_WEBVIEW2_NAME)).CreateLogger();

        public static readonly ILogger LoggerZipFile = new LoggerConfiguration().MinimumLevel.Information().WriteTo.File(Path.Combine(ConstStrings.C_LOGFOLDER_NAME, ConstStrings.C_LOGFILE_ZIPFILE_NAME)).CreateLogger();
        public static readonly ILogger LoggerGameFileTools = new LoggerConfiguration().MinimumLevel.Information().WriteTo.File(Path.Combine(ConstStrings.C_LOGFOLDER_NAME, ConstStrings.C_LOGFILE_GAMEFILETOOLS_NAME)).CreateLogger();
        public static readonly ILogger LoggerPatchModDectection = new LoggerConfiguration().MinimumLevel.Information().WriteTo.File(Path.Combine(ConstStrings.C_LOGFOLDER_NAME, ConstStrings.C_LOGFILE_PATCHMODDETECTION_NAME)).CreateLogger();
        public static readonly ILogger LoggerGRepairFile = new LoggerConfiguration().MinimumLevel.Information().WriteTo.File(Path.Combine(ConstStrings.C_LOGFOLDER_NAME, ConstStrings.C_LOGFILE_REPAIRFILE_NAME)).CreateLogger();
        public static readonly ILogger LoggerShortcuts = new LoggerConfiguration().MinimumLevel.Information().WriteTo.File(Path.Combine(ConstStrings.C_LOGFOLDER_NAME, ConstStrings.C_LOGFILE_SHORTCUTS_NAME)).CreateLogger();
    }
}