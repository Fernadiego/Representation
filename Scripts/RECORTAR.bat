@echo off
echo ========================================
echo Recortar Imagen en 20 Partes
echo ========================================
echo.

if "%1"=="" (
    echo Uso: RECORTAR.bat ruta_de_la_imagen
    echo.
    echo Ejemplo:
    echo   RECORTAR.bat captura.png
    echo   RECORTAR.bat "C:\Users\Usuario\Downloads\captura.png"
    echo.
    pause
    exit /b
)

echo Procesando imagen: %1
echo.

python Scripts\recortar_simple.py %1

echo.
echo ========================================
echo Proceso completado!
echo ========================================
pause

