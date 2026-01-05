# Instrucciones para Recortar la Imagen en 20 Partes

Para recortar la captura de pantalla en las 20 imágenes por defecto, sigue estos pasos:

## Opción 1: Usando Python (Recomendado)

1. Guarda la captura de pantalla en la raíz del proyecto con el nombre `captura.png`
2. Asegúrate de tener Pillow instalado:
   ```
   pip install Pillow
   ```
3. Ejecuta el script desde la raíz del proyecto:
   ```
   python Scripts\recortar_imagenes.py captura.png wwwroot\images\default
   ```

## Opción 2: Usando PowerShell

1. Guarda la captura de pantalla en la raíz del proyecto con el nombre `captura.png`
2. Ejecuta el script:
   ```
   .\Scripts\recortar_imagenes.ps1 -ImagenPath captura.png -OutputDir wwwroot\images\default
   ```

## Opción 3: Manualmente con herramientas gráficas

1. Abre la captura en un editor de imágenes (Paint, GIMP, Photoshop, etc.)
2. La imagen debe tener 4 filas y 5 columnas (20 iconos en total)
3. Recorta cada icono circular y guárdalo como:
   - default-1.png (primera fila, primer icono)
   - default-2.png (primera fila, segundo icono)
   - ... hasta default-20.png

## Verificación

Después de ejecutar el script, deberías tener 20 archivos:
- default-1.png hasta default-20.png
en el directorio `wwwroot\images\default\`

