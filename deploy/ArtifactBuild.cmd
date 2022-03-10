@echo off
pushd "%~dp0"
dir D:\a\nicedishy-windows\nicedishy-windows
dir D:\a\nicedishy-windows\nicedishy-windows\NiceDishy
dir D:\a\nicedishy-windows\nicedishy-windows\NiceDishy\bin\x86\Release
echo "Creating x86 zip"
powershell Compress-7Zip -Path ([IO.Path]::GetFullPath((join-path (Get-Location) "..\NiceDishy\bin\x86\Release\NiceDishy.exe"))) -ArchiveFileName "NiceDishyX86.zip" -Format Zip
echo "Creating x64 zip"
powershell Compress-7Zip -Path ([IO.Path]::GetFullPath((join-path (Get-Location) "..\NiceDishy\bin\x64\Release\NiceDishy.exe"))) -ArchiveFileName "NiceDishyX64.zip" -Format Zip
dir
:exit
popd
@echo on