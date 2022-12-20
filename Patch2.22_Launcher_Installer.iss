#define MyAppName "Patch 2.22 Launcher"
#define MyAppVerName "Patch 2.22 Launcher"
#define MyAppExeName "Updater.exe"
#define MyAppIcoName "GameSelection.exe"

[Setup]
AppName={#MyAppName}
AppId=Patch 2.22 Launcher
AppVerName={#MyAppVerName}
WizardStyle=modern
DefaultDirName={userappdata}\{#MyAppName}
DefaultGroupName={#MyAppName}
UninstallDisplayIcon={app}\Updater.exe
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
VersionInfoVersion=1.0.1.6

[Languages]
Name: en; MessagesFile: "compiler:Default.isl"

[Files]
Source: "BFME_Launcher\*"; DestDir: "{app}"; Flags: recursesubdirs
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
Type: filesandordirs; Name: "{app}\ReadMe.txt"

[Code]
procedure CurPageChanged(CurPageID: Integer);
begin
  if CurPageID = wpFinished then
    WizardForm.RunList.Visible := False;
end;