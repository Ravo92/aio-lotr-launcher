#define MyAppName "Patch 2.22 Launcher"
#define MyAppVerName "Patch 2.22 Launcher"
#define MyAppExeName "PatchLauncherBFME.exe"
#define MyAppIcoName "PatchLauncherBFME.exe"

[Setup]
AppName={#MyAppName}
AppId=Patch 2.22 Launcher
AppVerName={#MyAppVerName}
WizardStyle=modern
DefaultDirName={userappdata}\{#MyAppName}
DefaultGroupName={#MyAppName}
UninstallDisplayIcon={app}\{MyAppExeName}
VersionInfoDescription=Patch 2.22 Launcher Setup
VersionInfoProductName=Patch 2.22 Launcher
OutputDir=build
OutputBaseFilename=BFMELauncherSetup
DisableWelcomePage=No
MissingMessagesWarning=yes
NotRecognizedMessagesWarning=yes
ShowLanguageDialog=no
WizardImageFile=setup.bmp
LicenseFile=ReadMe.txt
SetupIconFile=MainIcon.ico
VersionInfoVersion=1.0.1.9

[Languages]
Name: en; MessagesFile: "compiler:Default.isl"

[Files]
Source: "BFME_Launcher\*"; DestDir: "{app}"; Flags: recursesubdirs ignoreversion
Source: "BFME_Launcher\{#MyAppExeName}"; DestDir: "{app}"

[Icons]
Name: "{group}\{#MyAppName}"; Filename: "{app}\Updater.exe"
Name: "{userdesktop}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; IconFilename: "{app}\{#MyAppIcoName}"; Tasks: desktopicon

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked

[UninstallDelete]
Type: filesandordirs; Name: "Download"

[Run]
Filename: "{app}\{#MyAppExeName}"; Description: "Launch Application after Install"; Flags: postinstall nowait unchecked runasoriginaluser
Filename: "{app}\{#MyAppExeName}"; Flags: postinstall nowait runasoriginaluser skipifnotsilent

[InstallDelete]
Type: filesandordirs; Name: "{app}\Images"
Type: filesandordirs; Name: "{app}\Fonts"
Type: files; Name: "Updater.exe"
Type: files; Name: "GameSelection.exe"
Type: files; Name: "GameSelection.dll"
Type: files; Name: "Updater.dll"
Type: files; Name: "PatchLauncherBFME1.exe"
Type: files; Name: "PatchLauncherBFME1.dll"

[Code]
procedure CurPageChanged(CurPageID: Integer);
begin
  if CurPageID = wpFinished then
    WizardForm.RunList.Visible := False;
end;