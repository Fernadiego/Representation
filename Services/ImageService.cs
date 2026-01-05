using System.Security.Cryptography;
using System.Text;
using System.IO;

namespace BlazorVentas.Services;

public class ImageService
{
    private readonly IWebHostEnvironment _environment;
    private readonly string _imagesDirectory = "uploads/productos";
    private readonly string[] _defaultImageNames = new[]
    {
        "default-1.png",  // Sillón rosa
        "default-2.png",  // Botella blanca
        "default-3.png",  // Smartphones
        "default-4.png",  // Smartwatch naranja
        "default-5.png"   // Zapatillas azules
    };

    public ImageService(IWebHostEnvironment environment)
    {
        _environment = environment;
        
        // Asegurar que el directorio existe
        var uploadsPath = Path.Combine(_environment.WebRootPath, _imagesDirectory);
        if (!Directory.Exists(uploadsPath))
        {
            Directory.CreateDirectory(uploadsPath);
        }
    }

    public async Task<string?> SaveProductImageAsync(Stream imageStream, int productId, string fileName)
    {
        try
        {
            var extension = Path.GetExtension(fileName).ToLowerInvariant();
            if (extension != ".jpg" && extension != ".jpeg" && extension != ".png" && extension != ".gif" && extension != ".webp")
            {
                throw new ArgumentException("Formato de imagen no válido. Se permiten: jpg, jpeg, png, gif, webp");
            }

            // Generar nombre único para la imagen
            var uniqueFileName = $"product_{productId}_{Guid.NewGuid()}{extension}";
            var filePath = Path.Combine(_environment.WebRootPath, _imagesDirectory, uniqueFileName);

            // Guardar la imagen
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await imageStream.CopyToAsync(fileStream);
            }

            // Retornar la ruta relativa para usar en la web
            return $"/{_imagesDirectory.Replace("\\", "/")}/{uniqueFileName}";
        }
        catch (Exception)
        {
            return null;
        }
    }

    public void DeleteProductImage(string? imagePath)
    {
        if (string.IsNullOrWhiteSpace(imagePath))
            return;

        try
        {
            var filePath = Path.Combine(_environment.WebRootPath, imagePath.TrimStart('/').Replace("/", "\\"));
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
        catch
        {
            // Ignorar errores al eliminar
        }
    }

    public string GetDefaultImagePath(int? seed = null)
    {
        // Usar el seed para seleccionar una imagen aleatoria pero determinística
        // Si no hay seed, usar un número aleatorio basado en la fecha/hora
        var index = seed.HasValue 
            ? Math.Abs(seed.Value % _defaultImageNames.Length)
            : Random.Shared.Next(0, _defaultImageNames.Length);
        
        // Asegurar que el índice sea válido
        if (index < 0) index = 0;
        if (index >= _defaultImageNames.Length) index = 0;
        
        var imageName = _defaultImageNames[index];
        
        // Verificar si la imagen existe
        try
        {
            var fullPath = Path.Combine(_environment.WebRootPath, "images", "default", imageName);
            if (File.Exists(fullPath))
            {
                return $"/images/default/{imageName}";
            }
        }
        catch
        {
            // Ignorar errores de acceso a archivos
        }
        
        // Si no existe, usar el favicon como fallback
        try
        {
            var faviconPath = Path.Combine(_environment.WebRootPath, "favicon.png");
            if (File.Exists(faviconPath))
            {
                return "/favicon.png";
            }
        }
        catch
        {
            // Ignorar errores de acceso a archivos
        }
        
        // Si tampoco existe, usar un placeholder SVG inline simple (círculo gris)
        return "data:image/svg+xml;charset=utf-8,%3Csvg xmlns='http://www.w3.org/2000/svg' width='50' height='50'%3E%3Ccircle cx='25' cy='25' r='25' fill='%23e0e0e0'/%3E%3C/svg%3E";
    }

    public string GetProductImagePath(string? imagePath, int productId)
    {
        if (!string.IsNullOrWhiteSpace(imagePath))
        {
            try
            {
                // Verificar que la imagen existe
                var cleanPath = imagePath.TrimStart('/').Replace("/", Path.DirectorySeparatorChar.ToString());
                var fullPath = Path.Combine(_environment.WebRootPath, cleanPath);
                if (File.Exists(fullPath))
                {
                    return imagePath;
                }
            }
            catch
            {
                // Si hay error al verificar, continuar con la imagen por defecto
            }
        }

        // Usar el ID del producto como seed para que siempre muestre la misma imagen por defecto
        return GetDefaultImagePath(productId);
    }
}

