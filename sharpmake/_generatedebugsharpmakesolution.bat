@echo off
cd sharpmakeApplication
Sharpmake.Application.exe /sources(@"..\puma.sharpmake.main.cs") /verbose /generateDebugSolution
pause