using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using TechFood.Application.Common.Attributes;

namespace TechFood.Application.Models.Product;

public class UpdateProductRequest
{
    [Required]
    public string Name { get; set; }

    [Required]
    public string Description { get; set; }

    [Required]
    public Guid CategoryId { get; set; }

    [MaxFileSize(5 * 1024 * 1024)]
    [AllowedExtensions(".jpg", ".jpeg", ".png", ".webp")]
    public IFormFile File { get; set; }

    [Required]
    public decimal Price { get; set; }
}
