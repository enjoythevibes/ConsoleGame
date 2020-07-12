@echo off
cd..
"C:\Program Files (x86)\Microsoft Visual Studio\2017\Community\MSBuild\15.0\Bin\MSBuild.exe" ConsoleGame.sln

if %ERRORLEVEL%==0 start ConsoleGame/bin/Debug/ConsoleGame.exe

