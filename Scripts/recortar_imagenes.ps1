# Script PowerShell para recortar una imagen en 20 partes usando .NET
# Requiere tener la imagen guardada localmente

param(
    [Parameter(Mandatory=$true)]
    [string]$ImagenPath,
    
    [Parameter(Mandatory=$false)]
    [string]$OutputDir = "wwwroot\images\default"
)

Write-Host "Recortando imagen: $ImagenPath" -ForegroundColor Cyan
Write-Host "Directorio de salida: $OutputDir" -ForegroundColor Cyan
Write-Host ""

if (-not (Test-Path $ImagenPath)) {
    Write-Host "Error: No se encontró el archivo: $ImagenPath" -ForegroundColor Red
    exit 1
}

# Cargar la imagen usando .NET
Add-Type -AssemblyName System.Drawing

try {
    $img = [System.Drawing.Image]::FromFile($ImagenPath)
    $width = $img.Width
    $height = $img.Height
    
    # Calcular el tamaño de cada recorte (4 filas x 5 columnas)
    $cols = 5
    $rows = 4
    $cropWidth = [Math]::Floor($width / $cols)
    $cropHeight = [Math]::Floor($height / $rows)
    
    # Crear el directorio de salida si no existe
    if (-not (Test-Path $OutputDir)) {
        New-Item -ItemType Directory -Path $OutputDir -Force | Out-Null
    }
    
    # Recortar y guardar cada imagen
    $index = 1
    for ($row = 0; $row -lt $rows; $row++) {
        for ($col = 0; $col -lt $cols; $col++) {
            # Calcular las coordenadas del recorte
            $left = $col * $cropWidth
            $top = $row * $cropHeight
            $right = $left + $cropWidth
            $bottom = $top + $cropHeight
            
            # Crear un bitmap para el recorte
            $cropped = New-Object System.Drawing.Bitmap($cropWidth, $cropHeight)
            $graphics = [System.Drawing.Graphics]::FromImage($cropped)
            
            # Copiar la parte recortada
            $srcRect = New-Object System.Drawing.Rectangle($left, $top, $cropWidth, $cropHeight)
            $destRect = New-Object System.Drawing.Rectangle(0, 0, $cropWidth, $cropHeight)
            $graphics.DrawImage($img, $destRect, $srcRect, [System.Drawing.GraphicsUnit]::Pixel)
            
            # Guardar la imagen recortada
            $outputPath = Join-Path $OutputDir "default-$index.png"
            $cropped.Save($outputPath, [System.Drawing.Imaging.ImageFormat]::Png)
            
            Write-Host "✓ Creada: $outputPath" -ForegroundColor Green
            
            # Liberar recursos
            $graphics.Dispose()
            $cropped.Dispose()
            
            $index++
        }
    }
    
    # Liberar la imagen original
    $img.Dispose()
    
    Write-Host ""
    Write-Host "✓ Proceso completado! Se crearon $($index - 1) imágenes en $OutputDir" -ForegroundColor Green
    
} catch {
    Write-Host "Error: $_" -ForegroundColor Red
    exit 1
}

