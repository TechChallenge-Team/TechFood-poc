using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using TechFood.Application.Common.Attributes;

namespace TechFood.Application.Models.Category;

public class UpdateCategoryRequest
{
    [MaxFileSize(5 * 1024 * 1024)]
    [AllowedExtensions(".jpg", ".jpeg", ".png", ".webp")]
    public IFormFile? File { get; set; }

    [Required]
    public string Name { get; set; } = null!;
}
