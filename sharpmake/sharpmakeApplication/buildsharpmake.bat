@echo off
rd /s /q bin
echo Deleted current files...
mkdir bin
echo Created bin folder...
echo Calling sharpmake bootstrap...
call submodule\bootstrap.bat
echo Sharpmake build finished
echo Copying files...
xcopy /s submodule\tmp\bin\debug\net6.0 bin

echo ********
echo **DONE**
echo ********
pause