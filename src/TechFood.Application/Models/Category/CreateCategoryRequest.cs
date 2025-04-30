namespace TechFood.Application.Models.Category
{
    public class CreateCategoryRequest
    {
        public required string Name { get; set; }

        public required string ImageFileName { get; set; }
    }
}
