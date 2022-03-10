@echo off
pushd "%~dp0"
powershell Compress-7Zip -Path ([IO.Path]::GetFullPath((join-path (Get-Location) "\x86\Release\NiceDishy.exe"))) -ArchiveFileName "NiceDishyX86.zip" -Format Zip
powershell Compress-7Zip -Path ([IO.Path]::GetFullPath((join-path (Get-Location) "\x64\Release\NiceDishy.exe"))) -ArchiveFileName "NiceDishyX64.zip" -Format Zip
dir
echo %cd%
:exit
popd
@echo on