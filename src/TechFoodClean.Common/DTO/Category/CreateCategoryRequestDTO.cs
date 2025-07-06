using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using TechFoodClean.Common.Attributes;

namespace TechFoodClean.Common.Category;

public class CreateCategoryRequestDTO
{
    [Required]
    public required string Name { get; set; }

    [Required]
    [MaxFileSize(5 * 1024 * 1024)]
    [AllowedExtensions(".jpg", ".jpeg", ".png", ".webp")]
    public IFormFile File { get; set; }
}
