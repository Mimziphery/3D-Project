@echo off
ECHO =================================================================
ECHO Deleting old config. - Are you sure? (close this window if not)
ECHO =================================================================
pause
RMDIR /Q /S "Library"
RMDIR /Q /S "Logs"
RMDIR /Q /S "obj"
RMDIR /Q /S "UserSettings"
RMDIR /Q /S "Temp"
DEL /Q "3D-Project.sln"
DEL /Q "Assembly-CSharp.csproj"
echo Deleting completed
pause
