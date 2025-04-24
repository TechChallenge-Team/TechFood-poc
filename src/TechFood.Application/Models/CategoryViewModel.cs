using System;

namespace TechFood.Application.Models
{
    public class CategoryViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public string ImageUrl { get; set; } = null!;
    }
}
