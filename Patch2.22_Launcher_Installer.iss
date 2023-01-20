#define MyAppName "Patch 2.22 Launcher"
#define MyAppExeName "PatchLauncherBFME.exe"

[Setup]
AppName={#MyAppName}
AppId=Patch 2.22 Launcher
AppVerName={#MyAppName}
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
VersionInfoVersion=1.0.1.15
AppSupportURL=https://discord.com/invite/Q5Yyy3XCuu
AppPublisher=Raphael Vogel
AppPublisherURL=https://github.com/Ravo92

[Languages]
Name: en; MessagesFile: "compiler:Default.isl"

[Files]
Source: "BFME_Launcher\*"; DestDir: "{app}"; Flags: recursesubdirs ignoreversion
Source: "BFME_Launcher\{#MyAppExeName}"; DestDir: "{app}"

[Icons]
Name: "{group}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"
Name: "{userdesktop}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; IconFilename: "{app}\{#MyAppExeName}"; Tasks: desktopicon; Check: Not FileExists(ExpandConstant('{userdesktop}\{#MyAppName}.lnk')) 

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked

[UninstallDelete]
Type: filesandordirs; Name: "Downloads"

[Run]
Filename: {app}\{#MyAppExeName}; Description: "Launch Application after Install"; Flags: postinstall shellexec nowait unchecked skipifsilent;
Filename: {app}\{#MyAppExeName}; Flags: postinstall nowait shellexec skipifnotsilent