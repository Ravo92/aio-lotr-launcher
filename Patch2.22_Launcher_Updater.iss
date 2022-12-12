#define MyAppName "Patch 2.22 Launcher Update"
#define MyAppVerName "Patch 2.22 Launcher Update"
#define MyAppExeName "Updater.exe"
#define MyAppIcoName "Updater.exe"

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
OutputBaseFilename=BFMELauncherUpdate1012
DisableWelcomePage=No
MissingMessagesWarning=yes
NotRecognizedMessagesWarning=yes
ShowLanguageDialog=no
WizardImageFile=setup.bmp
LicenseFile=ReadMe.txt
SetupIconFile=MainIcon.ico
VersionInfoVersion=1.0.1.2
DisableDirPage=auto

[Languages]
Name: en; MessagesFile: "compiler:Default.isl"

[Files]
Source: "BFME_Launcher\*"; DestDir: "{app}"; Flags: recursesubdirs
Source: "BFME_Launcher\{#MyAppExeName}"; DestDir: "{app}"
Source: "ReadMe.txt"; DestDir: "{app}\ReadMe.txt"; Languages: en; Flags: isreadme

[Icons]
Name: "{group}\{#MyAppName}"; Filename: "{app}\Updater.exe"
Name: "{userdesktop}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; IconFilename: "{app}\{#MyAppIcoName}"; Tasks: desktopicon

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked

[Run]
Filename: "{app}\{#MyAppExeName}"; Description: "Launch Application after Install"; Flags: postinstall nowait unchecked shellexec runasoriginaluser