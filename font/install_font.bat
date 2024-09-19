@echo off
set fontDir="G:\Users\hardy\Documents\Code_Saved_Projects\Visual Studio\Projects\PointOfSalesSystem\font"

echo Installing fonts from %fontDir%...

for %%i in (%fontDir%\*.otf) do (
    echo Installing %%~nxi...
    if not exist "%windir%\Fonts\%%~nxi" (
        copy %%i "%windir%\Fonts\"
    ) else (
        echo Font %%~nxi is already installed.
    )
)

echo All fonts installed.
pause
