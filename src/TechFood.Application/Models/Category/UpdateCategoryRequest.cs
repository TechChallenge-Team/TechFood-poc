using System.ComponentModel.DataAnnotations;

namespace TechFood.Application.Models.Category;

public class UpdateCategoryRequest
{
    public UpdateCategoryRequest(string name, string imageFileName)
    {
        Name = name;
        ImageFileName = imageFileName;
    }

    [Required]
    public string Name { get; set; }

    [Required]
    public string ImageFileName { get; set; }
}
