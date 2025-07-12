namespace TechFoodClean.Common.DTO
{
    public class MenuDTO
    {

        public List<Category> Categories { get; set; } = [];

        public class Category
        {
            public Guid Id { get; set; }

            public string Name { get; set; } = null!;

            public string ImageUrl { get; set; } = null!;

            public int SortOrder { get; set; }

            public List<ProductMenu> Products { get; set; } = [];
        }

        public class ProductMenu
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
