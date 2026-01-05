#!/usr/bin/env python3
"""
Script para recortar una imagen en 20 imágenes por defecto.
La imagen debe tener 4 filas y 5 columnas (20 iconos circulares).
"""

import sys
from PIL import Image
import os

def recortar_imagen_default(imagen_path, output_dir):
    """
    Recorta una imagen en 20 partes (4 filas x 5 columnas)
    """
    try:
        # Abrir la imagen
        img = Image.open(imagen_path)
        width, height = img.size
        
        # Calcular el tamaño de cada recorte (4 filas x 5 columnas)
        cols = 5
        rows = 4
        crop_width = width // cols
        crop_height = height // rows
        
        # Crear el directorio de salida si no existe
        os.makedirs(output_dir, exist_ok=True)
        
        # Recortar y guardar cada imagen
        index = 1
        for row in range(rows):
            for col in range(cols):
                # Calcular las coordenadas del recorte
                left = col * crop_width
                top = row * crop_height
                right = left + crop_width
                bottom = top + crop_height
                
                # Recortar la imagen
                cropped = img.crop((left, top, right, bottom))
                
                # Guardar la imagen recortada
                output_path = os.path.join(output_dir, f"default-{index}.png")
                cropped.save(output_path, "PNG")
                
                print(f"✓ Creada: {output_path}")
                index += 1
        
        print(f"\n✓ Proceso completado! Se crearon {index - 1} imágenes en {output_dir}")
        return True
        
    except FileNotFoundError:
        print(f"✗ Error: No se encontró el archivo {imagen_path}")
        return False
    except Exception as e:
        print(f"✗ Error: {str(e)}")
        return False

if __name__ == "__main__":
    if len(sys.argv) < 2:
        print("Uso: python recortar_imagenes.py <ruta_imagen> [directorio_salida]")
        print("\nEjemplo:")
        print("  python recortar_imagenes.py captura.png wwwroot/images/default")
        sys.exit(1)
    
    imagen_path = sys.argv[1]
    
    # Directorio de salida por defecto
    if len(sys.argv) >= 3:
        output_dir = sys.argv[2]
    else:
        output_dir = "wwwroot/images/default"
    
    print(f"Recortando imagen: {imagen_path}")
    print(f"Directorio de salida: {output_dir}\n")
    
    recortar_imagen_default(imagen_path, output_dir)

