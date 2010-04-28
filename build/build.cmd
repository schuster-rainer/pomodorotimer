@echo off
set PATH=%PATH%;"..\tools\ironruby-1.0_NET20\bin"
rem This is needed for machines that have other Ruby interpreters installed.
set rubyopt=

:Build
cls
ir "..\tools\Rake\bin\rake" %*

rem Bail if we're running a TeamCity build.
if defined TEAMCITY_PROJECT_NAME goto Quit

rem Loop the build script.
set CHOICE=nothing
echo (Q)uit, (Enter) runs the build again
set /P CHOICE= 
if /i "%CHOICE%"=="Q" goto :Quit

GOTO Build

:Quit