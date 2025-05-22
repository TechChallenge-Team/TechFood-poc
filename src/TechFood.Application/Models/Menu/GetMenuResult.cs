using System;
using System.Collections.Generic;

namespace TechFood.Application.Models.Menu
{
    public class GetMenuResult
    {
        public List<Category> Categories { get; set; } = [];

        public class Category
        {
            public Guid Id { get; set; }

            public string Name { get; set; } = null!;

            public string ImageUrl { get; set; } = null!;

            public int SortOrder { get; set; }

            public List<Product> Products { get; set; } = [];
        }

        public class Product
        {
            public Guid Id { get; set; }

            public Guid CategoryId { get; set; }

            public string Name { get; set; } = null!;

            public string Description { get; set; } = null!;

            public decimal Price { get; set; }

            public string ImageUrl { get; set; } = null!;
        }
    }
}
