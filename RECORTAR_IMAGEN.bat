@echo off
chcp 65001 >nul
echo ========================================
echo Recortar Imagen de Captura en 20 Partes
echo ========================================
echo.

:: Buscar archivos de imagen comunes en el directorio actual
echo Buscando imágenes en el directorio actual...
echo.

set imagen_encontrada=0
for %%f in (captura.png captura.jpg captura.jpeg screenshot.png screenshot.jpg imagen.png imagen.jpg *.png *.jpg *.jpeg) do (
    if exist "%%f" (
        echo Imagen encontrada: %%f
        set imagen_path=%%f
        set imagen_encontrada=1
        goto :procesar
    )
)

:procesar
if "%imagen_encontrada%"=="1" (
    echo.
    echo Usando: %imagen_path%
    echo.
) else (
    echo No se encontró ninguna imagen en el directorio actual.
    echo.
    echo Por favor, proporciona la ruta completa de la imagen:
    echo (Puedes arrastrar y soltar el archivo aquí)
    echo.
    set /p imagen_path="Ruta de la imagen: "
    
    if "!imagen_path!"=="" (
        echo.
        echo Error: No se proporcionó ninguna ruta.
        pause
        exit /b
    )
    
    :: Remover comillas si las hay
    set imagen_path=!imagen_path:"=!
    
    if not exist "!imagen_path!" (
        echo.
        echo Error: No se encontró el archivo: !imagen_path!
        pause
        exit /b
    )
)

echo Procesando imagen...
echo.

python Scripts\recortar_simple.py "%imagen_path%"

if %ERRORLEVEL% EQU 0 (
    echo.
    echo ========================================
    echo ¡Proceso completado exitosamente!
    echo Las imágenes se guardaron en: wwwroot\images\default\
    echo ========================================
) else (
    echo.
    echo ========================================
    echo Error al procesar la imagen.
    echo Verifica que Pillow esté instalado: pip install Pillow
    echo ========================================
)

echo.
pause
