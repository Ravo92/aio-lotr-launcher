namespace Helper
{
    public class UpdateRoutine
    {
        public async Task CheckForLauncherUpdates(int BetaChannelVersion)
        {
            //int XmlVersion = JSONFileHelper.GetXMLFileVersion(true);

            //if (XmlVersion > BetaChannelVersion && Settings.Default.BetaChannelVersion > 0 && Directory.Exists(Path.Combine(Application.StartupPath, ConstStrings.C_PATCHFOLDER_NAME, ConstStrings.C_BETAFOLDER_NAME + Settings.Default.BetaChannelVersion.ToString())))
            //    Directory.Delete(Path.Combine(Application.StartupPath, ConstStrings.C_PATCHFOLDER_NAME, ConstStrings.C_BETAFOLDER_NAME + Settings.Default.BetaChannelVersion.ToString()), true);

            //Task download = DownloadUpdate(Path.Combine(Application.StartupPath, ConstStrings.C_PATCHFOLDER_NAME, ConstStrings.C_BETAFOLDER_NAME + XmlVersion.ToString(), ConstStrings.C_MAIN_ASSET_FILE), "https://dl.dropboxusercontent.com/s/zmze7c5asdlq44u/asset.dat");
            //await download;

            //Task download2 = DownloadUpdate(Path.Combine(Application.StartupPath, ConstStrings.C_PATCHFOLDER_NAME, ConstStrings.C_BETAFOLDER_NAME + XmlVersion.ToString(), ConstStrings.C_TEXTURES_PATCH_FILE), "https://dl.dropboxusercontent.com/s/ok5a5507fpwmge3/_patch222textures.big");
            //await download2;

            //Task download3 = DownloadUpdate(Path.Combine(Application.StartupPath, ConstStrings.C_PATCHFOLDER_NAME, ConstStrings.C_BETAFOLDER_NAME + XmlVersion.ToString(), ConstStrings.C_MAIN_PATCH_FILE), "https://dl.dropboxusercontent.com/s/gi431h0gnqgjfh3/_patch222.big");
            //await download3;

            //Task download4 = DownloadUpdate(Path.Combine(Application.StartupPath, ConstStrings.C_PATCHFOLDER_NAME, ConstStrings.C_BETAFOLDER_NAME + XmlVersion.ToString(), ConstStrings.C_BASES_PATCH_FILE), "https://dl.dropboxusercontent.com/s/rlshiuuiaalsu1t/_patch222bases.big");
            //await download4;

            //Task download5 = DownloadUpdate(Path.Combine(Application.StartupPath, ConstStrings.C_PATCHFOLDER_NAME, ConstStrings.C_BETAFOLDER_NAME + XmlVersion.ToString(), ConstStrings.C_MAPS_PATCH_FILE), "https://dl.dropboxusercontent.com/s/b4ubdfi8uuk181m/_patch222maps.big");
            //await download5;

            //Task download6 = DownloadUpdate(Path.Combine(Application.StartupPath, ConstStrings.C_PATCHFOLDER_NAME, ConstStrings.C_BETAFOLDER_NAME + XmlVersion.ToString(), ConstStrings.C_LIBRARIES_PATCH_FILE), "https://dl.dropboxusercontent.com/s/9027uabrxkx7z85/_patch222libraries.big");
            //await download6;

            //File.Copy(Path.Combine(Application.StartupPath, ConstStrings.C_PATCHFOLDER_NAME, ConstStrings.C_BETAFOLDER_NAME + XmlVersion.ToString(), ConstStrings.C_MAIN_ASSET_FILE), Path.Combine(ConstStrings.GameInstallPath(), ConstStrings.C_MAIN_ASSET_FILE), true);
            //File.Copy(Path.Combine(Application.StartupPath, ConstStrings.C_PATCHFOLDER_NAME, ConstStrings.C_BETAFOLDER_NAME + XmlVersion.ToString(), ConstStrings.C_TEXTURES_PATCH_FILE), Path.Combine(ConstStrings.GameInstallPath(), ConstStrings.C_TEXTURES_PATCH_FILE), true);
            //File.Copy(Path.Combine(Application.StartupPath, ConstStrings.C_PATCHFOLDER_NAME, ConstStrings.C_BETAFOLDER_NAME + XmlVersion.ToString(), ConstStrings.C_MAIN_PATCH_FILE), Path.Combine(ConstStrings.GameInstallPath(), ConstStrings.C_MAIN_PATCH_FILE), true);
            //File.Copy(Path.Combine(Application.StartupPath, ConstStrings.C_PATCHFOLDER_NAME, ConstStrings.C_BETAFOLDER_NAME + XmlVersion.ToString(), ConstStrings.C_BASES_PATCH_FILE), Path.Combine(ConstStrings.GameInstallPath(), ConstStrings.C_BASES_PATCH_FILE), true);
            //File.Copy(Path.Combine(Application.StartupPath, ConstStrings.C_PATCHFOLDER_NAME, ConstStrings.C_BETAFOLDER_NAME + XmlVersion.ToString(), ConstStrings.C_LIBRARIES_PATCH_FILE), Path.Combine(ConstStrings.GameInstallPath(), ConstStrings.C_LIBRARIES_PATCH_FILE), true);
            //File.Copy(Path.Combine(Application.StartupPath, ConstStrings.C_PATCHFOLDER_NAME, ConstStrings.C_BETAFOLDER_NAME + XmlVersion.ToString(), ConstStrings.C_MAPS_PATCH_FILE), Path.Combine(ConstStrings.GameInstallPath(), ConstStrings.C_MAPS_PATCH_FILE), true);
            //await null;
        }
    }
}
