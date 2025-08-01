@echo off
setlocal

REM Define default delay if not provided
set DELAY=%1
if "%DELAY%"=="" (
    set DELAY=1
)

powershell -NoProfile -ExecutionPolicy Bypass -Command "for ($i = 1; $i -le 10000; $i++) { Invoke-WebRequest -Uri 'http://localhost:30000/api/v1/Menu' -UseBasicParsing | Out-Null; Start-Sleep -Seconds %DELAY% }"
