﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Dieser Code wurde von einem Tool generiert.
//     Laufzeitversion:4.0.30319.42000
//
//     Änderungen an dieser Datei können falsches Verhalten verursachen und gehen verloren, wenn
//     der Code erneut generiert wird.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PatchLauncher {
    using System;
    
    
    /// <summary>
    ///   Eine stark typisierte Ressourcenklasse zum Suchen von lokalisierten Zeichenfolgen usw.
    /// </summary>
    // Diese Klasse wurde von der StronglyTypedResourceBuilder automatisch generiert
    // -Klasse über ein Tool wie ResGen oder Visual Studio automatisch generiert.
    // Um einen Member hinzuzufügen oder zu entfernen, bearbeiten Sie die .ResX-Datei und führen dann ResGen
    // mit der /str-Option erneut aus, oder Sie erstellen Ihr VS-Projekt neu.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Strings {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Strings() {
        }
        
        /// <summary>
        ///   Gibt die zwischengespeicherte ResourceManager-Instanz zurück, die von dieser Klasse verwendet wird.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("PatchLauncher.Strings", typeof(Strings).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Überschreibt die CurrentUICulture-Eigenschaft des aktuellen Threads für alle
        ///   Ressourcenzuordnungen, die diese stark typisierte Ressourcenklasse verwenden.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Sucht eine lokalisierte Zeichenfolge, die INSTALL GAME ähnelt.
        /// </summary>
        internal static string BtnInstall_TextInstall {
            get {
                return ResourceManager.GetString("BtnInstall.TextInstall", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Sucht eine lokalisierte Zeichenfolge, die LAUNCH GAME ähnelt.
        /// </summary>
        internal static string BtnInstall_TextLaunch {
            get {
                return ResourceManager.GetString("BtnInstall.TextLaunch", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Sucht eine lokalisierte Zeichenfolge, die External Installer was canceled and Version will be set back to 1.06! ähnelt.
        /// </summary>
        internal static string Error_ExternalInstallerCanceled_Text {
            get {
                return ResourceManager.GetString("Error.ExternalInstallerCanceled.Text", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Sucht eine lokalisierte Zeichenfolge, die External Installer canceled ähnelt.
        /// </summary>
        internal static string Error_ExternalInstallerCanceled_Title {
            get {
                return ResourceManager.GetString("Error.ExternalInstallerCanceled.Title", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Sucht eine lokalisierte Zeichenfolge, die Copy files and apply patch... ähnelt.
        /// </summary>
        internal static string Info_CopyFilesAndApplyPatch {
            get {
                return ResourceManager.GetString("Info.CopyFilesAndApplyPatch", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Sucht eine lokalisierte Zeichenfolge, die Checking File integrity... ähnelt.
        /// </summary>
        internal static string Info_MD5Check {
            get {
                return ResourceManager.GetString("Info.MD5Check", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Sucht eine lokalisierte Zeichenfolge, die Searching for the one ring... ähnelt.
        /// </summary>
        internal static string Info_PleaseWait {
            get {
                return ResourceManager.GetString("Info.PleaseWait", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Sucht eine lokalisierte Zeichenfolge, die Preparing Setup... ähnelt.
        /// </summary>
        internal static string Info_PreparingSetup {
            get {
                return ResourceManager.GetString("Info.PreparingSetup", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Sucht eine lokalisierte Zeichenfolge, die Preparing Update... ähnelt.
        /// </summary>
        internal static string Info_PreparingUpdate {
            get {
                return ResourceManager.GetString("Info.PreparingUpdate", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Sucht eine lokalisierte Zeichenfolge, die Seems like we lost the game itself or a third party patch- or mod-launcher
        ///Its greatly advised, that you dont install any third party launcher into the game folder itself!
        ///Do you want to repair the game now, so we got a clean 1.06 state again? ähnelt.
        /// </summary>
        internal static string Msg_ErrorStartingGame_Text {
            get {
                return ResourceManager.GetString("Msg.ErrorStartingGame.Text", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Sucht eine lokalisierte Zeichenfolge, die There is a problem! ähnelt.
        /// </summary>
        internal static string Msg_ErrorStartingGame_Title {
            get {
                return ResourceManager.GetString("Msg.ErrorStartingGame.Title", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Sucht eine lokalisierte Zeichenfolge, die The target directory is not empty. Continue installing the game?
        ///Exisiting files will be deleted! ähnelt.
        /// </summary>
        internal static string Msg_InstallFolderNotEmpty_Text {
            get {
                return ResourceManager.GetString("Msg.InstallFolderNotEmpty.Text", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Sucht eine lokalisierte Zeichenfolge, die Found files in target directory ähnelt.
        /// </summary>
        internal static string Msg_InstallFolderNotEmpty_Title {
            get {
                return ResourceManager.GetString("Msg.InstallFolderNotEmpty.Title", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Sucht eine lokalisierte Zeichenfolge, die Repair finished. A protocol has been saved under &quot;Logfiles&quot;. ähnelt.
        /// </summary>
        internal static string Msg_RepairDone_Text {
            get {
                return ResourceManager.GetString("Msg.RepairDone.Text", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Sucht eine lokalisierte Zeichenfolge, die Repair finished! ähnelt.
        /// </summary>
        internal static string Msg_RepairDone_Title {
            get {
                return ResourceManager.GetString("Msg.RepairDone.Title", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Sucht eine lokalisierte Zeichenfolge, die A Launcher restart is required for the settings to be applied. Do you want to restart now? ähnelt.
        /// </summary>
        internal static string Msg_Restart_Text {
            get {
                return ResourceManager.GetString("Msg.Restart.Text", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Sucht eine lokalisierte Zeichenfolge, die Launcher Restart required ähnelt.
        /// </summary>
        internal static string Msg_Restart_Title {
            get {
                return ResourceManager.GetString("Msg.Restart.Title", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Sucht eine lokalisierte Zeichenfolge, die The selected patch or mod is already active!
        ///No action has been conducted. ähnelt.
        /// </summary>
        internal static string Msg_UpdateAlreadyActive_Text {
            get {
                return ResourceManager.GetString("Msg.UpdateAlreadyActive.Text", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Sucht eine lokalisierte Zeichenfolge, die Already activated! ähnelt.
        /// </summary>
        internal static string Msg_UpdateAlreadyActive_Title {
            get {
                return ResourceManager.GetString("Msg.UpdateAlreadyActive.Title", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Sucht eine lokalisierte Zeichenfolge, die Switch between faction music and default theme music ähnelt.
        /// </summary>
        internal static string ToolTip_MusicSwitcher {
            get {
                return ResourceManager.GetString("ToolTip.MusicSwitcher", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Sucht eine lokalisierte Zeichenfolge, die Launcher is already running! Please Check TrayIcon or Taskmanager first. ähnelt.
        /// </summary>
        internal static string Warning_AlreadyRunning {
            get {
                return ResourceManager.GetString("Warning.AlreadyRunning", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Sucht eine lokalisierte Zeichenfolge, die Launcher already running! ähnelt.
        /// </summary>
        internal static string Warning_AlreadyRunningTitle {
            get {
                return ResourceManager.GetString("Warning.AlreadyRunningTitle", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Sucht eine lokalisierte Zeichenfolge, die There is still some download or install active!
        ///Closing the Launcher now would damage the files or the game itself.
        ///Please try again, after the launcher finished its work! ähnelt.
        /// </summary>
        internal static string Warning_CantStopNow {
            get {
                return ResourceManager.GetString("Warning.CantStopNow", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Sucht eine lokalisierte Zeichenfolge, die Sadly, you cant close now! ähnelt.
        /// </summary>
        internal static string Warning_CantStopNowTitle {
            get {
                return ResourceManager.GetString("Warning.CantStopNowTitle", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Sucht eine lokalisierte Zeichenfolge, die Some settings are disabled until
        ///game ist installed! ähnelt.
        /// </summary>
        internal static string Warning_GameNotInstalled {
            get {
                return ResourceManager.GetString("Warning.GameNotInstalled", resourceCulture);
            }
        }
    }
}
