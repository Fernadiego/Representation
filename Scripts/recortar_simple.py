"""
Script simple para recortar una imagen en 20 partes (4 filas x 5 columnas)
Uso: python recortar_simple.py <ruta_imagen>
"""

from PIL import Image
import os
import sys

def main():
    if len(sys.argv) < 2:
        print("Uso: python recortar_simple.py <ruta_imagen>")
        print("\nEjemplo:")
        print("  python recortar_simple.py captura.png")
        sys.exit(1)
    
    imagen_path = sys.argv[1]
    
    if not os.path.exists(imagen_path):
        print(f"Error: No se encontró el archivo: {imagen_path}")
        sys.exit(1)
    
    # Directorio de salida
    output_dir = os.path.join(os.path.dirname(os.path.dirname(__file__)), "wwwroot", "images", "default")
    os.makedirs(output_dir, exist_ok=True)
    
    print(f"Recortando: {imagen_path}")
    print(f"Guardando en: {output_dir}\n")
    
    try:
        img = Image.open(imagen_path)
        width, height = img.size
        
        print(f"Tamaño original: {width}x{height}")
        
        # 4 filas x 5 columnas = 20 imágenes
        cols = 5
        rows = 4
        crop_width = width // cols
        crop_height = height // rows
        
        print(f"Tamaño de cada recorte: {crop_width}x{crop_height}\n")
        
        index = 1
        for row in range(rows):
            for col in range(cols):
                left = col * crop_width
                top = row * crop_height
                right = left + crop_width
                bottom = top + crop_height
                
                cropped = img.crop((left, top, right, bottom))
                
                output_path = os.path.join(output_dir, f"default-{index}.png")
                cropped.save(output_path, "PNG")
                
                print(f"✓ default-{index}.png")
                index += 1
        
        print(f"\n✓ ¡Completado! {index - 1} imágenes creadas.")
        
    except Exception as e:
        print(f"Error: {e}")
        sys.exit(1)

if __name__ == "__main__":
    main()

