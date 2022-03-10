@echo off
pushd "%~dp0"
if exist Debug rd /s /q Debug
if exist Release rd /s /q Release
if exist x64 rd /s /q x64
"%programfiles(x86)%\Microsoft Visual Studio\2019\Enterprise\MSBuild\Current\Bin\msbuild.exe" /p:Configuration=Release /p:Platform=x64 ..\NiceDishy\NiceDishy.csprojs
"%programfiles(x86)%\Microsoft Visual Studio\2019\Enterprise\MSBuild\Current\Bin\msbuild.exe" /p:Configuration=Release /p:Platform=x86 ..\NiceDishy\NiceDishy.csproj
dir
echo %cd%
:exit
popd
@echo on