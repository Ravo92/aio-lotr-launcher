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
UninstallDisplayIcon={app}\{#MyAppExeName}
VersionInfoDescription=Patch 2.22 Launcher Setup
VersionInfoProductName=Patch 2.22 Launcher
OutputDir=setup
OutputBaseFilename=BFMELauncherSetup
DisableWelcomePage=no
PrivilegesRequired=admin
MissingMessagesWarning=yes
NotRecognizedMessagesWarning=yes
ShowLanguageDialog=no
WizardImageFile=setup.bmp
LicenseFile=ReadMe.txt
SetupIconFile=MainIcon.ico
VersionInfoVersion=1.0.1.14

[Languages]
Name: en; MessagesFile: "compiler:Default.isl"

[Files]
Source: "BFME_Launcher\*"; DestDir: "{app}"; Flags: recursesubdirs ignoreversion
Source: "BFME_Launcher\{#MyAppExeName}"; DestDir: "{app}"

[Icons]
Name: "{group}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"
Name: "{userdesktop}\{#MyAppName}"; Filename:"{app}\{#MyAppExeName}"; Check: Not FileExists(ExpandConstant('{userdesktop}{#MyAppName}.lnk')) 

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked; Check: Not FileExists(ExpandConstant('{userdesktop}{#MyAppName}.lnk')) 

[UninstallDelete]
Type: filesandordirs; Name: "Download"

[Run]
Filename: {app}\{#MyAppExeName}; Description: "Launch Application after Install"; Flags: postinstall nowait unchecked
Filename: {app}\{#MyAppExeName}; Flags: postinstall nowait shellexec skipifnotsilent

[Code]
procedure CurPageChanged(CurPageID: Integer);
begin
  if CurPageID = wpFinished then
    WizardForm.RunList.Visible := False;
end;