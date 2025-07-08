using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using TechFoodClean.Common.Attributes;

namespace TechFoodClean.Common.DTO.Category;

public class UpdateCategoryRequestDTO
{
    [MaxFileSize(5 * 1024 * 1024)]
    [AllowedExtensions(".jpg", ".jpeg", ".png", ".webp")]
    public IFormFile? File { get; set; }

    [Required]
    public string Name { get; set; } = null!;
}
