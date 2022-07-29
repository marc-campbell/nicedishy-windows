; Script generated by the Inno Setup Script Wizard.
; SEE THE DOCUMENTATION FOR DETAILS ON CREATING INNO SETUP SCRIPT FILES!

#define MyAppName "NiceDishy"
#define MyAppVersion "0.0.2"
#define MyAppPublisher "Marc Campbell"
#define MyAppURL "https://nicedishy.com"
#define MyAppExeName "NiceDishy.exe"

[Setup]
; NOTE: The value of AppId uniquely identifies this application. Do not use the same AppId value in installers for other applications.
; (To generate a new GUID, click Tools | Generate GUID inside the IDE.)
AppId={{E57B1F7D-31CC-4DCD-B88B-2FEA492A53FE}
AppName={#MyAppName}
AppVersion={#MyAppVersion}
;AppVerName={#MyAppName} {#MyAppVersion}
AppPublisher={#MyAppPublisher}
AppPublisherURL={#MyAppURL}
AppSupportURL={#MyAppURL}
AppUpdatesURL={#MyAppURL}
DefaultDirName={autopf}\{#MyAppName}
DisableProgramGroupPage=yes
LicenseFile=C:\go\src\github.com\marc-campbell\nicedishy-windows\LICENSE
; Remove the following line to run in administrative install mode (install for all users.)
PrivilegesRequired=lowest
OutputBaseFilename=nicedishy
Compression=lzma
SolidCompression=yes
WizardStyle=modern
SignTool=signtool sign /a /n $qMarc Campbell$q /t http://timestamp.comodoca.com/authenticode /d $qNiceDishy$q $f

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked

[Files]
Source: "C:\go\src\github.com\marc-campbell\nicedishy-windows\NiceDishyCore\bin\Release\net6.0-windows\publish\win-x86\NiceDishy.exe"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\go\src\github.com\marc-campbell\nicedishy-windows\NiceDishyCore\bin\Release\net6.0-windows\publish\win-x86\D3DCompiler_47_cor3.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\go\src\github.com\marc-campbell\nicedishy-windows\NiceDishyCore\bin\Release\net6.0-windows\publish\win-x86\grpc_csharp_ext.x64.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\go\src\github.com\marc-campbell\nicedishy-windows\NiceDishyCore\bin\Release\net6.0-windows\publish\win-x86\grpc_csharp_ext.x86.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\go\src\github.com\marc-campbell\nicedishy-windows\NiceDishyCore\bin\Release\net6.0-windows\publish\win-x86\libgrpc_csharp_ext.x64.dylib"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\go\src\github.com\marc-campbell\nicedishy-windows\NiceDishyCore\bin\Release\net6.0-windows\publish\win-x86\libgrpc_csharp_ext.x64.so"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\go\src\github.com\marc-campbell\nicedishy-windows\NiceDishyCore\bin\Release\net6.0-windows\publish\win-x86\PenImc_cor3.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\go\src\github.com\marc-campbell\nicedishy-windows\NiceDishyCore\bin\Release\net6.0-windows\publish\win-x86\PresentationNative_cor3.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\go\src\github.com\marc-campbell\nicedishy-windows\NiceDishyCore\bin\Release\net6.0-windows\publish\win-x86\vcruntime140_cor3.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\go\src\github.com\marc-campbell\nicedishy-windows\NiceDishyCore\bin\Release\net6.0-windows\publish\win-x86\wpfgfx_cor3.dll"; DestDir: "{app}"; Flags: ignoreversion

; NOTE: Don't use "Flags: ignoreversion" on any shared system files

[Icons]
Name: "{autoprograms}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"
Name: "{autodesktop}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; Tasks: desktopicon

[Run]
Filename: "{app}\{#MyAppExeName}"; Description: "{cm:LaunchProgram,{#StringChange(MyAppName, '&', '&&')}}"; Flags: nowait postinstall skipifsilent

