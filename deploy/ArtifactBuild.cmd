@echo off
pushd "%~dp0"
echo "%cd%"
echo "1"
dir D:\a\nicedishy-windows\nicedishy-windows
echo "2"
dir D:\a\nicedishy-windows\nicedishy-windows\NiceDishy
echo "3"
dir D:\a\nicedishy-windows\nicedishy-windows\NiceDishy\bin\x86\Release
echo "Creating x86 zip"
powershell Compress-7Zip -Path ([IO.Path]::GetFullPath((join-path (Get-Location) "..\NiceDishy\bin\x86\Release\NiceDishy.exe"))) -ArchiveFileName "NiceDishyX86.zip" -Format Zip
echo "Creating x64 zip"
powershell Compress-7Zip -Path ([IO.Path]::GetFullPath((join-path (Get-Location) "..\NiceDishy\bin\x64\Release\NiceDishy.exe"))) -ArchiveFileName "NiceDishyX64.zip" -Format Zip
dir
:exit
popd
@echo on