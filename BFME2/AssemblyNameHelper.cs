using System;
using System.Reflection;

namespace PatchLauncher
{
    internal class AssemblyNameHelper
    {
        internal static readonly string BFMELauncherGameName = AssemblyName.GetAssemblyName(Assembly.GetExecutingAssembly().Location).Name!;
        internal static readonly Version BFMELauncherGameVersion = Assembly.GetEntryAssembly()!.GetName().Version!;
        internal static int ExternalInstallerReturnCode { get; set; }
        internal static bool EAXWasActivated { get; set; }
        internal static bool ThirdPartyToolExecutableMissing { get; set; }
    }
}