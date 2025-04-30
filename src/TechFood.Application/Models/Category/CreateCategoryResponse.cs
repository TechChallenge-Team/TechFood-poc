using System;

namespace TechFood.Application.Models.Category
{
    public class CreateCategoryResponse
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string ImageFileName { get; set; } 
    }
}
