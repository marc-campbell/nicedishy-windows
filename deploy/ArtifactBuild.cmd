@echo off
pushd "%~dp0"
powershell Compress-7Zip "Release" -ArchiveFileName "NiceDishyX86.zip" -Format Zip
powershell Compress-7Zip "x64\Release" -ArchiveFileName "NiceDishyX64.zip" -Format Zip
dir
echo %cd%
:exit
popd
@echo on