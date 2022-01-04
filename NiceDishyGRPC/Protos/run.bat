@echo off


SET CWD=%~dp0
echo %CWD%

SET GRPC_TOOLS_VER=2.42.0

SET PROTOC=%CWD%..\..\packages\Grpc.Tools.%GRPC_TOOLS_VER%\tools\windows_x86\protoc.exe
SET PLUGIN=%CWD%..\..\packages\Grpc.Tools.%GRPC_TOOLS_VER%\tools\windows_x86\grpc_csharp_plugin.exe
SET IMPORT=%CWD%..\..\packages\Grpc.Tools.%GRPC_TOOLS_VER%\build\native\include\
SET DESCRIPTOR_SET=%CWD%dish.protoset
SET SRC=%CWD%..\CSharp\

IF NOT exist %SRC% MKDIR %SRC%

%PROTOC% ^
	--plugin=protoc-gen-grpc=%PLUGIN% ^
	--descriptor_set_in=%DESCRIPTOR_SET% ^
	--csharp_out=%SRC% ^
	--grpc_out=%SRC% ^
	-I %IMPORT% ^
	spacex/api/device/device.proto ^
	spacex/api/device/command.proto ^
	spacex/api/device/common.proto ^
	spacex/api/device/dish.proto ^
	spacex/api/device/wifi.proto ^
	spacex/api/device/wifi_config.proto ^
	spacex/api/device/transceiver.proto ^
	spacex/api/common/status/status.proto