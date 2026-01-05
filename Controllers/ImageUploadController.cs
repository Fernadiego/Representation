using Microsoft.AspNetCore.Mvc;
using BlazorVentas.Services;

namespace BlazorVentas.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ImageUploadController : ControllerBase
{
    private readonly ImageService _imageService;

    public ImageUploadController(ImageService imageService)
    {
        _imageService = imageService;
    }

    [HttpPost("product/{productId}")]
    public async Task<IActionResult> UploadProductImage(int productId, IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            return BadRequest("No se proporcionó ningún archivo");
        }

        var imagePath = await _imageService.SaveProductImageAsync(file.OpenReadStream(), productId, file.FileName);
        
        if (imagePath == null)
        {
            return BadRequest("Error al guardar la imagen");
        }

        return Ok(new { imagePath });
    }
}

