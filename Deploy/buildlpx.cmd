@echo off

set version=7.1.1
set fileName=CsvLINQPadDriver.%version%
set ext=lpx
set ext6=%ext%6

set zip="%ProgramFiles%\7-Zip\7z.exe"

echo on

call :pack %fileName%-net6.%ext6%   net6.0-windows
call :pack %fileName%-net5.%ext6%   net5.0-windows
call :pack %fileName%-net3.1.%ext6% netcoreapp3.1
call :pack %fileName%.%ext%         net461

@echo off

echo.
pause

exit /b 0

:pack
@echo off

set lpx=%1
set folder=..\bin\Release\%2

set additional=

if exist %lpx% del %lpx%
if exist %folder%\Microsoft.Bcl.*.dll set additional=^
%folder%\Microsoft.Bcl.HashCode.dll

echo on

%zip% a -tzip -mx=9 %lpx% ^
header.xml ^
..\README.md ^
..\LICENSE ^
%folder%\*Connection.png ^
%folder%\CsvHelper.dll ^
%folder%\CsvLINQPadDriver.dll ^
%folder%\Humanizer.dll ^
%folder%\Microsoft.WindowsAPICodePack.dll ^
%folder%\Microsoft.WindowsAPICodePack.Shell.dll ^
%folder%\UnicodeCharsetDetector.dll ^
%folder%\UtfUnknown.dll ^
%additional%

@exit /b 0
