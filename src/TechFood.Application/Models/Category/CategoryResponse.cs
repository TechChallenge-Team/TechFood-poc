using System;

namespace TechFood.Application.Models.Category
{
    public class CategoryResponse
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string ImageUrl { get; set; }
    }
}
